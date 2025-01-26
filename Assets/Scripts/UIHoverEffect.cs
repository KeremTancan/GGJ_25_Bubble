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

    private static GameObject hoverTextGO;
    private IngredientButton ingredientButton;
    private TextMeshProUGUI hoverText;
    
    
    
    
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
        
        if (TryGetComponent<IngredientButton>(out ingredientButton))
        {
            OnHoverEnter.AddListener(PopUpTextHoverEnter);
            OnHoverExit.AddListener(PopUpTextHoverExit); 
        }
        
        if (hoverTextGO!=null)
        {
            hoverTextGO.SetActive(false);
        }
        else
        {
            hoverTextGO = GameObject.Find("HoverText");
            if (hoverTextGO==null)
            {
                Debug.LogError("BULAMADIM");
                return;
            }
        }
        hoverText = hoverTextGO.GetComponentInChildren<TextMeshProUGUI>();
        
    }

    void OnDestroy()
    {
        OnHoverEnter.RemoveAllListeners();
        OnHoverExit.RemoveAllListeners();
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
    
    public void PopUpTextWithStringHoverEnter(string message)
    {
        if (hoverTextGO == null || hoverText == null)
        {
            return;
        }
        hoverTextGO.SetActive(true);    
        hoverText.text = message;
    }

    public void PopUpTextHoverEnter()
    {
        if (hoverTextGO == null || ingredientButton == null || hoverText == null)
        {
            return;
        }
        hoverTextGO.SetActive(true);   
        hoverText.text = ingredientButton.Ingredient.ingredientName;
    }

    public void PopUpTextHoverExit()
    {
        if (hoverTextGO == null)
        {
            return;
        }
        hoverText.text = "";
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