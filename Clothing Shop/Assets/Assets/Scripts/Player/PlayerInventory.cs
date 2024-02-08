using System;
using System.Collections.Generic;
using Zenject;

public class PlayerInventory : IInitializable, IDisposable, IGameItemInventory
{
    [Inject] private readonly SignalBus m_signalBus;
    
    public Dictionary<int, GameItem> Inventory { get; private set; }

    public void Initialize()
    {
        InitializeInventory();
        
        m_signalBus.Subscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Subscribe<OnGameItemSoldSignal>(SoldItem);
    }
    
    public void Dispose()
    {
        m_signalBus.Unsubscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Unsubscribe<OnGameItemSoldSignal>(SoldItem);
    }
    
    public void InitializeInventory()
    {
        Inventory = new Dictionary<int, GameItem>();
    }
    
    private void BoughtItem(OnGameItemPurchasedSignal args)
    {
        AddItem(args.Item);
    }
    
    private void SoldItem(OnGameItemSoldSignal args)
    {
        RemoveItem(args.Item);
    }

    public void AddItem(GameItem item)
    {
        Inventory.Add(item.ItemID, item);
    }

    public void AddItem(int itemID)
    {
        // Needs Implementation
    }

    public void RemoveItem(GameItem item)
    {
        Inventory.Remove(item.ItemID);
    }

    public void RemoveItem(int itemID)
    {
        Inventory.Remove(itemID);
    }

    
}
