using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer: MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;
    //static int customerCount = 0;
    Order order;

    void Awake()
    {
        //name = "Customer" + (customerCount == 0 ? "" : customerCount);
        //customerCount++;
        name = "Customer";
        GetOrder();
        OnAnyCustomerGenerated?.Invoke(this);
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
