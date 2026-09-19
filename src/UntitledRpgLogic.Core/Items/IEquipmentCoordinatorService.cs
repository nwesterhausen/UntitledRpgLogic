namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Contract for the equipment coordinator to handle entities inventories and equipped items.
/// </summary>
public interface IEquipmentCoordinatorService
{
	/// <summary>
	///     Equips an item from the entity's inventory, swapping or storing any previously equipped item.
	/// </summary>
	public Task<bool> EquipItemAsync(
		Ulid entityId,
		Ulid itemInstanceId,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Unequips an active item and moves it back into the entity's inventory container.
	/// </summary>
	public Task<bool> UnequipItemAsync(
		Ulid entityId,
		Ulid itemInstanceId,
		CancellationToken cancellationToken = default);
}
