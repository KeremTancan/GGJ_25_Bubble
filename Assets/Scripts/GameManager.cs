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
        ClearOrder();
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
    
    public void SetIngredient(IngredientType type, Ingredient ingredient)
    {
        switch (type)
        {
            case IngredientType.Tapioca:
                if (order.TapiocaType == null)
                {
                    order.TapiocaType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.TapiocaType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Milk:
                if (order.MilkType == null)
                {
                    order.MilkType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.MilkType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Tea_Coffee:
                if (order.TeaType == null)
                {
                    order.TeaType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.TeaType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Sugar:
                if (order.SugarType == null)
                {
                    order.SugarType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.SugarType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Syrup:
                if (order.SyrupType == null)
                {
                    order.SyrupType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.SyrupType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Bottle:
                if (order.BottleType == null)
                {
                    order.BottleType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.BottleType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Cookie:
                if (order.CookieType == null)
                {
                    order.CookieType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    Debug.LogWarning($"{type} already set to {order.CookieType.ingredientName}. Cannot change it.");
                }
                break;

            default:
                Debug.LogError("Unknown ingredient type.");
                break;
        }
    }
    
    public void FinishTestOrder()
    {
        testOrderFinish = true;
        Debug.Log("Test order finish triggered!");
    }

    [ContextMenu("Clear Order")]
    public void ClearOrder()
    {
        order.TapiocaType = null;
        order.MilkType = null;
        order.TeaType = null;
        order.SugarType = null;
        order.SyrupType = null;
        order.BottleType = null;
        order.CookieType = null;

        Debug.Log("Order has been cleared. All ingredients are now null.");
    }

    
}