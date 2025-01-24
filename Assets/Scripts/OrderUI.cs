using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    private Order order;



    public void UpdateVisual()
    {
        for (int i = 0; i < order.GetAllIngredients().Length; i++)
        {
            TextMeshProUGUI childText = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            childText.text = order.GetAllIngredients()[i].itemName;
        }
    }
}
