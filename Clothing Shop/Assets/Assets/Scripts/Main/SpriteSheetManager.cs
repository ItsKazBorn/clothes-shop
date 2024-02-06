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

    private const string m_basePath = "char_{0}_p{1}_{2}{3}_{4}_v{5}";
    private const string m_versionFormat = "00";
    
    public void Initialize()
    {
        m_spriteSheets = new Dictionary<string, Dictionary<string, Sprite>>();
        LoadAllSheets();
    }

    public void Dispose()
    {
        Debug.Log("Sprite Sheet Manager Disposed");
    }

    private void LoadAllSheets()
    {
        string sheetPath;
        Dictionary<string, Sprite> sheet;

        foreach (CharSpriteSheet charCode in m_settings.CharSpriteSheetCodes)
        {
            for (int i = 0; i < charCode.Pages; i++)
            {
                foreach (CharLayer layer in charCode.CharLayers)
                {
                    foreach (CharLayerSpriteSheet sheetCode in layer.SpriteSheetCodes)
                    {
                        sheetPath = string.Format(m_basePath, charCode.Code, i+1, layer.Sorting, layer.Code, sheetCode.Code,
                            sheetCode.Version.ToString(m_versionFormat));
                        sheet = LoadSpriteSheet(sheetPath);
                        if (!sheet.IsEmpty()) m_spriteSheets.Add(sheetPath, sheet);
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
        public int Version;
    }
    
    [Serializable]
    public class Settings
    {
        public CharSpriteSheet[] CharSpriteSheetCodes;
    }
}
