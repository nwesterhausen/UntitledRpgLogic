namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Event arguments for when an item is stored in the inventory.
/// </summary>
public class SuccessfulItemStorageEventArgs : EventArgs
{
	/// <summary>
	///     Create a new instance of <see cref="SuccessfulItemStorageEventArgs" /> with the specified item, amount, item ID,
	///     and total in inventory.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="amount"></param>
	/// <param name="itemId"></param>
	/// <param name="totalInInventory"></param>
	public SuccessfulItemStorageEventArgs(string item, int amount, Guid itemId, int totalInInventory)
	{
		this.Item = item;
		this.Amount = amount;
		this.ItemId = itemId;
		this.TotalInInventory = totalInInventory;
	}

	/// <summary>
	///     The item that was stored in the inventory.
	/// </summary>
	public string Item { get; }

	/// <summary>
	///     The amount of the item that was stored.
	/// </summary>
	public int Amount { get; }

	/// <summary>
	///     The unique identifier of the item that was stored.
	/// </summary>
	public Guid ItemId { get; }

	/// <summary>
	///     Total number of this item in the inventory after storage.
	/// </summary>
	public int TotalInInventory { get; }
}
