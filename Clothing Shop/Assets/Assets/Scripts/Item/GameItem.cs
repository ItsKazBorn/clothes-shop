
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSlot
{
    OUTFIT,
    HAIR,
    HAT,
}

public class GameItem
{
    private static int nextId;
    public int ItemID { get; private set; }
    
    private string m_name;
    private int m_buyValue;
    private int m_sellValue;
    private string m_code;
    private int m_paperDollLayer;
    private Sprite m_icon;

    public string Name => m_name;
    public Sprite Icon => m_icon;
    public int BuyValue => m_buyValue;
    public int SellValue => m_sellValue;
    public string Code => m_code;
    public int PaperDollLayer => m_paperDollLayer;


    public GameItem(string name, Sprite icon, int buyValue, int sellValue, string code, int paperDollLayer)
    {
        ItemID = nextId;
        nextId++;
        
        m_name = name;
        m_icon = icon;
        m_buyValue = buyValue;
        m_sellValue = sellValue;
        m_code = code;
        m_paperDollLayer = paperDollLayer;
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
