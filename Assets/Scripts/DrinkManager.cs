using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkManager : MonoSingleton<DrinkManager>
{
    [SerializeField] Image[] ingredientImages;
    Order order;

    void OnEnable()
    {
        GameManager.Instance().OnAddedIngredientToDrink += GameManager_OnAddedIngredientToDrink;
    }

    void OnDisable()
    {
        if (GameManager.Instance() != null)
        {
            GameManager.Instance().OnAddedIngredientToDrink -= GameManager_OnAddedIngredientToDrink;
        }
    }

    void GameManager_OnAddedIngredientToDrink(Order order)
    {
        var ingredients = order.GetAllIngredients();
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i]!=null && ingredients[i].drinkSprite != null)
            {
                ingredientImages[i].sprite = ingredients[i].drinkSprite;
                ingredientImages[i].color = Color.white;
            }
            else
            {
                ingredientImages[i].sprite = null;
                ingredientImages[i].color = Color.clear;
            }
        }
    }
}
