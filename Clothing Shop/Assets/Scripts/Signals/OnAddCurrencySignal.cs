
public class OnAddCurrencySignal
{
    public int AmountAdded  { get; private set; }

    public OnAddCurrencySignal(int amountAdded)
    {
        AmountAdded = amountAdded;
    }
}