namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     An interface for an inventory system.
/// </summary>
public interface IInventory : IItemStorage, ICurrencyStorage
{
    /// <summary>
    ///     Gets the amount of space remaining in the inventory.
    /// </summary>
    float SpaceRemaining { get; }

    /// <summary>
    ///     Gets the amount of space currently used in the inventory.
    /// </summary>
    float SpaceUsed { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory has unlimited storage for currency.
    /// </summary>
    bool HasUnlimitedCurrencyStorage { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory allows storing other inventories.
    /// </summary>
    bool AllowsStoringInventories { get; }
}
