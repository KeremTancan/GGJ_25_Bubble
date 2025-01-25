using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] List<Customer> customers; 
    public Action<Customer> OnCustomerOrderMade;
    
    [SerializeField] bool testOrderFinish = false;
    [SerializeField] Order order;
    private void OnEnable()
    {
        GenerateCustomer();
    }

    void OnDestroy()
    {
        //customers.Clear();
    }
    
    public void GenerateCustomer()
    {
        GameObject customerGO = new GameObject();
        var customer = customerGO.AddComponent<Customer>();
        customers.Add(customer);
    }

    void Update()
    {
        if (testOrderFinish)
        {
            testOrderFinish = false;
            if (CheckOrder(order, out Customer finishedCustomer))
            {
                OnCustomerOrderMade?.Invoke(finishedCustomer);
                RemoveFinishedCustomer(finishedCustomer);
            }
        }
    }

    void RemoveFinishedCustomer(Customer finishedCustomer)
    {
        customers.Remove(finishedCustomer);
        Destroy(finishedCustomer.gameObject);
    }

    bool CheckOrder(Order order, out Customer finishedCustomer)
    {
        bool isSuccess = false;
        finishedCustomer = null;
        
        foreach (var _cust in customers)
        {
            if (order.IsEqual(_cust.GetOrder()))
            {
                isSuccess = true;
                finishedCustomer = _cust;
                return isSuccess;
            }
        }
        Debug.Log(isSuccess + " " + (finishedCustomer !=null? finishedCustomer.name:"No Customers' order finished"));
        return isSuccess;
    }
    
    
}