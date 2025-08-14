using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Service for an item storage system that allows storing and retrieving items.
/// </summary>
public interface IItemStorageService
{
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
	public bool TryRetrieveItem(Ulid itemId, out IStorable? item);

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
