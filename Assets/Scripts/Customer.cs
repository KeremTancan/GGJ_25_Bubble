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
    public static Action<Customer> OnCustomerDestroyedWithOrder; // Yeni event: Order'ı silmek için

    public float maxWaitingTime = 22f;
    private float remainingWaitingTime;
    private bool isWaiting = true;

    public int index = -1; // Hangi bekleme barıyla ilişkili olduğunu tutar
    public Image waitingBar; // Bekleme çubuğu
    public Image faceExpression; // Yüz ifadesi için görsel

    public event Action OnCustomerDestroyed;

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

        // Doğru bekleme çubuğunu ve yüz ifadesini al
        if (index >= 0)
        {
            GameObject barObject = TimeManagerUI.Instance(false).GetCustomerWaitingBars()[index];
            waitingBar = barObject.GetComponent<Image>();
        }

        if (waitingBar != null)
        {
            waitingBar.fillAmount = 1f; // Çubuğu tam doldur
            waitingBar.color = Color.green; // İlk renk yeşil
        }

        if (faceExpression != null)
        {
            faceExpression.sprite = GetHappyFace(); // İlk yüz ifadesi mutlu
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

            if (waitingBar != null)
            {
                waitingBar.fillAmount = remainingWaitingTime / maxWaitingTime;

                // Renk güncellemesi
                float percentage = remainingWaitingTime / maxWaitingTime;
                if (percentage > 0.5f)
                {
                    waitingBar.color = Color.green; // %50'den fazla süre kaldıysa yeşil
                    if (faceExpression != null) faceExpression.sprite = GetHappyFace(); // Mutlu yüz ifadesi
                }
                else if (percentage > 0.2f)
                {
                    waitingBar.color = Color.yellow; // %20-%50 arası süre kaldıysa sarı
                    if (faceExpression != null) faceExpression.sprite = GetNeutralFace(); // Bıkkın yüz ifadesi
                }
                else
                {
                    waitingBar.color = Color.red; // %20'den az süre kaldıysa kırmızı
                    if (faceExpression != null) faceExpression.sprite = GetAngryFace(); // Sinirli yüz ifadesi
                }
            }
        }

        if (remainingWaitingTime <= 0)
        {
            isWaiting = false;

            GameManager.Instance(false).RemoveWaitedCustomer(this);
        }
    }

    public void DeliverOrder()
    {
        isWaiting = false;

        // Mutlu müşteri animasyonu oynat ve yok et
        PlayExitingAnimation(true, () =>
        {
            OnCustomerDestroyed?.Invoke();
            Destroy(gameObject);
        });
    }

    // Yüz ifadesi sprite'larını almak için metotlar
    private Sprite GetHappyFace()
    {
        return Resources.Load<Sprite>("Faces/HappyFace"); // Mutlu yüz sprite yolu
    }

    private Sprite GetNeutralFace()
    {
        return Resources.Load<Sprite>("Faces/NeutralFace"); // Bıkkın yüz sprite yolu
    }

    private Sprite GetAngryFace()
    {
        return Resources.Load<Sprite>("Faces/AngryFace"); // Sinirli yüz sprite yolu
    }
}
    