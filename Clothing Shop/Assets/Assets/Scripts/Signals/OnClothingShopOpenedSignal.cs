
public class OnClothingShopOpenedSignal
{
    public Shopkeeper Shop { get; private set; }
    
    public OnClothingShopOpenedSignal(Shopkeeper shop)
    {
        Shop = shop;
    }
}
