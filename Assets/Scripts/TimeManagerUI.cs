using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerUI : MonoSingleton<TimeManagerUI>
{
    [SerializeField] private GameObject[] customerWaitingBars;
    
    public GameObject[] GetCustomerWaitingBars()
    {
        return customerWaitingBars;
    }

   
}

