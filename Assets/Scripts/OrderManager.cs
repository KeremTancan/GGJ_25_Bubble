using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoSingleton<OrderManager>
{
    public Action<Order> OnOrderGenerated;
    public List<Item> MilkTypes ;
    public List<Item> TeaTypes ;
    public List<Item> SugarTypes ;
    public List<Item> SyrupTypes ;
    public List<Item> BottleTypes ;
    public List<Item> CookieTypes;

    public Order GenerateRandomOrder()
    {
        Order newOrder = new Order
        {
            MilkType = GetRandomElement(MilkTypes),
            TeaType = GetRandomElement(TeaTypes),
            SugarType = GetRandomElement(SugarTypes),
            SyrupType = GetRandomElement(SyrupTypes),
            BottleType = GetRandomElement(BottleTypes),
            CookieType = GetRandomElement(CookieTypes)
        };

        Debug.Log($"New Order: {newOrder}");
        OnOrderGenerated?.Invoke(newOrder);
        return newOrder;
    }

    private Item GetRandomElement(List<Item> items)
    {
        return items[UnityEngine.Random.Range(0, items.Count)];
    }

    void OnEnable()
    {
        LoadIngredients();
    }
    private void LoadIngredients()
    {
        // Load all Ingredient ScriptableObjects from the Resources folder
        Item[] allIngredients = Resources.LoadAll<Item>("Items");

        foreach (var ingredient in allIngredients)
        {
            switch (ingredient.ingredientType)
            {
                //case IngredientType.Tapioca:
                  //  TapiocaIngredients.Add(ingredient);
                    //break;
                case IngredientType.Milk:
                    MilkTypes.Add(ingredient);
                    break;
                case IngredientType.Tea_Coffee:
                    TeaTypes.Add(ingredient);
                    break;
                case IngredientType.Sugar:
                    SugarTypes.Add(ingredient);
                    break;
                case IngredientType.Syrup:
                    SyrupTypes.Add(ingredient);
                    break;
                case IngredientType.Bottle:
                    BottleTypes.Add(ingredient);
                    break;
                case IngredientType.Cookie:
                    CookieTypes.Add(ingredient);
                    break;
            }
        }

        Debug.Log("Ingredients categorized successfully.");
    }
}