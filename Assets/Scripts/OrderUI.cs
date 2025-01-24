using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    private Order order;
    //getter setter
    public Order GetOrder()
    {
        return order;
    }

    public void SetOrder(Order order)
    {
        this.order = order;
        UpdateVisual();
    }
    



    public void UpdateVisual()
    {
        var ingredientsParent = transform.Find("Ingredients");
        for (int i = 0; i < order.GetAllIngredients().Length; i++)
        {
            TextMeshProUGUI childText = ingredientsParent.GetChild(i).GetComponent<TextMeshProUGUI>();
            childText.text = order.GetAllIngredients()[i].ingredientName;
        }
    }
}
