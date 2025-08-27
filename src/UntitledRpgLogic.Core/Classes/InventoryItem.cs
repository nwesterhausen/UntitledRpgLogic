namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     An item stored within an inventory. Cannot be interacted with directly, but provides information about the item.
/// </summary>
public class InventoryItem
{
	/// <summary>
	///     The unique identifier for the item in the inventory.
	/// </summary>
	public required Ulid ItemId { get; init; }

	/// <summary>
	///     The name of the item in the inventory.
	/// </summary>
	public required string ItemName { get; init; }

	/// <summary>
	///     The amount of the item in the inventory (if it is stackable, and the inventory allows stacking).
	/// </summary>
	public int Amount { get; init; } = 1;
}
