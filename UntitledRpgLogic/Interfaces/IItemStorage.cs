using UntitledRpgLogic.Events;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for an item storage system that allows storing and retrieving items.
/// </summary>
public interface IItemStorage
{
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
}