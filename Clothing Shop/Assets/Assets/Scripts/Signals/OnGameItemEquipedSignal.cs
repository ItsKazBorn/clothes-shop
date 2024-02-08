
public class OnGameItemEquipedSignal
{
    public GameItem Item { get; private set; }

    public OnGameItemEquipedSignal(GameItem item)
    {
        Item = item;
    }
}
