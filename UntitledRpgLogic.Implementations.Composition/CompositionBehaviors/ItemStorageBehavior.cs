namespace UntitledRpgLogic.CompositionBehaviors;

/// <summary>
///     Behavior for an inventory that supports storing items.
/// </summary>
public class ItemStorageBehavior : IItemStorage
{
    /// <summary>
    ///     Delegate that defines a method to determine if an item can be stored in the inventory.
    /// </summary>
    public delegate bool AbleToStoreItem(IStorable item);

    /// <summary>
    ///     Delegate the calculates the storage usage of the inventory.
    /// </summary>
    public delegate float CalculateItemStorageUsage(IEnumerable<IStorable> items);

    /// <summary>
    ///     Fallback maximum capacity for the inventory if no custom delegate is provided.
    /// </summary>
    private const int FallbackMaxCapacity = 100;

    /// <summary>
    ///     Internal handle for a custom delegate that determines if an item can be stored in the inventory.
    /// </summary>
    private readonly AbleToStoreItem? _ableToStoreItem;

    /// <summary>
    ///     Internal handle for a custom delegate that calculates the storage usage of the inventory.
    /// </summary>
    private readonly CalculateItemStorageUsage? _calculateItemStorageUsage;

    /// <summary>
    ///     The stored items in the inventory, indexed by their unique identifier.
    /// </summary>
    private readonly Dictionary<Guid, IStorable> _items = new();

    /// <summary>
    ///     Create a new instance of <see cref="ItemStorageBehavior" /> with a custom delegate to determine if an item can be
    ///     stored.
    /// </summary>
    /// <param name="options">options to configure the item storage behavior</param>
    public ItemStorageBehavior(ItemStorageOptions options)
    {
        _ableToStoreItem = options.AbleToStoreItem;
        _calculateItemStorageUsage = options.CalculateItemStorageUsage;
        HasLimitedStorage = options.HasLimitedStorage;
    }

    /// <inheritdoc />
    public int ItemCount => _items.Count;

    /// <inheritdoc />
    public bool HasLimitedStorage { get; }

    /// <inheritdoc />
    public float Usage { get; private set; }

    /// <inheritdoc />
    public bool IsFull => Math.Abs(Usage - 1.0f) < float.Epsilon;

    /// <inheritdoc />
    public bool AllowsStacking => true;

    /// <inheritdoc />
    public bool StoreItem(IStorable item)
    {
        if (_ableToStoreItem != null &&
            !_ableToStoreItem(item)) return false; // Item cannot be stored based on custom logic

        if (IsFull) return false; // Inventory is full

        CancelableItemActionEventArgs cancelableEventArgs = new(item);
        BeforeItemStored?.Invoke(this, cancelableEventArgs);
        if (cancelableEventArgs.Cancel) return false; // Storing the item was canceled

        _items.Add(item.Guid, item);

        ItemStored?.Invoke(this, new SuccessfulItemStorageEventArgs(item.Name, 1, item.Guid, ItemCount));

        RefreshUsage();

        return true;
    }

    /// <inheritdoc />
    public bool TryRetrieveItem(Guid itemId, out IStorable? item)
    {
        if (_items.TryGetValue(itemId, out item))
        {
            CancelableItemActionEventArgs cancelableEventArgs = new(item);
            BeforeItemRetrieved?.Invoke(this, cancelableEventArgs);
            if (cancelableEventArgs.Cancel)
            {
                item = null; // Retrieval was canceled
                return false;
            }

            _items.Remove(itemId);
            ItemRetrieved?.Invoke(this, new SuccessfulItemStorageEventArgs(item.Name, 1, item.Guid, ItemCount));
            RefreshUsage();
            return true; // Item successfully retrieved
        }

        item = null; // Item not found
        return false;
    }

    /// <inheritdoc />
    public bool TryRetrieveItem(string itemName, out IStorable? item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemStored;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemStored;

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemRetrieved;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemRetrieved;

    /// <summary>
    ///     Internal method to refresh the usage of the inventory based on the items stored, or using the provided delegate.
    /// </summary>
    private void RefreshUsage()
    {
        if (_calculateItemStorageUsage != null)
            Usage = _calculateItemStorageUsage(_items.Values);
        else if (!HasLimitedStorage)
            Usage = 0.0f; // No usage if storage is unlimited
        else
            // Fallback usage calculation based on item count and fallback max capacity
            Usage = (float)ItemCount / FallbackMaxCapacity;
    }
}
