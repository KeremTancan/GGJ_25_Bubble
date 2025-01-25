using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;
    
    public void PanelToggle()
    {
        if (panel != null)  
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
