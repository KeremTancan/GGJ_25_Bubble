using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;
    public static Action<Customer> OnCustomerDestroyedWithOrder; // Yeni olay: Müşteri yok olurken tetiklenir

    public float maxWaitingTime = 22f;
    private float remainingWaitingTime;
    private bool isWaiting = true;

    public int index = -1; // Hangi bekleme barıyla ilişkili olduğunu tutar
    public Image waitingBar; // Bekleme çubuğu

    public event Action OnCustomerDestroyed; // Müşteri yok olduğunda tetiklenecek olay

    Order order;

    SkeletonAnimation skeletonAnimation;
    CustomerAnimation customerAnimation;

    void Awake()
    {
        name = "Customer";
        GetOrder();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        customerAnimation = GetComponent<CustomerAnimation>();

        PlayEnteringAnimation(() =>
        {
            OnAnyCustomerGenerated?.Invoke(this);
        });

        remainingWaitingTime = maxWaitingTime;

        // Doğru bekleme çubuğunu al
        if (index >= 0)
        {
            GameObject barObject = TimeManagerUI.Instance(false).GetCustomerWaitingBars()[index];
            waitingBar = barObject.GetComponent<Image>();
        }

        if (waitingBar != null)
        {
            waitingBar.fillAmount = 1f; // Çubuğu tam doldur
        }

        StartCoroutine(WaitingCountdown());
    }

    public void PlayEnteringAnimation(Action onComplete)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { onComplete?.Invoke(); });
    }

    public void PlayExitingAnimation(bool isHappy, Action onComplete)
    {
        if (isHappy)
        {
            BeHappy(() =>
            {
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { onComplete?.Invoke(); });
            });
        }
        else
        {
            BeAngry(() =>
            {
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { onComplete?.Invoke(); });
            });
        }
    }

    public void AddIdleAnimation()
    {
        customerAnimation.AddIdleAnimation();
    }
    public void BeAngry(Action onComplete)
    {
        customerAnimation.PlayAngryAnimation(onComplete);
    }

    public void BeHappy(Action onComplete)
    {
        customerAnimation.PlayHappyAnimation(onComplete);
    }

    void OnEnable()
    {
        ExposedList<Skin> listOfSkins = skeletonAnimation.skeleton.Data.Skins;
        skeletonAnimation.skeleton.Skin = GetRandomElement(listOfSkins);
        skeletonAnimation.skeleton.SetSlotsToSetupPose();
    }

    Skin GetRandomElement(ExposedList<Skin> skins)
    {
        return skins.Items[UnityEngine.Random.Range(0, skins.Count)];
    }

    void OnDestroy()
    {
        //DestroyImmediate(gameObject);
    }

    public Order GetOrder()
    {
        if (order == null)
        {
            order = OrderGenerator.Instance().GenerateRandomOrder();
        }
        return order;
    }

    private IEnumerator WaitingCountdown()
    {
        while (remainingWaitingTime > 0 && isWaiting)
        {
            yield return new WaitForSeconds(1f);
            remainingWaitingTime--;

            // Bekleme çubuğunu güncelle
            if (waitingBar != null)
            {
                waitingBar.fillAmount = remainingWaitingTime / maxWaitingTime;
            }
        }

        if (remainingWaitingTime <= 0)
        {
            isWaiting = false;

            // Önce angry animasyonu oyna, ardından order'ı sil ve yok et
            BeAngry(() =>
            {
                OnCustomerDestroyedWithOrder?.Invoke(this); // Order'ı silmek için olay tetiklenir
                OnCustomerDestroyed?.Invoke();
                Destroy(gameObject);
            });
        }
    }

    public void DeliverOrder()
    {
        isWaiting = false;

        // Mutlu müşteri animasyonu oynat ve yok et
        PlayExitingAnimation(true, () =>
        {
            OnCustomerDestroyedWithOrder?.Invoke(this); // Order'ı silmek için olay tetiklenir
            OnCustomerDestroyed?.Invoke();
            Destroy(gameObject);
        });
    }
}
