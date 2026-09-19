namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Event arguments for when an item is stored in the inventory.
/// </summary>
/// <remarks>
///     Create a new instance of <see cref="SuccessfulItemStorageEventArgs" /> with the specified item, amount, item ID,
///     and total in inventory.
/// </remarks>
/// <param name="item">item that was stored in the inventory</param>
/// <param name="amount">amount of the item that was stored</param>
/// <param name="itemId">unique identifier of the item that was stored</param>
/// <param name="totalInInventory">Total number of this item in the inventory after storage</param>
public class SuccessfulItemStorageEventArgs(string item, int amount, Ulid itemId, int totalInInventory) : EventArgs
{
	/// <summary>
	///     The item that was stored in the inventory.
	/// </summary>
	public string Item { get; } = item;

	/// <summary>
	///     The amount of the item that was stored.
	/// </summary>
	public int Amount { get; } = amount;

	/// <summary>
	///     The unique identifier of the item that was stored.
	/// </summary>
	public Ulid ItemId { get; } = itemId;

	/// <summary>
	///     Total number of this item in the inventory after storage.
	/// </summary>
	public int TotalInInventory { get; } = totalInInventory;
}
