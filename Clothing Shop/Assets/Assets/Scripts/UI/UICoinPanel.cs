using Zenject;

public class UICoinPanel : UITextPanel
{
    [Inject] private SignalBus m_signalBus;
    
    void Start()
    {
        m_signalBus.Subscribe<OnCurrencyAmountChangedSignal>(UpdateCoinsAmount);
    }

    private void UpdateCoinsAmount(OnCurrencyAmountChangedSignal args)
    {
        SetUp(args.TotalCurrencyAmount.ToString());
    }
}
