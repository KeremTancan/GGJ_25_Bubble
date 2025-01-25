using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;
using DG.Tweening;

public class Customer: MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;
    
    
    
    //static int customerCount = 0;
    Order order;
    
    SkeletonAnimation skeletonAnimation;
    CustomerAnimation customerAnimation;

    void Awake()
    {
        //name = "Customer" + (customerCount == 0 ? "" : customerCount);
        //customerCount++;
        name = "Customer";
        GetOrder();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        customerAnimation = GetComponent<CustomerAnimation>();
        PlayEnteringAnimation(() =>
        {
            OnAnyCustomerGenerated?.Invoke(this);
        });
    }

    public void PlayEnteringAnimation(Action onComplete)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(()=>{onComplete?.Invoke();});
    }

    public void PlayExitingAnimation(bool isHappy, Action onComplete)
    {
        BeHappy(() =>
        {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { onComplete?.Invoke(); });
        });
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
}
