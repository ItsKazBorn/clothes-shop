using System;
using System.Collections.Generic;
using Zenject;

public class GameItemManager : IInitializable
{
    [Inject] private Settings m_itemSettings;
    
    public Dictionary<int, GameItem> GameItems { get; private set; }
    
    public void Initialize()
    {
        CreateAllItems();
    }

    private void CreateAllItems()
    {
        foreach (GameItemInfo itemInfo in m_itemSettings.ItemInfos)
        {
            GameItem item = new GameItem(itemInfo.Name, itemInfo.BuyValue, itemInfo.SellValue, itemInfo.Code,
                itemInfo.Slot);
            GameItems.Add(item.ItemID, item);
        }
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
