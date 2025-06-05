using UntitledRpgLogic.Events;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     An interface for an inventory system.
/// </summary>
public interface IInventory
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
    ///     Gets the total number of items stored in the inventory.
    /// </summary>
    int ItemCount { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory is full.
    /// </summary>
    bool IsFull { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory allows stacking of items.
    /// </summary>
    bool AllowsStacking { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory has unlimited storage for currency.
    /// </summary>
    bool HasUnlimitedCurrencyStorage { get; }

    /// <summary>
    ///     Gets a value indicating whether the inventory allows storing other inventories.
    /// </summary>
    bool AllowsStoringInventories { get; }

    /// <summary>
    ///     Stores an item in the inventory.
    /// </summary>
    /// <param name="item">The item to store.</param>
    /// <returns>True if the item was successfully stored; otherwise, false.</returns>
    bool StoreItem(IStorable item);

    /// <summary>
    ///     Attempts to retrieve an item from the inventory by its unique identifier.
    /// </summary>
    /// <param name="itemId">The unique identifier of the item to retrieve.</param>
    /// <param name="item">The retrieved item, or null if not found.</param>
    /// <returns>True if the item was found and retrieved; otherwise, false.</returns>
    bool TryRetrieveItem(Guid itemId, out IStorable? item);

    /// <summary>
    ///     Attempts to retrieve an item from the inventory by its name.
    /// </summary>
    /// <param name="itemName">The name of the item to retrieve.</param>
    /// <param name="item">The retrieved item, or null if not found.</param>
    /// <returns>True if the item was found and retrieved; otherwise, false.</returns>
    bool TryRetrieveItem(string itemName, out IStorable? item);

    /// <summary>
    ///     Occurs when an item is stored in the inventory.
    /// </summary>
    event EventHandler<ItemStoredEventArgs> ItemStored;

    /// <summary>
    ///     Occurs when an item is retrieved from the inventory.
    /// </summary>
    event EventHandler<ItemRetrievedEventArgs> ItemRetrieved;

    /// <summary>
    ///     Deposits a specified amount of currency into the inventory.
    /// </summary>
    /// <param name="currency">The currency to deposit.</param>
    /// <param name="amount">The amount of currency to deposit. If null, deposits all available.</param>
    /// <returns>The remaining currency that could not be deposited, or null if all was deposited.</returns>
    ICurrency? DepositCurrency(ICurrency currency, int? amount = null);

    /// <summary>
    ///     Withdraws a specified amount of currency from the inventory.
    /// </summary>
    /// <param name="currency">The currency to withdraw.</param>
    /// <param name="amount">The amount of currency to withdraw. Defaults to 1.</param>
    /// <returns>The withdrawn currency, or null if not enough currency was available.</returns>
    ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1);

    /// <summary>
    ///     Occurs when currency is deposited into the inventory.
    /// </summary>
    event EventHandler<CurrencyDepositedEventArgs> CurrencyDeposited;

    /// <summary>
    ///     Occurs when currency is withdrawn from the inventory.
    /// </summary>
    event EventHandler<CurrencyWithdrawnEventArgs> CurrencyWithdrawn;
}