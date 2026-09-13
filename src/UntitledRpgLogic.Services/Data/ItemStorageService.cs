using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service that handles moving items around while respecting inventory and item restrictions.
/// </summary>
public class ItemStorageService : IItemStorageService
{
	/// <inheritdoc />
	public bool CanStoreItem(Inventory inventory, Item item)
	{
		ArgumentNullException.ThrowIfNull(inventory);
		ArgumentNullException.ThrowIfNull(item);

		// No quantity items are invalid
		if (item.Quantity <= 0)
		{
			return false;
		}

		// Evaluate filter constraints
		if (inventory.Filter is not null)
		{
			if (item.Definition is null || !inventory.Filter.IsAllowed(item.Definition))
			{
				return false;
			}
		}

		// Assume 1 stack size if not specified.
		var maxStack = item.Definition?.MaxStackSize ?? 1;

		// If non-stackable, then if we have room, yes we can store it.
		if (maxStack <= 1)
		{
			return inventory.Items.Count < inventory.Capacity;
		}

		// Check to see if we can merge it with another of the same item to increment the stack.
		var matchingItem = inventory.Items.FirstOrDefault(i => i.DefinitionId == item.DefinitionId);
		if (matchingItem is not null && matchingItem.Quantity + item.Quantity <= maxStack)
		{
			return true;
		}

		// Store as a new "stack"
		return inventory.Items.Count < inventory.Capacity;
	}

	/// <inheritdoc />
	public bool TryStoreItem(Inventory inventory, Item item)
	{
		ArgumentNullException.ThrowIfNull(inventory);
		ArgumentNullException.ThrowIfNull(item);

		if (!this.CanStoreItem(inventory, item))
		{
			return false;
		}

		var maxStack = item.Definition?.MaxStackSize ?? 1;

		if (maxStack > 1)
		{
			var matchingItem = inventory.Items.FirstOrDefault(i => i.DefinitionId == item.DefinitionId);
			if (matchingItem is not null && matchingItem.Quantity + item.Quantity <= maxStack)
			{
				matchingItem.Quantity += item.Quantity;
				return true;
			}
		}

		inventory.Items.Add(item);
		return true;
	}

	/// <inheritdoc />
	public bool TryRemoveItem(Inventory inventory, Ulid itemInstanceId, int quantity, out Item? removedItem)
	{
		ArgumentNullException.ThrowIfNull(inventory);
		removedItem = null;

		if (quantity <= 0)
		{
			return false;
		}

		var target = inventory.Items.FirstOrDefault(i => i.Id == itemInstanceId);
		if (target is null || target.Quantity < quantity)
		{
			return false;
		}

		if (target.Quantity == quantity)
		{
			inventory.Items.Remove(target);
			removedItem = target;
			return true;
		}

		target.Quantity -= quantity;
		removedItem = target with { Id = Ulid.NewUlid(), Quantity = quantity };
		return true;
	}

	/// <inheritdoc />
	public bool TryTransferItem(Inventory source, Inventory destination, Ulid itemInstanceId, int quantity)
	{
		ArgumentNullException.ThrowIfNull(source);
		ArgumentNullException.ThrowIfNull(destination);

		var item = source.Items.FirstOrDefault(i => i.Id == itemInstanceId);
		if (item is null || item.Quantity < quantity || quantity <= 0)
		{
			return false;
		}

		var candidate = item with { Quantity = quantity };
		if (!this.CanStoreItem(destination, candidate))
		{
			return false;
		}

		if (!this.TryRemoveItem(source, itemInstanceId, quantity, out var extracted) || extracted is null)
		{
			return false;
		}

		return this.TryStoreItem(destination, extracted);
	}
}
