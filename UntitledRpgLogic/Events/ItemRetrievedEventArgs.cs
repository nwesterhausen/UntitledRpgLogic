namespace UntitledRpgLogic.Events;

public class ItemRetrievedEventArgs : EventArgs
{
    public string Item { get; }
    public int Amount { get; }
    public Guid ItemId { get; }
    public int TotalInInventory { get; }
}