using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredients", order = 1)]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public IngredientType ingredientType;
    public Sprite supplySprite;
    public Sprite drinkSprite;
    
    private void OnValidate()
    {
        // Format the ScriptableObject name to a readable format
        ingredientName = FormatName(name);
    }

    private string FormatName(string rawName)
    {
        // Use regex to add spaces before capital letters and capitalize the first letter of the name
        string formattedName = Regex.Replace(rawName, "(\\B[A-Z])", " $1");
        return char.ToUpper(formattedName[0]) + formattedName.Substring(1);
    }
}