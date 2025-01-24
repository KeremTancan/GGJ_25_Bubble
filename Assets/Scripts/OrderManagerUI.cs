using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManagerUI : MonoSingleton<OrderManagerUI>
{
    [SerializeField] GameObject orderUIPrefab;

    
    protected override void Awake()
    {
        base.Awake();
        OrderManager.Instance.OnOrderGenerated += OrderManager_OnOrderGenerated;
    }

    void OrderManager_OnOrderGenerated(Order order)
    {
        Debug.Log(order);
    }
}
