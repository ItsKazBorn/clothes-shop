
public class OnShopItemSelectedSignal
{
    public GameItem Item { get; private set; }
    
    public OnShopItemSelectedSignal(GameItem item)
    {
        Item = item;
    }
}
