namespace UntitledRpgLogic.Events;

public class CurrencyWithdrawnEventArgs : EventArgs
{
    public string WithdrawalDescription { get; }
    public string TotalInInventroy { get; }
}