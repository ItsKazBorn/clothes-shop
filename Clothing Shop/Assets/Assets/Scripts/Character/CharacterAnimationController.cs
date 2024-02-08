using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterAnimationController : MonoBehaviour
{
    [Inject] private SignalBus m_signalBus;
    [Inject] private SpriteSheetManager m_spriteSheetManager;

    [SerializeField] private bool IsPlayer;
    [SerializeField] private List<CharacterSpriteLayerController> m_spriteLayerControllers;

    private Dictionary<ItemSlot, GameItem> m_equippedItems;
    
    private const string m_charCode = "a";
    private const int m_charPage = 1;
    
    private void Awake()
    {
        m_equippedItems = new Dictionary<ItemSlot, GameItem>();

        if (IsPlayer)
        {
            m_signalBus.Subscribe<OnGameItemEquipedSignal>(EquipItem);
            m_signalBus.Subscribe<OnGameItemSoldSignal>(UnequipItem);
        }
    }

    private void OnDestroy()
    {
        m_signalBus.TryUnsubscribe<OnGameItemEquipedSignal>(EquipItem);
        m_signalBus.TryUnsubscribe<OnGameItemSoldSignal>(UnequipItem);
    }

    private void EquipItem(OnGameItemEquipedSignal args)
    {
        EquipItem(args.Item);
    }

    private void UnequipItem(OnGameItemSoldSignal args)
    {
        UnequipItem(args.Item);
    }
    
    private void UnequipItem(GameItem item)
    {
        if (m_equippedItems.ContainsKey(item.Slot))
        {
            if (m_equippedItems[item.Slot].Code.Equals(item.Code))
            {
                m_equippedItems.Remove(item.Slot);
            }
        }
        UpdateSpriteSheets();
    }

    public void EquipItem(GameItem item)
    {
        if (m_equippedItems.ContainsKey(item.Slot)) m_equippedItems[item.Slot] = item;
        else m_equippedItems.Add(item.Slot, item);
        
        UpdateSpriteSheets();
    }

    private void UpdateSpriteSheets()
    {
        foreach (CharacterSpriteLayerController layer in m_spriteLayerControllers)
        {
            if (m_equippedItems.ContainsKey(layer.Slot))
            {
                layer.SetNewSpriteSheet(m_spriteSheetManager.GetSpriteSheetName(m_charCode, m_charPage, m_equippedItems[layer.Slot]));
                layer.SetLayerEnabled(true);
            }
            else
            {
                if (layer.Slot != ItemSlot.BASE) layer.SetLayerEnabled(false);
            }
        }
    }
    
    public void SetMovementParameters(Vector2 movement, float speed)
    {
        foreach (CharacterSpriteLayerController spriteLayer in m_spriteLayerControllers)
        {
            spriteLayer.SetMovementParameters(movement, speed);
        }
    }
}
