using System;
using Zenject;

public class CurrencyManager : IInitializable, IDisposable
{
    [Inject] private readonly SignalBus m_signalBus;
    [Inject] private readonly Settings m_settings;

    private int m_currencyAmount = 0;
    
    [Serializable]
    public class Settings
    {
        public int StartMoney;
    }
    
    public void Initialize()
    {
        m_signalBus.Subscribe<OnGameStartedSignal>(SetUp);
        m_signalBus.Subscribe<OnAddCurrencySignal>(AddCurrency);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<OnGameStartedSignal>(SetUp);
        m_signalBus.Unsubscribe<OnAddCurrencySignal>(AddCurrency);
    }
    
    private void SetUp()
    {
        m_currencyAmount = m_settings.StartMoney;
        m_signalBus.Fire(new OnCurrencyAmountChangedSignal(m_currencyAmount));
    }
    
    private void AddCurrency(OnAddCurrencySignal args)
    {
        AddCurrency(args.AmountAdded);
    }
    
    private void AddCurrency(int amount)
    {
        m_currencyAmount += amount;
        m_signalBus.Fire(new OnCurrencyAmountChangedSignal(m_currencyAmount));
    }

    public bool HasEnoughCurrency(int cost)
    {
        return m_currencyAmount >= cost;
    }
}
