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

    public float minWaitingTime = 22f;
    public float maxWaitingTime = 22f;
    private float remainingWaitingTime;
    private bool isWaiting = true;

    public int index = -1; // Hangi bekleme barıyla ilişkili olduğunu tutar
    public GameObject waitingBarGO;
    private Image face;
    private Image waitingBar;

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
        
        int xCoord = (int)transform.position.x;
        index = Math.Sign(xCoord) + 1;

        PlayEnteringAnimation(() =>
        {
            OnAnyCustomerGenerated?.Invoke(this);
        });

        remainingWaitingTime = maxWaitingTime;

        // Doğru bekleme çubuğunu ve yüz ifadesini al
        if (index >= 0)
        {
            waitingBarGO = TimeManagerUI.Instance(false).GetCustomerWaitingBars()[index];
            waitingBarGO.SetActive(true);
            waitingBar = waitingBarGO.transform.Find("FillBar").GetComponent<Image>();
            face = waitingBarGO.transform.Find("Face").GetComponent<Image>();
        }

        if (waitingBar != null)
        {
            waitingBar.fillAmount = 1f; // Çubuğu tam doldur
            waitingBar.color = Color.green; // İlk renk yeşil
        }

        if (face != null)
        {
            face.sprite = GetHappyFace(); // İlk yüz ifadesi mutlu
        }

        

        //StartCoroutine(WaitingCountdown());
    }

    void Update()
    {
        WaitingCountdown();
    }

    public void PlayEnteringAnimation(Action onComplete)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { onComplete?.Invoke(); });
    }

    public void PlayExitingAnimation(bool isHappy, Action onComplete)
    {
        waitingBarGO.SetActive(false);
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

    private void WaitingCountdown()
    {
        if(isWaiting && remainingWaitingTime > 0)
        {
            remainingWaitingTime-= Time.deltaTime;

            if (waitingBar != null)
            {
                waitingBar.fillAmount = remainingWaitingTime / maxWaitingTime;

                // Renk güncellemesi
                float percentage = remainingWaitingTime / maxWaitingTime;
                if (percentage > 0.5f)
                {
                    waitingBar.color = Color.green; // %50'den fazla süre kaldıysa yeşil
                    if (face != null) face.sprite = GetHappyFace(); // Mutlu yüz ifadesi
                }
                else if (percentage > 0.2f)
                {
                    waitingBar.color = Color.yellow; // %20-%50 arası süre kaldıysa sarı
                    if (face != null) face.sprite = GetNeutralFace(); // Bıkkın yüz ifadesi
                }
                else
                {
                    waitingBar.color = Color.red; // %20'den az süre kaldıysa kırmızı
                    if (face != null) face.sprite = GetAngryFace(); // Sinirli yüz ifadesi
                }
            }
        }

        if (isWaiting && remainingWaitingTime <= 0)
        {
            isWaiting = false;
            waitingBarGO.SetActive(false);
            GameManager.Instance(false).RemoveWaitedCustomer(this);
        }
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
    