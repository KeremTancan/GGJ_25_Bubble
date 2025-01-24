using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    private void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        Order newOrder = OrderManager.Instance.GenerateRandomOrder();
        // Display the order in the UI or pass it to the next part of the game logic
    }
}