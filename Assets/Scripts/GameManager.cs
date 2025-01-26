using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] List<Customer> customers; 
    [SerializeField] List<Transform> spawnPoints;
    
    [SerializeField] float spawnInterval;
    
    private Dictionary<Vector3, bool> spawnPointAvailability;
    [SerializeField] Customer customerPrefab;
    
    public Action<Customer> OnCustomerOrderMade;
    public Action<Order> OnAddedIngredientToDrink;
    
    [SerializeField] bool testOrderFinish = false;
    [SerializeField] Order order;
    
    [SerializeField] Ingredient noIceSO;
    
    public Order GetOrder { get { return order; } }
    protected override void Awake()
    {
        base.Awake();
        spawnPointAvailability = new Dictionary<Vector3, bool>();
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPointAvailability.Add(spawnPoint.position, true); // All spawn points are available initially
        }
        ClearOrder();
    }
    private void OnEnable()
    {
        GenerateCustomer();
    }

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            if (GenerateCustomer())
            {
            }
        }
    }

    
    private Vector3 GetFirstAvailableSpawnPoint()
    {
        
        foreach (var entry in spawnPointAvailability)
        {
            if (entry.Value) // If the spawn point is available
            {
                return entry.Key;
            }
        }

        return Vector3.negativeInfinity; // No available spawn points
    }

    private void FreeSpawnPoint(Transform spawnPoint)
    {
        if (spawnPointAvailability.ContainsKey(spawnPoint.position))
        {
            spawnPointAvailability[spawnPoint.position] = true; // Mark as available
            Debug.Log($"Spawn point at {spawnPoint.position} is now free.");
        }
    }


    void OnDestroy()
    {
        //customers.Clear();
    }
    
    public bool GenerateCustomer()
{
    Vector3 availableSpawnPoint = GetFirstAvailableSpawnPoint();

    if (availableSpawnPoint != Vector3.negativeInfinity && customers != null && customers.FindAll(e => e != null).Count < 3)
    {
        // Instantiate the customer at the available spawn point
        Customer newCustomer = Instantiate(customerPrefab, availableSpawnPoint, Quaternion.identity);
        customers.Add(newCustomer);
        return true;
    }
    else
    {
        Debug.LogWarning("No available spawn points!");
        return false;
    }
}


    public void FinishTestOrder()
    {
        if (CheckOrder(order, out Customer finishedCustomer))
        {
            RemoveFinishedCustomer(finishedCustomer);
        }
        else
        {
            foreach (var _cust in customers)
            {
                _cust.BeAngry(() =>
                {
                    _cust.AddIdleAnimation();
                });
            }
        }
        
        //ClearOrder();
        Debug.Log("Test order finish triggered!");
    }
    void RemoveFinishedCustomer(Customer finishedCustomer)
    {
        OnCustomerOrderMade?.Invoke(finishedCustomer);
        ClearOrder();
        finishedCustomer.PlayExitingAnimation(true,()=>{
            //customers.Remove(finishedCustomer);
            FreeSpawnPoint(finishedCustomer.transform);
            Destroy(finishedCustomer.gameObject);});
    }

    
    public void RemoveWaitedCustomer(Customer waitedCustomer)
    {
        OnCustomerOrderMade?.Invoke(waitedCustomer); 
        
        FreeSpawnPoint(waitedCustomer.transform); 

        waitedCustomer.PlayExitingAnimation(false, () =>
        {
            Destroy(waitedCustomer.gameObject); 
        });

        customers.Remove(waitedCustomer);
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
            case IngredientType.Bottle:
                if (order.BottleType == null)
                {
                    order.BottleType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.BottleType.ingredientName}. Cannot change it.");
                }
                break;
            
            case IngredientType.Tapioca:
                if (order.TapiocaType == null && order.BottleType != null)
                {
                    order.TapiocaType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.TapiocaType.ingredientName}. Cannot change it.");
                }
                break;
            
            case IngredientType.Ice:
                if ((order.IceType == noIceSO || order.IceType == null) && order.BottleType != null)
                {
                    order.IceType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.IceType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Milk:
                if (order.MilkType == null && order.BottleType != null)
                {
                    order.MilkType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.MilkType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Tea_Coffee:
                if (order.TeaType == null && order.BottleType != null)
                {
                    order.TeaType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.TeaType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Sugar:
                if (order.SugarType == null && order.BottleType != null)
                {
                    order.SugarType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.SugarType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Syrup:
                if (order.SyrupType == null && order.BottleType != null)
                {
                    order.SyrupType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.SyrupType.ingredientName}. Cannot change it.");
                }
                break;

            case IngredientType.Cookie:
                if (order.CookieType == null && order.BottleType != null)
                {
                    order.CookieType = ingredient;
                    Debug.Log($"Set {ingredient.ingredientName} to {type}");
                }
                else
                {
                    //Debug.LogWarning($"{type} already set to {order.CookieType.ingredientName}. Cannot change it.");
                }
                break;

            default:
                Debug.LogError("Unknown ingredient type.");
                break;
        }
        
        OnAddedIngredientToDrink?.Invoke(order);
    }
    
    

    [ContextMenu("Clear Order")]
    public void ClearOrder()
    {
        order.TapiocaType = null;
        order.IceType = noIceSO;
        order.MilkType = null;
        order.TeaType = null;
        order.SugarType = null;
        order.SyrupType = null;
        order.BottleType = null;
        order.CookieType = null;
        OnAddedIngredientToDrink?.Invoke(order);
        Debug.Log("Order has been cleared. All ingredients are now null.");
    }

    
}
