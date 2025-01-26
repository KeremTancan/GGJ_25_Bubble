using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;

public class TapiocaCooker : MonoBehaviour
{
    [SerializeField] private Button cookButton;  // This should be the same button as your IngredientButton
    [SerializeField] private Image cookingProgressBar;
    [SerializeField] private Image panImage;
    [SerializeField] private Sprite rawPanSprite;
    [SerializeField] private Sprite cookedPanSprite;
    [SerializeField] private float cookingTime = 5f;
    
    private bool isCooking = false;
    private bool isCooked = false;
    private float currentCookingTime = 0f;
    private IngredientButton tapiocaButton;

    private void Awake()
    {
        // First try to get IngredientButton from this GameObject
        tapiocaButton = GetComponent<IngredientButton>();

        // If not found, try to get it from the cookButton's GameObject
        if (tapiocaButton == null && cookButton != null)
        {
            tapiocaButton = cookButton.GetComponent<IngredientButton>();
        }

        // If still not found, look for it in parent
        if (tapiocaButton == null)
        {
            tapiocaButton = GetComponentInParent<IngredientButton>();
        }

        // If still not found, look for it in children
        if (tapiocaButton == null)
        {
            tapiocaButton = GetComponentInChildren<IngredientButton>();
        }

        // If cookButton wasn't assigned in inspector, try to get it
        if (cookButton == null)
        {
            cookButton = GetComponent<Button>();
        }

        Debug.Log($"TapiocaCooker initialized. Button reference: {cookButton != null}, IngredientButton reference: {tapiocaButton != null}");
        
        if (tapiocaButton == null)
        {
            Debug.LogError("Could not find IngredientButton component! Make sure it exists in the hierarchy.");
        }
    }

    private void Start()
    {
        // Initially hide the pan and progress bar
        if (panImage != null)
        {
            panImage.gameObject.SetActive(false);
        }
        if (cookingProgressBar != null)
        {
            cookingProgressBar.gameObject.SetActive(false);
        }

        // Set up the button click handler
        if (cookButton != null)
        {
            cookButton.onClick.RemoveAllListeners();
            cookButton.onClick.AddListener(HandleButtonClick);
            Debug.Log("Button click listener set up");
        }
        else
        {
            Debug.LogError("Cook button reference is missing!");
        }
    }

    private void HandleButtonClick()
    {
        Debug.Log($"Button clicked. IsCooking: {isCooking}, IsCooked: {isCooked}");
        
        if (!isCooking && !isCooked)
        {
            StartCooking();
        }
        else if (!isCooking && isCooked)
        {
            CollectTapioca();
        }
    }

    private void StartCooking()
    {
        Debug.Log("Starting cooking process");
        isCooking = true;
        isCooked = false;
        currentCookingTime = 0f;
        
        // Show pan and progress bar
        panImage.gameObject.SetActive(true);
        panImage.sprite = rawPanSprite;
        cookingProgressBar.gameObject.SetActive(true);
        cookingProgressBar.fillAmount = 0f;
        
        // Disable the button while cooking
        cookButton.interactable = false;

        // Animate the pan appearing
        panImage.transform.localScale = Vector3.zero;
        panImage.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
    }

    private void Update()
    {
        if (isCooking)
        {
            currentCookingTime += Time.deltaTime;
            float progress = currentCookingTime / cookingTime;
            float rotationAngle = progress * 360f; // Full rotation in degrees

            // Rotate the clock hand around its pivot point
            cookingProgressBar.transform.localRotation = Quaternion.Euler(0, 0, -rotationAngle);

            if (currentCookingTime >= cookingTime)
            {
                FinishCooking();
            }
        }
    }

    private void FinishCooking()
    {
        Debug.Log("Finishing cooking process");
        isCooking = false;
        isCooked = true;
        
        // Change pan sprite to cooked state
        panImage.sprite = cookedPanSprite;
        
        // Enable the button for collecting
        cookButton.interactable = true;
    }

    private void CollectTapioca()
    {
        Debug.Log("Collecting tapioca");
        // Add tapioca to the drink using the GameManager
        if (tapiocaButton != null && tapiocaButton.Ingredient != null)
        {
            Debug.Log($"Adding tapioca ingredient: {tapiocaButton.Ingredient.ingredientName}");
            GameManager.Instance(true).SetIngredient(IngredientType.Tapioca, tapiocaButton.Ingredient);
        }
        else
        {
            Debug.LogError("TapiocaButton or its Ingredient is null!");
            if (tapiocaButton == null) Debug.LogError("TapiocaButton component is null");
            if (tapiocaButton != null && tapiocaButton.Ingredient == null) Debug.LogError("Ingredient is null");
        }

        // Reset the cooking system
        ResetCooker();
    }

    private void ResetCooker()
    {
        Debug.Log("Resetting cooker");
        // Reset all states
        isCooking = false;
        isCooked = false;
        
        // Hide pan and progress bar
        panImage.gameObject.SetActive(false);
        cookingProgressBar.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Clean up listeners
        if (cookButton != null)
        {
            cookButton.onClick.RemoveAllListeners();
        }
    }
}