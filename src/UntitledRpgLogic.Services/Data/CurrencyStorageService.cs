using UntitledRpgLogic.Core.Economy;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <summary>
///     A service for handling the process of moving currency around.
/// </summary>
public class CurrencyStorageService : ICurrencyStorageService
{
	private readonly IItemStorageService itemStorageService;


	/// <summary>
	///     Creates a new instance of the currency service, using DI if possible for the ItemStorage service.
	/// </summary>
	/// <param name="itemStorageService"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public CurrencyStorageService(IItemStorageService itemStorageService) =>
		this.itemStorageService = itemStorageService ?? throw new ArgumentNullException(nameof(itemStorageService));

	/// <inheritdoc />
	public long GetTotalValue(Inventory inventory)
	{
		ArgumentNullException.ThrowIfNull(inventory);

		return inventory.Items
			.Where(i => i.Definition?.ItemType == ItemType.Currency)
			.Sum(i => (long)(i.Definition?.BaseValue ?? 0) * i.Quantity);
	}

	/// <inheritdoc />
	public int GetQuantity(Inventory inventory, Ulid currencyItemDefinitionId)
	{
		ArgumentNullException.ThrowIfNull(inventory);

		return inventory.Items
			.Where(i => i.DefinitionId == currencyItemDefinitionId)
			.Sum(i => i.Quantity);
	}

	/// <inheritdoc />
	public bool TryDeposit(Inventory inventory, ItemDefinition currencyDef, int quantity)
	{
		ArgumentNullException.ThrowIfNull(inventory);
		ArgumentNullException.ThrowIfNull(currencyDef);

		if (currencyDef.ItemType != ItemType.Currency || quantity <= 0)
		{
			return false;
		}

		var remaining = quantity;
		var maxStack = currencyDef.MaxStackSize > 0 ? currencyDef.MaxStackSize : int.MaxValue;

		// 1. Top off existing stacks first
		var existingStacks = inventory.Items
			.Where(i => i.DefinitionId == currencyDef.Id && i.Quantity < maxStack)
			.ToList();

		foreach (var stack in existingStacks)
		{
			var room = maxStack - stack.Quantity;
			var toAdd = Math.Min(room, remaining);
			stack.Quantity += toAdd;
			remaining -= toAdd;

			if (remaining == 0)
			{
				return true;
			}
		}

		// 2. Create new Item stacks for the remainder
		while (remaining > 0)
		{
			var stackQty = Math.Min(remaining, maxStack);
			var newItem = new Item
			{
				Id = Ulid.NewUlid(), DefinitionId = currencyDef.Id, Definition = currencyDef, Quantity = stackQty
			};

			if (!this.itemStorageService.CanStoreItem(inventory, newItem))
			{
				return false;
			}

			this.itemStorageService.TryStoreItem(inventory, newItem);
			remaining -= stackQty;
		}

		return true;
	}

	/// <inheritdoc />
	public bool TryWithdraw(Inventory inventory, Ulid currencyItemDefinitionId, int quantity, out Item? withdrawnItem)
	{
		ArgumentNullException.ThrowIfNull(inventory);
		withdrawnItem = null;

		if (quantity <= 0 || this.GetQuantity(inventory, currencyItemDefinitionId) < quantity)
		{
			return false;
		}

		var remainingToWithdraw = quantity;
		ItemDefinition? def = null;

		var matchingStacks = inventory.Items
			.Where(i => i.DefinitionId == currencyItemDefinitionId)
			.ToList();

		foreach (var stack in matchingStacks)
		{
			def ??= stack.Definition;

			if (stack.Quantity <= remainingToWithdraw)
			{
				remainingToWithdraw -= stack.Quantity;
				inventory.Items.Remove(stack);
			}
			else
			{
				stack.Quantity -= remainingToWithdraw;
				remainingToWithdraw = 0;
			}

			if (remainingToWithdraw == 0)
			{
				break;
			}
		}

		withdrawnItem = new Item
		{
			Id = Ulid.NewUlid(), DefinitionId = currencyItemDefinitionId, Definition = def, Quantity = quantity
		};

		return true;
	}
}
