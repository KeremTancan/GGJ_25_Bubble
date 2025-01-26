using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;
using DG.Tweening;
using UnityEngine.UI; // UI için gerekli

public class Customer : MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;

    public float maxWaitingTime = 22f; 
    private float remainingWaitingTime;
    private bool isWaiting = true;
    public int index = -1;

    public Image waitingBar; 

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
        if (waitingBar != null)
        {
            waitingBar.fillAmount = 1f; 
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
            }
        }

        if (remainingWaitingTime <= 0)
        {
            isWaiting = false;
            GameManager.Instance().RemoveWaitedCustomer(this);
            yield break;
        }
    }
}
