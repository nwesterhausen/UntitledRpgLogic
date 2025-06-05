using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     A base class for an inventory that has a volume capacity.
/// </summary>
public class VolumeInventoryBase : IInventory
{
    /// <summary>
    ///     The stored items in the inventory, indexed by their unique identifier.
    /// </summary>
    private Dictionary<Guid, IStorable> _items = new();

    /// <summary>
    ///     The total volume of items currently stored in the inventory in cm³.
    /// </summary>
    private float _totalItemVolume = 0f;

    /// <summary>
    ///     Create a new inventory with a specified capacity.
    /// </summary>
    /// <param name="capacity"></param>
    protected VolumeInventoryBase(float capacity)
    {
        Capacity = capacity;
    }

    /// <summary>
    ///     The capacity of the inventory in cm³.
    /// </summary>
    public float Capacity { get; init; }

    /// <inheritdoc />
    public float SpaceRemaining => Capacity - SpaceUsed;

    /// <inheritdoc />
    public float SpaceUsed { get; private set; }

    /// <inheritdoc />
    public int ItemCount { get; }

    /// <inheritdoc />
    public bool IsFull => SpaceRemaining == 0;

    /// <inheritdoc />
    public bool AllowsStacking => true;

    /// <inheritdoc />
    public bool HasUnlimitedCurrencyStorage => false;

    /// <inheritdoc />
    public bool AllowsStoringInventories => false;

    /// <inheritdoc />
    public bool StoreItem(IStorable item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool TryRetrieveItem(Guid itemId, out IStorable? item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool TryRetrieveItem(string itemName, out IStorable? item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<ItemStoredEventArgs>? ItemStored;

    /// <inheritdoc />
    public event EventHandler<ItemRetrievedEventArgs>? ItemRetrieved;

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
    public event EventHandler<CurrencyDepositedEventArgs>? CurrencyDeposited;

    /// <inheritdoc />
    public event EventHandler<CurrencyWithdrawnEventArgs>? CurrencyWithdrawn;
}