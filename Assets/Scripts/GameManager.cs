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
        List<Order> orders = new List<Order>();
        
        foreach (var customer in customers)
        {
            if (customer.GetOrder() != null) // Check if the customer has an order
            {
                orders.Add(customer.GetOrder());
            }
        }

        bool isSuccess = false;
        
        foreach (var _order in orders)
        {
            if (order.IsEqual(_order))
            {
                isSuccess = true;
                
            }
        }
        
        Debug.Log(isSuccess);
    }
    
    
}