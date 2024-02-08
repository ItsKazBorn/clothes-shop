using UnityEngine;
using Zenject;

public class CharacterLayer : MonoBehaviour
{
    [Inject] private SpriteSheetManager m_spriteSheetManager;
    
    [SerializeField] private string m_baseSpriteSheetName;
    public int SortingLayer { get; private set; }

    protected ISpriteHaver m_spriteHaver;
    
    private SpriteSwapper m_spriteSwapper;
    private bool initialized = false;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_spriteSwapper = new SpriteSwapper(m_spriteHaver, m_spriteSheetManager);
        if (!string.IsNullOrEmpty(m_baseSpriteSheetName)) m_spriteSwapper.SetSpriteSheet(m_baseSpriteSheetName);
        else m_spriteHaver.SetSpriteEnabled(false);
        
        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized || !m_spriteHaver.IsSpriteEnabled()) return;
        m_spriteSwapper.LateUpdate();
    }
    
}
