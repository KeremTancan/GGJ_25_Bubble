using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OrderManagerUI : MonoSingleton<OrderManagerUI>
{
    [SerializeField] GameObject orderUIPrefab;
    [SerializeField] Transform orderGroup;
    [SerializeField] RectTransform orderParent;
    Vector3 initPosOrderParent;
    [SerializeField] Button orderParentToggleBtn;
    
    [SerializeField] List<OrderUI> orderUIList = new List<OrderUI>();

    
    protected override void Awake()
    {
        base.Awake();
        initPosOrderParent = orderParent.localPosition;
        Customer.OnAnyCustomerGenerated += Customer_OnAnyCustomerGenerated;
        GameManager.Instance().OnCustomerOrderMade += GameManager_OnCustomerOrderMade;
        TogglePanel.OnTogglePanel += OnUIClicked;
        Clickable.OnClickableClicked += OnUIClicked;
        orderParentToggleBtn.onClick.AddListener(OrderParentToggle);
    }

    void OnDestroy()
    {
        Customer.OnAnyCustomerGenerated -= Customer_OnAnyCustomerGenerated;
        if (GameManager.Instance() != null)
        {
            GameManager.Instance().OnCustomerOrderMade -= GameManager_OnCustomerOrderMade;
        }
        TogglePanel.OnTogglePanel -= OnUIClicked;
        Clickable.OnClickableClicked += OnUIClicked;
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

    
    
    bool toggle = false;
    void OrderParentToggle()
    {
        toggle = !toggle;
        if (toggle)
        {
            orderParent.DOLocalMove(Vector3.zero, 0.5f);
            //
            TogglePanel.CloseAllPanels();
        }
        else
        {
            orderParent.DOLocalMove(initPosOrderParent, 0.5f);
        }
    }

    void OnUIClicked()
    {
        if (toggle)
        {
            OrderParentToggle();
        }
    }
}
