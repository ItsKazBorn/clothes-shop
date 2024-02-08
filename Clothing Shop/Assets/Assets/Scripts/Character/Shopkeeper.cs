using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shopkeeper : MonoBehaviour, IInteractible, IGameItemInventory
{
    [Inject] private SignalBus m_signalBus;
    [Inject] private GameItemManager m_itemManager;
    
    [SerializeField] private string m_prompt;
    [SerializeField] private CharacterAnimationController m_animationController;

    [SerializeField] private string m_outfitCode;
    [SerializeField] private string m_hairCode;
    

    public Dictionary<int, GameItem> Inventory { get; private set; }
    public string InteractionPrompt => m_prompt;

    private void Start()
    {
        m_signalBus.Subscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Subscribe<OnGameItemSoldSignal>(SoldItem);
        
        m_animationController.EquipItem(m_itemManager.GetCopyOfItem(ItemSlot.OUTFIT, m_outfitCode));
        m_animationController.EquipItem(m_itemManager.GetCopyOfItem(ItemSlot.HAIR, m_hairCode));

        InitializeInventory();
        
        GetAllItems();
    }
    
    public void OnDestroy()
    {
        m_signalBus.Unsubscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Unsubscribe<OnGameItemSoldSignal>(SoldItem);
    }

    private void GetAllItems()
    {
        foreach (KeyValuePair<int, GameItem> kvp in m_itemManager.GameItems)
        {
            AddItem(kvp.Value);
        }
    }
    
    private void BoughtItem(OnGameItemPurchasedSignal args)
    {
        RemoveItem(args.Item);
    }
    
    private void SoldItem(OnGameItemSoldSignal args)
    {
        AddItem(args.Item);
    }

    public bool Interact(Interactor interactor)
    {
        m_signalBus.Fire(new OnClothingShopOpenedSignal(this));
        return true;
    }

    public void InitializeInventory()
    {
        Inventory = new Dictionary<int, GameItem>();
    }

    public void AddItem(GameItem item)
    {
        Inventory.Add(item.ItemID, item);
    }

    public void AddItem(int itemID)
    {
        // Needs Implementation
    }

    public void RemoveItem(GameItem item)
    {
        Inventory.Remove(item.ItemID);
    }

    public void RemoveItem(int itemID)
    {
        Inventory.Remove(itemID);
    }
}
