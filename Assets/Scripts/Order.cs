using System;

[Serializable]
public struct Order
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
}