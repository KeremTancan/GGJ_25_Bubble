using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaHitThreshold : MonoBehaviour
{
    private Image levelImage;
    private void Awake()
    {
        levelImage = GetComponent<Image>();
    }
    void Start()
    {
        levelImage.alphaHitTestMinimumThreshold = 0.1f;
    }
}
