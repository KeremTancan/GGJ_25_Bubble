using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;

public class Customer: MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;
    
    
    
    //static int customerCount = 0;
    Order order;
    
    SkeletonAnimation skeletonAnimation;

    void Awake()
    {
        //name = "Customer" + (customerCount == 0 ? "" : customerCount);
        //customerCount++;
        name = "Customer";
        GetOrder();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        OnAnyCustomerGenerated?.Invoke(this);
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
