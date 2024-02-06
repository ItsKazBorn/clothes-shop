
public class OnCurrencyAmountChangedSignal
{
    public int TotalCurrencyAmount { get; private set; }

    public OnCurrencyAmountChangedSignal(int totalCurrencyAmount)
    {
        TotalCurrencyAmount = totalCurrencyAmount;
    }
}
