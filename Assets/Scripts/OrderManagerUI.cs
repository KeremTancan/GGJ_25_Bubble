using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManagerUI : MonoSingleton<OrderManagerUI>
{
    [SerializeField] GameObject orderUIPrefab;
    [SerializeField] Transform orderGroup;
    [SerializeField] Transform orderParent;
    [SerializeField] Button orderParentToggleBtn;

    
    protected override void Awake()
    {
        base.Awake();
        OrderManager.Instance.OnOrderGenerated += OrderManager_OnOrderGenerated;
        orderParentToggleBtn.onClick.AddListener(OrderParentToggle);
    }

    void OrderManager_OnOrderGenerated(Order order)
    {
        Debug.Log(order);
        var orderUIGO = Instantiate(orderUIPrefab, orderGroup);
        orderUIGO.GetComponent<OrderUI>().SetOrder(order);

    }

    
    void OrderParentToggle()
    {
        orderParent.gameObject.SetActive(!orderParent.gameObject.activeSelf);
    }
}
