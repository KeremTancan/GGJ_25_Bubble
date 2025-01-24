using System;

[Serializable]
public struct Order
{
    public Item MilkType;
    public Item TeaType;
    public Item SugarType;
    public Item SyrupType;
    public Item BottleType;
    public Item CookieType;

    public Item[] GetAllIngredients()
    {
        return new Item[] {MilkType, TeaType, SugarType, SyrupType, BottleType, CookieType };
    }

    public override string ToString()
    {
        return $"Milk: {MilkType}, Tea: {TeaType}, Sugar: {SugarType}, Syrup: {SyrupType}, Bottle: {BottleType}, Cookie: {CookieType}";
    }
}