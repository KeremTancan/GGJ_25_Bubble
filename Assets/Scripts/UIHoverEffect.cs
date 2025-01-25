using UnityEngine;
using UnityEngine.EventSystems; // Required for handling UI events
using DG.Tweening; // Import DoTween namespace

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f; // Scale to increase to when hovered
    public float duration = 0.2f;  // Duration of the scale animation
    private Vector3 originalScale; // Store the original scale of the UI element
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Save the original scale of the UI element
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up the UI element when the mouse hovers over it
        rectTransform.DOScale(originalScale * hoverScale, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale back to the original size when the mouse stops hovering
        rectTransform.DOScale(originalScale, duration).SetEase(Ease.InBack);
    }
}