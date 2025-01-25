using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderGenerator : MonoSingleton<OrderGenerator>
{
    //public Action<Order> OnOrderGenerated;

    [SerializeField] List<Ingredient> TapiocaTypes;
    [SerializeField] List<Ingredient> MilkTypes ;
    [SerializeField] List<Ingredient> TeaTypes ;
    [SerializeField] List<Ingredient> SugarTypes ;
    [SerializeField] List<Ingredient> SyrupTypes ;
    [SerializeField] List<Ingredient> BottleTypes ;
    [SerializeField] List<Ingredient> CookieTypes;

    public Order GenerateRandomOrder()
    {
        Order newOrder = new Order
        {
            TapiocaType = GetRandomElement(TapiocaTypes),
            MilkType = GetRandomElement(MilkTypes),
            TeaType = GetRandomElement(TeaTypes),
            SugarType = GetRandomElement(SugarTypes),
            SyrupType = GetRandomElement(SyrupTypes),
            BottleType = GetRandomElement(BottleTypes),
            CookieType = GetRandomElement(CookieTypes)
        };

        Debug.Log($"New Order: {newOrder}");
        //OnOrderGenerated?.Invoke(newOrder);
        return newOrder;
    }

    Ingredient GetRandomElement(List<Ingredient> ingredients)
    {
        return ingredients[UnityEngine.Random.Range(0, ingredients.Count)];
    }

    void OnEnable()
    {
        LoadIngredients();
    }

    void ClearLists()
    {
        TapiocaTypes.Clear();
        MilkTypes.Clear();
        TeaTypes.Clear();
        SugarTypes.Clear();
        SyrupTypes.Clear();
        BottleTypes.Clear();
        CookieTypes.Clear();
    }
    private void LoadIngredients()
    {
        ClearLists();
        // Load all Ingredient ScriptableObjects from the Resources folder
        Ingredient[] allIngredients = Resources.LoadAll<Ingredient>("Ingredients");

        foreach (var ingredient in allIngredients)
        {
            switch (ingredient.ingredientType)
            {
                case IngredientType.Tapioca: 
                    TapiocaTypes.Add(ingredient);
                    break;
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