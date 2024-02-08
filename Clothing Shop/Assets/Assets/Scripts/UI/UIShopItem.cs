using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class UIShopItem : MonoBehaviour
{
    [Inject] private SignalBus m_signalBus;
    [Inject] private SpriteSheetManager m_spriteSheetManager;

    [SerializeField] private Button m_button;
    [SerializeField] private Image m_itemIcon;
    [SerializeField] private UITextPanel m_coinPanel;

    private GameItem m_item;
    
    private void SetUp(GameItem item, bool isBuying)
    {
        m_item = item;
        m_itemIcon.sprite = m_spriteSheetManager.GetItemIcon(item.Slot, item.Code);
        m_coinPanel.SetUp(isBuying ? item.BuyValue.ToString() : item.SellValue.ToString());
        m_button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        m_signalBus.Fire(new OnShopItemSelectedSignal(m_item));
    }

    public class Pool : MonoMemoryPool<GameItem, bool, UIShopItem>
    {
        protected override void Reinitialize(GameItem item, bool isBuying, UIShopItem uiShopItem)
        {
            base.Reinitialize(item, isBuying, uiShopItem);
            uiShopItem.SetUp(item, isBuying);
        }
    }
}
