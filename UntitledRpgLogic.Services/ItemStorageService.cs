using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for an inventory that supports storing items.
/// </summary>
public class ItemStorageService : IItemStorageService
{
    private readonly Dictionary<Guid, IStorable> _items = new();

    /// <inheritdoc />
    public bool StoreItem(IStorable item)
    {
        CancelableItemActionEventArgs cancelableEventArgs = new(item);
        BeforeItemStored?.Invoke(this, cancelableEventArgs);
        if (cancelableEventArgs.Cancel) return false;

        _items.Add(item.Guid, item);

        ItemStored?.Invoke(this, new SuccessfulItemStorageEventArgs(item.Name.Singular, 1, item.Guid, _items.Count));

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
                item = null;
                return false;
            }

            _items.Remove(itemId);
            ItemRetrieved?.Invoke(this,
                new SuccessfulItemStorageEventArgs(item.Name.Singular, 1, item.Guid, _items.Count));
            return true;
        }

        item = null;
        return false;
    }

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemStored;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemStored;

    /// <inheritdoc />
    public event EventHandler<SuccessfulItemStorageEventArgs>? ItemRetrieved;

    /// <inheritdoc />
    public event EventHandler<CancelableItemActionEventArgs>? BeforeItemRetrieved;
}
