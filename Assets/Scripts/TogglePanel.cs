using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;

    private static List<GameObject> panels;

    public static List<GameObject> Panels()
    {
        return panels;
    }

    void Awake()
    {
        if (panels == null || panels.Count == 0)
        {
            panels = new List<GameObject>();
            var togglePanels = GameObject.FindObjectsOfType<TogglePanel>();
            foreach (var tp in togglePanels)
            {
                panels.Add(tp.panel);
            }
        }
    }

    void OnDestroy()
    {
        if (panels != null && panels.Count != 0)
        {
            panels.Clear();
        }
    }
    
    public void PanelToggle()
    {
        if (panel == null)  
        {
            return;
        }
        
        bool active = panel.activeSelf;

        if (!active)
        {
            foreach (var pnl in panels)
            {
                pnl.SetActive(false);
            }
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
