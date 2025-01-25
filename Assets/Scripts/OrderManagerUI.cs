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
        Customer.OnAnyCustomerGenerated += Customer_OnAnyCustomerGenerated;
        orderParentToggleBtn.onClick.AddListener(OrderParentToggle);
    }

    void Customer_OnAnyCustomerGenerated(Customer customer)
    {
        var orderUIGO = Instantiate(orderUIPrefab, orderGroup);
        orderUIGO.GetComponent<OrderUI>().SetCustomer(customer);
    }

    
    void OrderParentToggle()
    {
        orderParent.gameObject.SetActive(!orderParent.gameObject.activeSelf);
    }
}
