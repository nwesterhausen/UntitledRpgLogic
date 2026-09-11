using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class CurrencyStorageServiceTests
{
	private readonly CurrencyStorageService currencyService = new CurrencyStorageService(new ItemStorageService());
	private readonly ItemDefinition goldCoinDef = new ItemDefinition
	{
		Id = Ulid.NewUlid(),
		Name = new Name("Gold Coin"),
		ItemType = ItemType.Currency,
		ItemSubtype = ItemSubtype.Coin,
		BaseValue = 100,
		MaxStackSize = 50
	};
	private readonly ItemDefinition silverCoinDef = new ItemDefinition
	{
		Id = Ulid.NewUlid(),
		Name = new Name("Silver Coin"),
		ItemType = ItemType.Currency,
		ItemSubtype = ItemSubtype.Coin,
		BaseValue = 10,
		MaxStackSize = 50
	};

	[TestMethod]
	public void TryDeposit_CreatesNewItemStack_WhenInventoryHasSpace()
	{
		var pouch = new Inventory { Capacity = 5 };

		var result = this.currencyService.TryDeposit(pouch, this.goldCoinDef, 25);

		Assert.IsTrue(result);
		Assert.AreEqual(25, this.currencyService.GetQuantity(pouch, this.goldCoinDef.Id));
		Assert.HasCount(1, pouch.Items);
	}

	[TestMethod]
	public void TryDeposit_ExceedsMaxStack_SplitsAcrossMultipleSlots()
	{
		var pouch = new Inventory { Capacity = 5 };

		// MaxStack is 50, depositing 70 -> 1 stack of 50, 1 stack of 20
		var result = this.currencyService.TryDeposit(pouch, this.goldCoinDef, 70);

		Assert.IsTrue(result);
		Assert.AreEqual(70, this.currencyService.GetQuantity(pouch, this.goldCoinDef.Id));
		Assert.HasCount(2, pouch.Items);
	}

	[TestMethod]
	public void TryDeposit_RespectsAllowListInventoryFilter()
	{
		var gemBag = new Inventory
		{
			Capacity = 5,
			Filter = new InventoryFilter
			{
				IsAllowList = true,
				ItemSubtypes = [ItemSubtype.CutGemstone, ItemSubtype.RawGemstone]
			}
		};

		var result = this.currencyService.TryDeposit(gemBag, this.goldCoinDef, 10);

		Assert.IsFalse(result);
		Assert.IsEmpty(gemBag.Items);
	}

	[TestMethod]
	public void GetTotalValue_CalculatesAcrossMultipleCurrencyTypes()
	{
		var pouch = new Inventory { Capacity = 5 };

		this.currencyService.TryDeposit(pouch, this.goldCoinDef, 2);   // 2 * 100 = 200
		this.currencyService.TryDeposit(pouch, this.silverCoinDef, 5); // 5 * 10  = 50

		var total = this.currencyService.GetTotalValue(pouch);

		Assert.AreEqual(250, total);
	}

	[TestMethod]
	public void TryWithdraw_SufficientFunds_ExtractsItemAndReducesInventory()
	{
		var pouch = new Inventory { Capacity = 5 };
		this.currencyService.TryDeposit(pouch, this.goldCoinDef, 40);

		var success = this.currencyService.TryWithdraw(pouch, this.goldCoinDef.Id, 15, out var withdrawn);

		Assert.IsTrue(success);
		Assert.IsNotNull(withdrawn);
		Assert.AreEqual(15, withdrawn.Quantity);
		Assert.AreEqual(this.goldCoinDef.Id, withdrawn.DefinitionId);
		Assert.AreEqual(25, this.currencyService.GetQuantity(pouch, this.goldCoinDef.Id));
	}

	[TestMethod]
	public void TryWithdraw_InsufficientFunds_ReturnsFalseAndLeavesInventoryUntouched()
	{
		var pouch = new Inventory { Capacity = 5 };
		this.currencyService.TryDeposit(pouch, this.goldCoinDef, 10);

		var success = this.currencyService.TryWithdraw(pouch, this.goldCoinDef.Id, 25, out var withdrawn);

		Assert.IsFalse(success);
		Assert.IsNull(withdrawn);
		Assert.AreEqual(10, this.currencyService.GetQuantity(pouch, this.goldCoinDef.Id));
	}
}
