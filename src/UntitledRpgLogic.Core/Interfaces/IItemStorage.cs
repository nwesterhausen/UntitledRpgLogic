using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for an item storage system that allows storing and retrieving items.
/// </summary>
public interface IItemStorage
{
	/// <summary>
	///     The total number of items currently stored within.
	/// </summary>
	public int ItemCount { get; }

	/// <summary>
	///     Gets the number of unique items currently stored.
	/// </summary>
	public int UniqueItemCount { get; }

	/// <summary>
	///     Whether the inventory has limited storage capacity for items.
	/// </summary>
	public bool HasLimitedStorage { get; }

	/// <summary>
	///     The percentage of space used by items in the inventory.
	/// </summary>
	public float Usage { get; }

	/// <summary>
	///     Whether the storage allows stacking of items of the same type.
	/// </summary>
	public bool AllowsStacking { get; }

	/// <summary>
	///     Whether the storage is full and cannot accept more items.
	/// </summary>
	public bool IsFull { get; }

	/// <summary>
	///     Stores an item in the inventory.
	/// </summary>
	/// <param name="item">The item to store.</param>
	/// <returns>True if the item was successfully stored; otherwise, false.</returns>
	public bool StoreItem(IStorable item);

	/// <summary>
	///     Attempts to retrieve an item from the inventory by its unique identifier.
	/// </summary>
	/// <param name="itemId">The unique identifier of the item to retrieve.</param>
	/// <param name="item">The retrieved item, or null if not found.</param>
	/// <returns>True if the item was found and retrieved; otherwise, false.</returns>
	public bool TryRetrieveItem(Guid itemId, out IStorable? item);

	/// <summary>
	///     Attempts to retrieve an item from the inventory by its name.
	/// </summary>
	/// <param name="itemName">The name of the item to retrieve.</param>
	/// <param name="item">The retrieved item, or null if not found.</param>
	/// <returns>True if the item was found and retrieved; otherwise, false.</returns>
	public bool TryRetrieveItem(string itemName, out IStorable? item);

	/// <summary>
	///     Occurs when an item is stored in the inventory.
	/// </summary>
	public event EventHandler<SuccessfulItemStorageEventArgs> ItemStored;

	/// <summary>
	///     Event that is raised before an item is stored in the inventory.
	/// </summary>
	public event EventHandler<CancelableItemActionEventArgs> StoringItem;

	/// <summary>
	///     Occurs when an item is retrieved from the inventory.
	/// </summary>
	public event EventHandler<SuccessfulItemStorageEventArgs> ItemRetrieved;

	/// <summary>
	///     Event raised before an item is retrieved from the inventory.
	/// </summary>
	public event EventHandler<CancelableItemActionEventArgs> RetrievingItem;
}
