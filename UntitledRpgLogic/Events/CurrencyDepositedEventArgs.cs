namespace UntitledRpgLogic.Events;

public class CurrencyDepositedEventArgs : EventArgs
{
    public string DepositDescription { get; }
    public string TotalInInventory { get; }
}