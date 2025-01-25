using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer: MonoBehaviour
{
    public static Action<Customer> OnAnyCustomerGenerated;
    static int customerCount = 0;
    Order order;

    void Awake()
    {
        name = "Customer" + (customerCount == 0 ? "" : customerCount);
        customerCount++;
        GetOrder();
        OnAnyCustomerGenerated?.Invoke(this);
    }

    public Order GetOrder()
    {
        if (order == null)
        {
            order = OrderGenerator.Instance.GenerateRandomOrder();
        }
        return order; 
    }
}
