using System;
using Zenject;

public class PlayerWallet : IInitializable, IDisposable
{
    [Inject] private readonly SignalBus m_signalBus;
    [Inject] private readonly Settings m_settings;

    private int m_currency;
    
    public void Initialize()
    {
        m_currency = m_settings.StartMoney;
        m_signalBus.Subscribe<OnAddCurrencySignal>(ChangeCurrencyBy);
        m_signalBus.Subscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Subscribe<OnGameItemSoldSignal>(SoldItem);
    }
    
    public void Dispose()
    {
        m_signalBus.Unsubscribe<OnAddCurrencySignal>(ChangeCurrencyBy);
        m_signalBus.Unsubscribe<OnGameItemPurchasedSignal>(BoughtItem);
        m_signalBus.Unsubscribe<OnGameItemSoldSignal>(SoldItem);
    }
    
    private void ChangeCurrencyBy(OnAddCurrencySignal args)
    {
        ChangeCurrencyBy(args.AmountAdded);
    }

    private void BoughtItem(OnGameItemPurchasedSignal args)
    {
        ChangeCurrencyBy(-args.Item.BuyValue);
    }
    
    private void SoldItem(OnGameItemSoldSignal args)
    {
        ChangeCurrencyBy(args.Item.SellValue);
    }

    private void ChangeCurrencyBy(int amount)
    {
        m_currency += amount;
        m_signalBus.Fire(new OnCurrencyAmountChangedSignal(m_currency));
    }

    public bool HasEnoughCurrency(int amount)
    {
        return m_currency >= amount;
    }
    
    [Serializable]
    public class Settings
    {
        public int StartMoney;
    }

    
}
