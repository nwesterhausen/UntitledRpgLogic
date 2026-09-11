using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class ItemStorageServiceTests
{
	private readonly ItemStorageService service = new();
	private readonly ItemDefinition stackableHerb = new()
	{
		Id = Ulid.NewUlid(),
		Name = new Name("Kingsbloom"),
		MaxStackSize = 10,
		ItemType = ItemType.Consumable,
		ItemSubtype = ItemSubtype.Herb,
	};
	private readonly ItemDefinition nonStackableSword = new()
	{
		Id = Ulid.NewUlid(),
		Name = new Name("Broadsword"),
		MaxStackSize = 1,
		ItemType = ItemType.Weapon,
		ItemSubtype = ItemSubtype.Broadsword,
	};

	[TestMethod]
	public void TryStoreItem_WithinCapacity_AddsItemInstanceDirectly()
	{
		var inventory = new Inventory { Capacity = 2 };
		var sword = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.nonStackableSword.Id,
			Definition = this.nonStackableSword,
			Quantity = 1
		};

		var stored = this.service.TryStoreItem(inventory, sword);

		Assert.IsTrue(stored);
		Assert.HasCount(1, inventory.Items);
		Assert.AreSame(sword, inventory.Items.First());
	}

	[TestMethod]
	public void TryStoreItem_StackableItemWithinLimit_MergesQuantitiesWithoutAddingSlot()
	{
		var inventory = new Inventory { Capacity = 2 };
		var herb1 = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.stackableHerb.Id,
			Definition = this.stackableHerb,
			Quantity = 3
		};
		var herb2 = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.stackableHerb.Id,
			Definition = this.stackableHerb,
			Quantity = 4
		};

		this.service.TryStoreItem(inventory, herb1);
		var merged = this.service.TryStoreItem(inventory, herb2);

		Assert.IsTrue(merged);
		Assert.HasCount(1, inventory.Items);
		Assert.AreEqual(7, inventory.Items.First().Quantity);
	}

	[TestMethod]
	public void TryStoreItem_InventoryFullAndCannotStack_ReturnsFalse()
	{
		var inventory = new Inventory { Capacity = 1 };
		var sword1 = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.nonStackableSword.Id,
			Definition = this.nonStackableSword,
			Quantity = 1
		};
		var sword2 = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.nonStackableSword.Id,
			Definition = this.nonStackableSword,
			Quantity = 1
		};

		this.service.TryStoreItem(inventory, sword1);
		var stored = this.service.TryStoreItem(inventory, sword2);

		Assert.IsFalse(stored);
		Assert.HasCount(1, inventory.Items);
	}

	[TestMethod]
	public void TryRemoveItem_PartialStack_DecrementsOriginalAndReturnsNewInstance()
	{
		var inventory = new Inventory { Capacity = 5 };
		var originalId = Ulid.NewUlid();
		var herbStack = new Item
		{
			Id = originalId,
			DefinitionId = this.stackableHerb.Id,
			Definition = this.stackableHerb,
			Quantity = 8
		};
		inventory.Items.Add(herbStack);

		var success = this.service.TryRemoveItem(inventory, originalId, 3, out var removed);

		Assert.IsTrue(success);
		Assert.IsNotNull(removed);
		Assert.AreEqual(3, removed.Quantity);
		Assert.AreNotEqual(originalId, removed.Id);
		Assert.AreEqual(5, herbStack.Quantity);
		Assert.HasCount(1, inventory.Items);
	}

	[TestMethod]
	public void TryRemoveItem_ExactStack_RemovesInstanceFromInventory()
	{
		var inventory = new Inventory { Capacity = 5 };
		var swordId = Ulid.NewUlid();
		var sword = new Item
		{
			Id = swordId,
			DefinitionId = this.nonStackableSword.Id,
			Definition = this.nonStackableSword,
			Quantity = 1
		};
		inventory.Items.Add(sword);

		var success = this.service.TryRemoveItem(inventory, swordId, 1, out var removed);

		Assert.IsTrue(success);
		Assert.AreSame(sword, removed);
		Assert.IsEmpty(inventory.Items);
	}

	[TestMethod]
	public void TryTransferItem_ValidTransfer_MovesItemBetweenInventories()
	{
		var source = new Inventory { Capacity = 5 };
		var dest = new Inventory { Capacity = 5 };
		var herbId = Ulid.NewUlid();

		source.Items.Add(new Item
		{
			Id = herbId,
			DefinitionId = this.stackableHerb.Id,
			Definition = this.stackableHerb,
			Quantity = 5
		});

		var transferred = this.service.TryTransferItem(source, dest, herbId, 2);

		Assert.IsTrue(transferred);
		Assert.AreEqual(3, source.Items.First().Quantity);
		Assert.HasCount(1, dest.Items);
		Assert.AreEqual(2, dest.Items.First().Quantity);
	}
	[TestMethod]
	public void TryStoreItem_AllowListFilter_AcceptsAllowedType()
	{
		var herbPouch = new Inventory
		{
			Capacity = 5,
			Filter = new InventoryFilter
			{
				IsAllowList = true,
				ItemSubtypes = [ItemSubtype.Herb]
			}
		};

		var herbInstance = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = this.stackableHerb.Id,
			Definition = this.stackableHerb,
			Quantity = 1
		};

		var stored = this.service.TryStoreItem(herbPouch, herbInstance);

		Assert.IsTrue(stored);
		Assert.HasCount(1, herbPouch.Items);
	}

	[TestMethod]
	public void TryStoreItem_AllowListFilter_RejectsUnmatchedType()
	{
		var oreDef = new ItemDefinition
		{
			Id = Ulid.NewUlid(),
			Name = new Name("Iron Ore"),
			ItemType = ItemType.Consumable,
			ItemSubtype = ItemSubtype.Ore
		};

		var herbPouch = new Inventory
		{
			Capacity = 5,
			Filter = new InventoryFilter
			{
				IsAllowList = true,
				ItemSubtypes = [ItemSubtype.Herb]
			}
		};

		var oreInstance = new Item()
		{
			Id = Ulid.NewUlid(),
			DefinitionId = oreDef.Id,
			Definition = oreDef,
			Quantity = 1
		};

		var stored = this.service.TryStoreItem(herbPouch, oreInstance);

		Assert.IsFalse(stored);
		Assert.IsEmpty(herbPouch.Items);
	}

	[TestMethod]
	public void TryStoreItem_BlockListFilter_RejectsBlockedCategory()
	{
		var junkDef = new ItemDefinition
		{
			Id = Ulid.NewUlid(),
			Name = new Name("Broken Bottle"),
			ItemType = ItemType.Junk,
			ItemSubtype = ItemSubtype.None
		};

		var tidyBag = new Inventory
		{
			Capacity = 10,
			Filter = new InventoryFilter
			{
				IsAllowList = false,
				ItemTypes = [ItemType.Junk]
			}
		};

		var junkInstance = new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = junkDef.Id,
			Definition = junkDef,
			Quantity = 1
		};

		var stored = this.service.TryStoreItem(tidyBag, junkInstance);

		Assert.IsFalse(stored);
		Assert.IsEmpty(tidyBag.Items);
	}
}
