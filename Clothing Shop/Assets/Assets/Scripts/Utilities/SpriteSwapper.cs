using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper
{
    private SpriteSheetManager m_spriteSheetManager;
    
    private ISpriteHaver m_spriteRenderer;
    private string m_spriteSheetName;
    private string m_loadedSpriteSheetName;
    private Dictionary<string, Sprite> m_spriteSheet;

    public SpriteSwapper(ISpriteHaver spriteRenderer, SpriteSheetManager spriteSheetManager)
    {
        m_spriteSheetManager = spriteSheetManager;
        m_spriteRenderer = spriteRenderer;
        m_spriteSheet = new Dictionary<string, Sprite>();
    }

    public void LateUpdate()
    {
        if (string.IsNullOrEmpty(m_spriteSheetName)) return;
        
        if (m_loadedSpriteSheetName != m_spriteSheetName)
        {
            LoadSpriteSheet();
        }

        // Important: The name of the sprite must be the same!
        if (m_spriteRenderer.GetSprite() != m_spriteSheet[m_spriteRenderer.GetSpriteName()])
        {
            m_spriteRenderer.SetSprite(m_spriteSheet[m_spriteRenderer.GetSpriteName()]);
        }
    }
    
    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        if (string.IsNullOrEmpty(m_spriteSheetName)) return;
        
        Dictionary<string, Sprite> newSheet = m_spriteSheetManager.GetSpriteSheet(m_spriteSheetName);
        if (newSheet == null) return;
        m_spriteSheet = newSheet;
        
        // Remember the name of the sprite sheet in case it is changed later
        m_loadedSpriteSheetName = m_spriteSheetName;
    }

    public void SetSpriteSheet(string newSpriteSheetName)
    {
        m_spriteSheetName = newSpriteSheetName;
    }
}
