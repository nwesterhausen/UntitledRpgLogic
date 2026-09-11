using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for an inventory that supports storing items.
/// </summary>
public class ItemStorageService : IItemStorageService
{
	private readonly Dictionary<Ulid, Item> items = [];

	/// <inheritdoc />
	public bool StoreItem(Item item)
	{
		ArgumentNullException.ThrowIfNull(item, nameof(item));

		CancelableItemActionEventArgs cancelableEventArgs = new(item.Id);
		this.StoringItem?.Invoke(this, cancelableEventArgs);
		if (cancelableEventArgs.Cancel)
		{
			return false;
		}

		ArgumentNullException.ThrowIfNull(item.ItemDefinition);
		var itemDefinition = item.ItemDefinition;

		this.items.Add(item.Id, item);

		this.ItemStored?.Invoke(this, new SuccessfulItemStorageEventArgs(itemDefinition.Name.Singular, 1, item.Id, this.items.Count));

		return true;
	}

	/// <inheritdoc />
	public bool TryRetrieveItem(Ulid itemId, out Item item)
	{
		item = this.items[itemId];
		return false;
	}

	/// <inheritdoc />
	public event EventHandler<SuccessfulItemStorageEventArgs>? ItemStored;

	/// <inheritdoc />
	public event EventHandler<CancelableItemActionEventArgs>? StoringItem;

	/// <inheritdoc />
	public event EventHandler<SuccessfulItemStorageEventArgs>? ItemRetrieved;

	/// <inheritdoc />
	public event EventHandler<CancelableItemActionEventArgs>? RetrievingItem;
}
