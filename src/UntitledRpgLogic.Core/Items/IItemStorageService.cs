namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Domain service managing item containment, stacking, capacity, and retrieval in inventories.
/// </summary>
public interface IItemStorageService
{
	/// <summary>
	///     Evaluates whether the specified item instance can be stored within the target inventory.
	/// </summary>
	/// <param name="inventory">Inventory to check if we can store into.</param>
	/// <param name="item">Item to check if it can be stored in the inventory.</param>
	/// <returns>True if item can be stored.</returns>
	public bool CanStoreItem(Inventory inventory, Item item);

	/// <summary>
	///     Attempts to store an item instance into the target inventory, merging stacks when permissible.
	/// </summary>
	public bool TryStoreItem(Inventory inventory, Item item);

	/// <summary>
	///     Attempts to split or remove a specific quantity of an item from the target inventory.
	/// </summary>
	public bool TryRemoveItem(Inventory inventory, Ulid itemInstanceId, int quantity, out Item? removedItem);

	/// <summary>
	///     Transfers a designated quantity of an item from a source inventory to a destination inventory.
	/// </summary>
	public bool TryTransferItem(Inventory source, Inventory destination, Ulid itemInstanceId, int quantity);
}
