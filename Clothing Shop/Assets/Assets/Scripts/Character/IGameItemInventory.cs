using System.Collections.Generic;

public interface IGameItemInventory
{
    public Dictionary<int, GameItem> Inventory { get; }

    public void InitializeInventory();
    
    public void AddItem(GameItem item);
    public void AddItem(int itemID);

    public void RemoveItem(GameItem item);
    public void RemoveItem(int itemID);
    
}