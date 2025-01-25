using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] List<Customer> customers = new List<Customer>();
    private void OnEnable()
    {
        GenerateCustomer();
    }

    public void GenerateCustomer()
    {
        GameObject customerGO = new GameObject();
        var customer = customerGO.AddComponent<Customer>();
        customers.Add(customer);
    }
}