using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

public class SpriteSheetManager : IInitializable, IDisposable
{
    [Inject] private readonly Settings m_settings;
    private Dictionary<string, Dictionary<string, Sprite>> m_spriteSheets;
    private Dictionary<string, Sprite> m_icons;

    private const string m_basePath = "char_{0}_p{1}_{2}{3}_{4}";
    private const string m_baseIconPath = "char_{0}_p{1}_{2}{3}_{4}_icon";
    private const string m_baseCode = "bas";
    private const string m_outfitCode = "out";
    private const string m_hairCode = "har";
    private const string m_hatCode = "hat";
    private const string m_defCharCode = "a";
    private const int m_defPage = 1;
    
    public void Initialize()
    {
        m_spriteSheets = new Dictionary<string, Dictionary<string, Sprite>>();
        m_icons = new Dictionary<string, Sprite>();
        LoadAllSheets();
    }

    public void Dispose()
    {
        Debug.Log("Sprite Sheet Manager Disposed");
    }

    private void LoadAllSheets()
    {
        string sheetPath;
        string iconPath;
        Dictionary<string, Sprite> sheet;
        Sprite icon;

        foreach (CharSpriteSheet charCode in m_settings.CharSpriteSheetCodes)
        {
            for (int i = 0; i < charCode.Pages; i++)
            {
                foreach (CharLayer layer in charCode.CharLayers)
                {
                    foreach (CharLayerSpriteSheet sheetCode in layer.SpriteSheetCodes)
                    {
                        sheetPath = string.Format(m_basePath, charCode.Code, i+1, layer.Sorting, layer.Code, sheetCode.Code);
                        sheet = LoadSpriteSheet(sheetPath);
                        if (!sheet.IsEmpty()) m_spriteSheets.Add(sheetPath, sheet);
                        
                        iconPath = string.Format(m_baseIconPath, charCode.Code, i+1, layer.Sorting, layer.Code, sheetCode.Code);
                        icon = Resources.Load<Sprite>(iconPath);
                        if (icon != null) m_icons.Add(iconPath, icon);
                    }
                }
            }
        }
    }

    private Dictionary<string, Sprite> LoadSpriteSheet(string sheetName)
    {
        if (string.IsNullOrEmpty(sheetName)) return null;
        
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        var sprites = Resources.LoadAll<Sprite>(sheetName);
        return sprites.ToDictionary(x => x.name, x => x);
    }

    public Dictionary<string, Sprite> GetSpriteSheet(string sheetName)
    {
        return m_spriteSheets.ContainsKey(sheetName) ? m_spriteSheets[sheetName] : null;
    }

    private string GetItemLayerCode(ItemSlot slot)
    {
        switch (slot)
        {
            case ItemSlot.OUTFIT:
                return m_outfitCode;
            case ItemSlot.HAIR:
                return m_hairCode;
            case ItemSlot.HAT:
                return m_hatCode;
            default:
                return m_baseCode;
        }
    }
    
    private int GetItemLayerSort(ItemSlot slot)
    {
        switch (slot)
        {
            case ItemSlot.OUTFIT:
                return 1;
            case ItemSlot.HAIR:
                return 4;
            case ItemSlot.HAT:
                return 5;
            default:
                return 0;
        }
    }

    public Dictionary<string, Sprite> GetSpriteSheet(string charCode, int page, ItemSlot slot, string itemCode)
    {
        return GetSpriteSheet(string.Format(m_basePath, charCode, page, GetItemLayerSort(slot), GetItemLayerCode(slot), itemCode));
    }
    
    public Dictionary<string, Sprite> GetSpriteSheet(string charCode, int page, GameItem item)
    {
        return GetSpriteSheet(string.Format(m_basePath, charCode, page, GetItemLayerSort(item.Slot), GetItemLayerCode(item.Slot), item.Code));
    }
    public string GetSpriteSheetName(string charCode, int page, GameItem item)
    {
        return string.Format(m_basePath, charCode, page, GetItemLayerSort(item.Slot), GetItemLayerCode(item.Slot), item.Code);
    }

    public Sprite GetItemIcon(string charCode, int page, ItemSlot slot, string itemCode)
    {
        return GetItemIcon(string.Format(m_baseIconPath, charCode, page, GetItemLayerSort(slot), GetItemLayerCode(slot), itemCode));
    }

    public Sprite GetItemIcon(ItemSlot slot, string itemCode)
    {
        return GetItemIcon(string.Format(m_baseIconPath, m_defCharCode, m_defPage, GetItemLayerSort(slot), GetItemLayerCode(slot), itemCode));
    }
    
    public Sprite GetItemIcon(string iconName)
    {
        return m_icons.ContainsKey(iconName) ? m_icons[iconName] : null;
    }
    
    

    [Serializable]
    public struct CharSpriteSheet
    {
        public string Code;
        public int Pages;
        public CharLayer[] CharLayers;
    }
    
    [Serializable]
    public struct CharLayer
    {
        public string Code;
        public int Sorting;
        public CharLayerSpriteSheet[] SpriteSheetCodes;
    }
    
    [Serializable]
    public struct CharLayerSpriteSheet
    {
        public string Code;
    }
    
    [Serializable]
    public class Settings
    {
        public CharSpriteSheet[] CharSpriteSheetCodes;
    }
}
