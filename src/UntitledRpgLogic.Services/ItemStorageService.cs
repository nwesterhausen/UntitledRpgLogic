using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Inventory;
using UntitledRpgLogic.Core.Interfaces.Services;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for an inventory that supports storing items.
/// </summary>
public class ItemStorageService : IItemStorageService
{
	private readonly Dictionary<Ulid, IStorable> items = [];

	/// <inheritdoc />
	public bool StoreItem(IStorable item)
	{
		ArgumentNullException.ThrowIfNull(item, nameof(item));

		CancelableItemActionEventArgs cancelableEventArgs = new(item);
		this.StoringItem?.Invoke(this, cancelableEventArgs);
		if (cancelableEventArgs.Cancel)
		{
			return false;
		}

		this.items.Add(item.Id, item);

		this.ItemStored?.Invoke(this, new SuccessfulItemStorageEventArgs(item.Name.Singular, 1, item.Id, this.items.Count));

		return true;
	}

	/// <inheritdoc />
	public bool TryRetrieveItem(Ulid itemId, out IStorable? item)
	{
		if (this.items.TryGetValue(itemId, out item))
		{
			CancelableItemActionEventArgs cancelableEventArgs = new(item);
			this.RetrievingItem?.Invoke(this, cancelableEventArgs);
			if (cancelableEventArgs.Cancel)
			{
				item = null;
				return false;
			}

			_ = this.items.Remove(itemId);
			this.ItemRetrieved?.Invoke(this,
				new SuccessfulItemStorageEventArgs(item.Name.Singular, 1, item.Id, this.items.Count));
			return true;
		}

		item = null;
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
