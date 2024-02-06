using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private string m_spriteSheetName;
    private string m_loadedSpriteSheetName;
    private Dictionary<string, Sprite> m_spriteSheet;

    private void Start()
    {
        m_spriteSheet = new Dictionary<string, Sprite>();
    }

    public void Setup(SpriteRenderer spriteRenderer)
    {
        m_spriteRenderer = spriteRenderer;
    }

    private void LateUpdate()
    {
        if (string.IsNullOrEmpty(m_spriteSheetName)) return;
        
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (m_loadedSpriteSheetName != m_spriteSheetName)
        {
            // Load the new sprite sheet
            LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        m_spriteRenderer.sprite = m_spriteSheet[m_spriteRenderer.sprite.name];
    }
    
    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        if (string.IsNullOrEmpty(m_spriteSheetName)) return;
        
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        var sprites = Resources.LoadAll<Sprite>(m_spriteSheetName);
        m_spriteSheet.Clear();
        m_spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later
        m_loadedSpriteSheetName = m_spriteSheetName;
    }

    public void SetSpriteSheet(string newSpriteSheetName)
    {
        m_spriteSheetName = newSpriteSheetName;
    }
}