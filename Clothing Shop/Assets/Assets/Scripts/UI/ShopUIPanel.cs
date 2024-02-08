using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopUIPanel : MonoBehaviour
{
    [Inject] private SignalBus m_signalBus;
    [Inject] private UIShopItem.Pool m_itemPool;
    [Inject] private UITab.Pool m_tabPool;
    [Inject] private UIButton.Pool m_buttonPool;
    [Inject] private PlayerInventory m_playerInventory;
    [Inject] private PlayerWallet m_playerWallet;

    [SerializeField] private GameObject m_shopPanel;
    [SerializeField] private Button m_closeShopButton;
    [SerializeField] private Transform m_itemListPanel;
    [SerializeField] private Transform m_actionsPanel;
    [SerializeField] private Transform m_tabsPanel;
    [SerializeField] private PlayerPreview m_playerPreview;

    private UITab m_buyTab;
    private UITab m_sellTab;
    private UIButton m_buyButton;
    private UIButton m_equipButton;
    private UIShopItem[] m_items;

    private Shopkeeper m_shopkeeper;
    private GameItem m_selectedItem;
    
    private bool m_isBuying = true;

    private const string m_sellItem = "Sell";
    private const string m_buyItem = "Buy";
    private const string m_equipItem = "Equip";
    private const string m_buyTabText = "Shop";
    private const string m_sellTabText = "Inventory";
    
    void Start()
    {
        m_signalBus.Subscribe<OnClothingShopOpenedSignal>(SetUp);
        m_signalBus.Subscribe<OnShopItemSelectedSignal>(SelectItem);
        
        m_closeShopButton.onClick.AddListener(CloseShop);
        
        CreateTabs();
        CreateActionButtons();
        
        CloseShop();
    }

    private void OnDestroy()
    {
        m_signalBus.Unsubscribe<OnClothingShopOpenedSignal>(SetUp);
        m_signalBus.Unsubscribe<OnShopItemSelectedSignal>(SelectItem);
    }

    private void SetUp(OnClothingShopOpenedSignal args)
    {
        m_shopkeeper = args.Shop;
        
        m_shopPanel.SetActive(true);

        BuyTabSelected();
    }

    private void CreateTabs()
    {
        m_buyTab = m_tabPool.Spawn(m_buyTabText);
        m_sellTab = m_tabPool.Spawn(m_sellTabText);
        
        m_buyTab.transform.SetParent(m_tabsPanel);
        m_sellTab.transform.SetParent(m_tabsPanel);
        
        m_buyTab.Button.onClick.AddListener(BuyTabSelected);
        m_sellTab.Button.onClick.AddListener(SellTabSelected);
    }

    private void CreateActionButtons()
    {
        m_buyButton = m_buttonPool.Spawn(m_buyItem);
        m_equipButton = m_buttonPool.Spawn(m_equipItem);
        
        m_equipButton.transform.SetParent(m_actionsPanel);
        m_buyButton.transform.SetParent(m_actionsPanel);
        
        m_buyButton.Button.onClick.AddListener(BuyItemButtonClicked);
        m_equipButton.Button.onClick.AddListener(EquipItemButtonClicked);
        
        m_equipButton.gameObject.SetActive(false);
    }

    private void CloseShop()
    {
        m_shopPanel.SetActive(false);
        
        DespawnItems();
    }

    private void BuyTabSelected()
    {
        m_isBuying = true;
        UpdateTabSelection();
    }
    
    private void SellTabSelected()
    {
        m_isBuying = false;
        UpdateTabSelection();
    }

    private void UpdateTabSelection()
    {
        m_buyTab.SetTabActive(m_isBuying);
        m_sellTab.SetTabActive(!m_isBuying);
        
        UpdateItemList();
        
        m_buyButton.SetUp(m_isBuying ? m_buyItem : m_sellItem);
        m_equipButton.gameObject.SetActive(!m_isBuying);
    }

    private void UpdateItemList()
    {
        DespawnItems();
        SpawnItems(m_isBuying ? m_shopkeeper.Inventory : m_playerInventory.Inventory);
    }

    private void DespawnItems()
    {
        foreach (UIShopItem item in m_items)
        {
            item.Despawn();
        }
    }

    private void SpawnItems(Dictionary<int, GameItem> itemDictionary)
    {
        foreach (KeyValuePair<int, GameItem> kvp in itemDictionary)
        {
            UIShopItem item = m_itemPool.Spawn(kvp.Value, m_isBuying);
            item.transform.SetParent(m_itemListPanel);
            m_items.Append(item);
        }
    }

    private void BuyItemButtonClicked()
    {
        if (m_isBuying) m_signalBus.Fire(new OnGameItemPurchasedSignal(m_selectedItem));
        else m_signalBus.Fire(new OnGameItemSoldSignal(m_selectedItem));
        
        UpdateItemList();
    }
    
    private void EquipItemButtonClicked()
    {
        m_signalBus.Fire(new OnGameItemEquipedSignal(m_selectedItem));
    }

    private void SelectItem(OnShopItemSelectedSignal args)
    {
        SelectItem(args.Item);
    }

    private void SelectItem(GameItem item)
    {
        m_selectedItem = item;
    }
    

    private void OnEnable()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }
}
