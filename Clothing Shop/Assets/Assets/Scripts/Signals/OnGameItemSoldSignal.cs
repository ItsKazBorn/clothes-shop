
public class OnGameItemSoldSignal 
{
    public GameItem Item { get; private set; }

    public OnGameItemSoldSignal(GameItem item)
    {
        Item = item;
    }
}
