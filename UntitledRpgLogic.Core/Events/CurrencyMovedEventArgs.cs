namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Event arguments for when currency is moved to or from an inventory.
/// </summary>
public class CurrencyMovedEventArgs : EventArgs
{
    /// <summary>
    ///     Create a new instance of <see cref="CurrencyMovedEventArgs" />.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="totalInInventory"></param>
    public CurrencyMovedEventArgs(string description, string totalInInventory)
    {
        Description = description;
        TotalInInventory = totalInInventory;
    }

    /// <summary>
    ///     A description of the amount of currency moved, such as "100 gold coins"
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     The total amount of currency remaining in the inventory after the movement.
    /// </summary>
    public string TotalInInventory { get; }
}
