using Microsoft.Extensions.Logging;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     A base class for an inventory that has a volume capacity.
/// </summary>
public class VolumeInventoryBase : IInventory, IHasLogging
{
    /// <summary>
    ///     The behavior that handles item storage logic
    /// </summary>
    private readonly ItemStorageBehavior _itemStorageBehavior;

    /// <summary>
    ///     Behavior that handles logging for the inventory.
    /// </summary>
    private readonly LoggingBehavior _loggingBehavior;

    /// <summary>
    ///     The total volume of items currently stored in the inventory in cm³.
    /// </summary>
    private float _totalItemVolume;

    /// <summary>
    ///     Create a new inventory with a specified capacity.
    /// </summary>
    /// <param name="capacity"></param>
    /// <param name="logger"></param>
    protected VolumeInventoryBase(float capacity, ILogger? logger = null)
    {
        Capacity = capacity;

        _loggingBehavior = new LoggingBehavior(logger);

        _itemStorageBehavior = new ItemStorageBehavior(new ItemStorageOptions
        {
            HasLimitedStorage = true,
            AbleToStoreItem = CanStoreItem,
            CalculateItemStorageUsage = CalculateItemStorageUsage
        });

        // register event passthrus
        _itemStorageBehavior.ItemStored += (_, args) => ItemStored?.Invoke(this, args);
        _itemStorageBehavior.BeforeItemStored += (_, args) => BeforeItemStored?.Invoke(this, args);
        _itemStorageBehavior.ItemRetrieved += (_, args) => ItemRetrieved?.Invoke(this, args);
        _itemStorageBehavior.BeforeItemRetrieved += (_, args) => BeforeItemRetrieved?.Invoke(this, args);
    }

    /// <summary>
    ///     The capacity of the inventory in cm³.
    /// </summary>
    public float Capacity { get; init; }

    /// <inheritdoc />
    public ILogger Logger => _loggingBehavior.Logger;

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        _loggingBehavior.LogEvent(eventId, args);
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        _loggingBehavior.LogError(exception, eventId);
    }

    /// <inheritdoc />
    public bool StoreItem(IStorable item)
    {
        return _itemStorageBehavior.StoreItem(item);
    }

    /// <inheritdoc />
    public float SpaceRemaining => 1.0f - SpaceUsed;

    /// <inheritdoc />
    public float SpaceUsed => _totalItemVolume / Capacity;

    /// <inheritdoc />
    public int ItemCount => _itemStorageBehavior.ItemCount;

    /// <inheritdoc />
    public bool HasLimitedStorage => _itemStorageBehavior.HasLimitedStorage;

    /// <inheritdoc />
    public float Usage => _itemStorageBehavior.Usage;

    /// <inheritdoc />
    public bool AllowsStacking => _itemStorageBehavior.AllowsStacking;

    /// <inheritdoc />
    public bool HasUnlimitedCurrencyStorage => false; // Volume inventory does not support unlimited currency storage

    /// <inheritdoc />
    public bool AllowsStoringInventories => true;

    /// <inheritdoc />
    public bool IsFull => _itemStorageBehavior.IsFull;

    /// <inheritdoc />
    public bool TryRetrieveItem(Guid itemId, out IStorable? item)
    {
        return _itemStorageBehavior.TryRetrieveItem(itemId, out item);
    }

    /// <inheritdoc />
    public bool TryRetrieveItem(string itemName, out IStorable? item)
    {
        return _itemStorageBehavior.TryRetrieveItem(itemName, out item);
    }

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemStored;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemStored;

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemRetrieved;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemRetrieved;

    /// <inheritdoc />
    public ICurrency? DepositCurrency(ICurrency currency, int? amount = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

    /// <inheritdoc />
    public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;

    /// <summary>
    ///     Our delegate to determine if an item can be stored in the inventory. Utilizes volume calculations.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool CanStoreItem(IStorable item)
    {
        if (_totalItemVolume + item.Volume > Capacity) return false; // Not enough space in the inventory

        return true;
    }

    /// <summary>
    ///     Our delegate to calculate the storage usage of the inventory based on the items stored.
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    private float CalculateItemStorageUsage(IEnumerable<IStorable> items)
    {
        _totalItemVolume = items.Sum(item => item.Volume);
        return _totalItemVolume / Capacity;
    }
}