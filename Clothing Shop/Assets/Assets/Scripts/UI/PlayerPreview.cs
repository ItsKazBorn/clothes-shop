using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerPreview : MonoBehaviour
{
    [Inject] private SpriteSheetManager m_spriteSheetManager;
    
    [SerializeField] private List<PlayerPreviewLayer> m_layers;
    
    private GameItem m_selectedItem;
    
    private const string m_charCode = "a";
    private const int m_charPage = 1;

    
    public void OnItemSelected(GameItem item)
    {
        m_selectedItem = item;
        
        UpdateSpriteSheets();
    }

    public void ClearItem()
    {
        m_selectedItem = null;
        UpdateSpriteSheets();
    }
    
    private void UpdateSpriteSheets()
    {
        foreach (PlayerPreviewLayer layer in m_layers)
        {
            if (m_selectedItem != null && m_selectedItem.Slot.Equals(layer.Slot))
            {
                layer.SetNewSpriteSheet(m_spriteSheetManager.GetSpriteSheetName(m_charCode, m_charPage, m_selectedItem));
                layer.SetLayerEnabled(true);
            }
            else
            {
                if (layer.Slot != ItemSlot.BASE) layer.SetLayerEnabled(false);
            }
        }
    }
    
}
