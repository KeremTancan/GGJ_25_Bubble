using UnityEngine;
using UnityEngine.EventSystems; // Required for handling UI events
using DG.Tweening;
using TMPro;
using UnityEngine.UI; // Import DoTween namespace
using UnityEngine.Events;

public class UIHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f; // Scale to increase to when hovered
    public float duration = 0.2f;  // Duration of the scale animation
    //private Vector3 originalScale; // Store the original scale of the UI element
    private RectTransform rectTransform;
    private float originalMinWidth; 
    private float originalMinHeight; 
    private LayoutElement layoutElement;

    private GameObject hoverTextGO;
    
    
    
    
    [SerializeField] UnityEvent OnHoverEnter;
    [SerializeField] UnityEvent OnHoverExit;

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

        hoverTextGO = GameObject.Find("HoverText");
        if (hoverTextGO!=null)
        {
            hoverTextGO.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
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

    public void PopUpTextHoverEnter()
    {
        if (hoverTextGO == null)
        {
            return;
        }
        hoverTextGO.SetActive(true);    
        var ingredientButton = GetComponent<IngredientButton>();
        var hoverText = hoverTextGO.GetComponentInChildren<TextMeshProUGUI>();
        if (ingredientButton != null && hoverText != null)
        {
            hoverText.text = ingredientButton.Ingredient.ingredientName;
        }
        else
        {
            hoverTextGO.SetActive(false);
        }
    }

    public void PopUpTextHoverExit()
    {
        if (hoverTextGO == null)
        {
            return;
        }
        hoverTextGO.SetActive(false);
    }
    



    public void ScaleHoverEnter()
    {
        // Calculate the target width
        float targetWidth = originalMinWidth * hoverScale;
        // Animate the preferredWidth property
        DOTween.To(() => layoutElement.minWidth, x => layoutElement.minWidth = x, targetWidth, duration)
            .SetEase(Ease.OutBack);
        
        float targetHeight = originalMinHeight * hoverScale;
        DOTween.To(() => layoutElement.minHeight, x => layoutElement.minHeight = x, targetHeight, duration).SetEase(Ease.OutBack);

    }

    public void ScaleHoverExit()
    {
        // Animate the preferredWidth property back to its original value
        DOTween.To(() => layoutElement.minWidth, x => layoutElement.minWidth = x, originalMinWidth, duration)
            .SetEase(Ease.InBack);
        DOTween.To(() => layoutElement.minHeight, x => layoutElement.minHeight = x, originalMinHeight, duration).SetEase(Ease.InBack);

    }
    
    
}