using UnityEngine;
using Zenject;

public class CharacterLayer : MonoBehaviour
{
    [Inject] private SpriteSheetManager m_spriteSheetManager;
    
    [SerializeField] private string m_baseSpriteSheetName;
    [SerializeField] private int LayerSort = 0;
    
    public ItemSlot Slot { get; private set; }

    protected ISpriteHaver m_spriteHaver;
    
    private SpriteSwapper m_spriteSwapper;
    private bool initialized = false;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Slot = SetSlot();
        
        m_spriteSwapper = new SpriteSwapper(m_spriteHaver, m_spriteSheetManager);

        initialized = true;
    }

    public void SetNewSpriteSheet(string newSheetName)
    {
        m_spriteSwapper.SetSpriteSheet(newSheetName);
    }

    private ItemSlot SetSlot()
    {
        switch (LayerSort)
        {
            case 1:
                return ItemSlot.OUTFIT;
            case 4:
                return ItemSlot.HAIR;
            case 5:
                return ItemSlot.HAT;
            default:
                return ItemSlot.BASE;
        }
    }

    public void SetLayerEnabled(bool enabled)
    {
        m_spriteHaver.SetSpriteEnabled(enabled);
    }

    private void LateUpdate()
    {
        if (!initialized) return;
        m_spriteSwapper.LateUpdate();
    }
    
}
