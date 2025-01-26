using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    private Customer customer;
    private Order order;

    
    
    public Customer GetCustomer()
    {
        return customer;
    }

    public void SetCustomer(Customer customer)
    {
        this.customer = customer;
        SetOrder(customer.GetOrder());
        UpdateVisual();
    }

    
    public Order GetOrder()
    {
        return order;
    }

    void SetOrder(Order order)
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
