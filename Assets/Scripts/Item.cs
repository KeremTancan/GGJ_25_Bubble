using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public IngredientType ingredientType;
    public Sprite supplySprite;
    public Sprite drinkSprite;
}