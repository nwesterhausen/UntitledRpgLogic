namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Application coordination service managing database loading, storage operations,
///     and persistence commits for inventories.
/// </summary>
public interface IInventoryCoordinatorService
{
	/// <summary>
	///     Stores an instanced item into an entity's inventory and commits the transaction.
	/// </summary>
	public Task<bool> StoreItemInEntityInventoryAsync(
		Ulid entityId,
		Item item,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Removes an item from an entity's inventory by ID and commits the transaction.
	/// </summary>
	public Task<Item?> RemoveItemFromEntityInventoryAsync(
		Ulid entityId,
		Ulid itemId,
		int quantity,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Transfers an item stack between two entity inventories atomically in a single transaction.
	/// </summary>
	public Task<bool> TransferItemBetweenEntitiesAsync(
		Ulid sourceEntityId,
		Ulid targetEntityId,
		Ulid itemId,
		int quantity,
		CancellationToken cancellationToken = default);
}
