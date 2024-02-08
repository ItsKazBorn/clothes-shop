

public class OnGameItemPurchasedSignal 
{
    public GameItem Item { get; private set; }

    public OnGameItemPurchasedSignal(GameItem item)
    {
        Item = item;
    }
}
