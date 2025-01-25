using UnityEngine;
using UnityEngine.EventSystems; // Required for handling UI events
using DG.Tweening;
using UnityEngine.UI; // Import DoTween namespace

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f; // Scale to increase to when hovered
    public float duration = 0.2f;  // Duration of the scale animation
    //private Vector3 originalScale; // Store the original scale of the UI element
    private RectTransform rectTransform;
    private float originalMinWidth; 
    private float originalMinHeight; 
    private LayoutElement layoutElement;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (layoutElement == null)
        {
            layoutElement = gameObject.AddComponent<LayoutElement>();
        }
        layoutElement.minWidth = rectTransform.rect.width;
        layoutElement.minHeight = rectTransform.rect.height;
        
        originalMinWidth = layoutElement.minWidth;
        originalMinHeight = layoutElement.minHeight;
        
        //originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Calculate the target width
        float targetWidth = originalMinWidth * hoverScale;
        // Animate the preferredWidth property
        DOTween.To(() => layoutElement.minWidth, x => layoutElement.minWidth = x, targetWidth, duration)
            .SetEase(Ease.OutBack);
        
        float targetHeight = originalMinHeight * hoverScale;
        DOTween.To(() => layoutElement.minHeight, x => layoutElement.minHeight = x, targetHeight, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Animate the preferredWidth property back to its original value
        DOTween.To(() => layoutElement.minWidth, x => layoutElement.minWidth = x, originalMinWidth, duration)
            .SetEase(Ease.InBack);
        DOTween.To(() => layoutElement.minHeight, x => layoutElement.minHeight = x, originalMinHeight, duration).SetEase(Ease.InBack);
    }

    private void OnDisable()
    {
        if (layoutElement != null)
        {
            // Reset the preferred width to its original value
            layoutElement.minWidth = originalMinWidth;
            layoutElement.minHeight = originalMinHeight;
        }
    }
}