using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class GameItemManager : IInitializable
{
    [Inject] private Settings m_itemSettings;
    
    public Dictionary<int, GameItem> GameItems { get; private set; }
    public Dictionary<ItemSlot, Dictionary<string, GameItem>> AllGameItems { get; private set; }
    
    
    public void Initialize()
    {
        GameItems = new Dictionary<int, GameItem>();
        AllGameItems = new Dictionary<ItemSlot, Dictionary<string, GameItem>>();
        
        CreateAllItems();
    }

    private void CreateAllItems()
    {
        foreach (GameItemInfo itemInfo in m_itemSettings.ItemInfos)
        {
            GameItem item = new GameItem(itemInfo.Name, itemInfo.BuyValue, itemInfo.SellValue, itemInfo.Code,
                itemInfo.Slot);
            GameItems.Add(item.ItemID, item);
            
            if (!AllGameItems.ContainsKey(item.Slot)) AllGameItems.Add(item.Slot, new Dictionary<string, GameItem>());
            AllGameItems[item.Slot].Add(item.Code, item);
        }
    }

    public GameItem GetCopyOfItem(ItemSlot slot, string code)
    {
        if (!AllGameItems.ContainsKey(slot)) return null;
        if (!AllGameItems[slot].ContainsKey(code)) return null;
        GameItem item = AllGameItems[slot][code];
        return new GameItem(item.Name, item.BuyValue, item.SellValue, item.Code, item.Slot);
    }
    
    [Serializable]
    public class GameItemInfo
    {
        public string Name;
        public ItemSlot Slot;
        public string Code;
        public int BuyValue;
        public int SellValue;
    }

    [Serializable]
    public class Settings
    {
        public GameItemInfo[] ItemInfos;
    }
}
