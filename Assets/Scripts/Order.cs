using System;

[Serializable]
public class Order
{
    public Ingredient TapiocaType;
    public Ingredient MilkType;
    public Ingredient TeaType;
    public Ingredient SugarType;
    public Ingredient SyrupType;
    public Ingredient BottleType;
    public Ingredient CookieType;

    public Ingredient[] GetAllIngredients()
    {
        return new Ingredient[] {TapiocaType, MilkType, TeaType, SugarType, SyrupType, BottleType, CookieType };
    }

    public override string ToString()
    {
        return $"Tapioca: {TapiocaType}, Milk: {MilkType}, Tea: {TeaType}, Sugar: {SugarType}, Syrup: {SyrupType}, Bottle: {BottleType}, Cookie: {CookieType}";
    }

    public  bool IsEqual(Order order2)
    {
        var order1Ingredients = GetAllIngredients();
        var order2Ingredients = order2.GetAllIngredients();
        for (int i = 0; i < order1Ingredients.Length; i++)
        {
            if (order1Ingredients[i].ingredientName != order2Ingredients[i].ingredientName)
            {
                return false;
            }
        }
        return true;
    }
}