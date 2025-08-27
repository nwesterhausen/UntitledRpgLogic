namespace UntitledRpgLogic.Core.Interfaces.Inventory;

/// <summary>
///     An interface for an inventory system.
/// </summary>
public interface IInventory : IItemStorage, ICurrencyStorage
{
	/// <summary>
	///     Gets the amount of space remaining in the inventory.
	/// </summary>
	public float SpaceRemaining { get; }

	/// <summary>
	///     Gets the amount of space currently used in the inventory.
	/// </summary>
	public float SpaceUsed { get; }

	/// <summary>
	///     Gets a value indicating whether the inventory has unlimited storage for currency.
	/// </summary>
	public bool HasUnlimitedCurrencyStorage { get; }

	/// <summary>
	///     Gets a value indicating whether the inventory allows storing other inventories.
	/// </summary>
	public bool AllowsStoringInventories { get; }
}
