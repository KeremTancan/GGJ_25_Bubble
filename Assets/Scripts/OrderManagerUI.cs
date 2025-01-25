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
    
    [SerializeField] List<OrderUI> orderUIList = new List<OrderUI>();

    
    protected override void Awake()
    {
        base.Awake();
        Customer.OnAnyCustomerGenerated += Customer_OnAnyCustomerGenerated;
        GameManager.Instance().OnCustomerOrderMade += GameManager_OnCustomerOrderMade;
        orderParentToggleBtn.onClick.AddListener(OrderParentToggle);
    }

    void OnDestroy()
    {
        Customer.OnAnyCustomerGenerated -= Customer_OnAnyCustomerGenerated;
        if (GameManager.Instance() != null)
        {
            GameManager.Instance().OnCustomerOrderMade -= GameManager_OnCustomerOrderMade;
        }
    }
    
    void GameManager_OnCustomerOrderMade(Customer finishedCustomer)
    {
        OrderUI destroyOrderUI = null;
        foreach (OrderUI orderUI in orderUIList)
        {
            if (orderUI.GetCustomer() == finishedCustomer)
            {
                destroyOrderUI = orderUI;
                break;
            }
        }

        if (destroyOrderUI == null)
        {
            return;
        }
        orderUIList.Remove(destroyOrderUI);
        Destroy(destroyOrderUI.gameObject);
        
    }

    void Customer_OnAnyCustomerGenerated(Customer customer)
    {
        var orderUIGO = Instantiate(orderUIPrefab, orderGroup);
        var orderUI = orderUIGO.GetComponent<OrderUI>();
        orderUI.SetCustomer(customer);
        orderUIList.Add(orderUI);
    }

    
    void OrderParentToggle()
    {
        orderParent.gameObject.SetActive(!orderParent.gameObject.activeSelf);
    }
}
