namespace UntitledRpgLogic.Core.Economy;

/// <summary>
///     Event arguments for when currency is moved to or from an inventory.
/// </summary>
/// <remarks>
///     Create a new instance of <see cref="CurrencyMovedEventArgs" />.
/// </remarks>
/// <param name="description">
///     A description of the amount of currency moved, such as "100 gold coins"
/// </param>
/// <param name="totalInInventory">
///     The total amount of currency remaining in the inventory after the movement.
/// </param>
public class CurrencyMovedEventArgs(string description, string totalInInventory) : EventArgs
{
	/// <summary>
	///     A description of the amount of currency moved, such as "100 gold coins"
	/// </summary>
	public string Description { get; } = description;

	/// <summary>
	///     The total amount of currency remaining in the inventory after the movement.
	/// </summary>
	public string TotalInInventory { get; } = totalInInventory;
}
