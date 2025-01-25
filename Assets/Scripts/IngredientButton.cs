using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour
{
    //[SerializeField] private IngredientType ingredientType; // Type of ingredient (Milk, Tapioca, etc.)
    [SerializeField] private Ingredient ingredient;         // ScriptableObject for the ingredient
    public Ingredient Ingredient { get { return ingredient; } }
    [SerializeField] private Button button;                // Reference to the button

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        // Add the button click listener
        button.onClick.AddListener(OnButtonClick);
        button.GetComponent<Image>().sprite = ingredient.supplySprite;
        button.GetComponentInChildren<TextMeshProUGUI>().text = ingredient.name;
    }

    private void OnButtonClick()
    {
        GameManager.Instance(true).SetIngredient(ingredient.ingredientType, ingredient); // Use the required argument
    }

}