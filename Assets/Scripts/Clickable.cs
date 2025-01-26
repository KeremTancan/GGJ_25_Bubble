using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{
    void Start()
    {
        var buttons = GetComponents<Button>();
        if (buttons == null || buttons.Length == 0)
        {
            return;
        }

        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(Click);
        }
    }
    private List<GameObject> panels;
    public void Click()
    {
        if (panels == null || panels.Count == 0)
        {
            panels = TogglePanel.Panels();
        }
        
        foreach (var pnl in panels)
        {
            pnl.SetActive(false);
        }
    }
}
