
using System;
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
    private ItemSlot m_slot;

    public string Name => m_name;
    public Sprite Icon => m_icon;
    public int BuyValue => m_buyValue;
    public int SellValue => m_sellValue;
    public string Code => m_code;
    public int PaperDollLayer => m_paperDollLayer;


    public GameItem(string name, int buyValue, int sellValue, string code, ItemSlot slot)
    {
        ItemID = nextId;
        nextId++;
        
        m_name = name;
        m_buyValue = buyValue;
        m_sellValue = sellValue;
        m_code = code;
        m_slot = slot;
    }

    
}
