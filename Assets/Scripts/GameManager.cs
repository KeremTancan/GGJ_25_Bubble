using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] List<Customer> customers = new List<Customer>(); 
    
    
    [SerializeField] bool testOrderFinish = false;
    [SerializeField] Order order;
    private void OnEnable()
    {
        GenerateCustomer();
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
            FinishOrder(order);
        }
    }

    void FinishOrder(Order order)
    {
        

        bool isSuccess = false;
        Customer finishedCustomer = null;
        
        foreach (var _cust in customers)
        {
            if (order.IsEqual(_cust.GetOrder()))
            {
                isSuccess = true;
                finishedCustomer = _cust;
            }
        }
        
        Debug.Log(isSuccess + " " + finishedCustomer !=null? finishedCustomer.name:"No Customers' order finished");
    }
    
    
}