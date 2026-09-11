using Microsoft.VisualStudio.TestTools.UnitTesting;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public class ItemFactoryServiceTests
{
	private readonly ItemFactoryService factory = new ItemFactoryService();

	[TestMethod]
	public void CreateDefinition_ValidParameters_InstantiatesDefinitionCorrectly()
	{
		var name = new Name("Iron Broadsword");
		var def = this.factory.CreateDefinition(
			name,
			ItemType.Weapon,
			ItemSubtype.None,
			baseValue: 150,
			weight: 3.5f,
			maxStackSize: 1);

		Assert.AreNotEqual(Ulid.Empty, def.Id);
		Assert.AreEqual("Iron Broadsword", def.Name.Singular);
		Assert.AreEqual(ItemType.Weapon, def.ItemType);
		Assert.AreEqual(150, def.BaseValue);
		Assert.AreEqual(3.5f, def.Weight);
		Assert.AreEqual(1, def.MaxStackSize);
	}

	[TestMethod]
	public void CreateDefinition_InvalidStackSize_ThrowsArgumentOutOfRangeException()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			this.factory.CreateDefinition(new Name("Rock"), ItemType.Consumable, ItemSubtype.None, maxStackSize: 0));
	}

	[TestMethod]
	public void CreateItem_FromDefinition_BindsIdAndPreservesDefinitionReference()
	{
		var def = this.factory.CreateDefinition(new Name("Health Potion"), ItemType.Consumable, ItemSubtype.None, maxStackSize: 5);
		var item = this.factory.CreateItem(def, quantity: 3);

		Assert.AreNotEqual(Ulid.Empty, item.Id);
		Assert.AreEqual(def.Id, item.DefinitionId);
		Assert.AreSame(def, item.Definition);
		Assert.AreEqual(3, item.Quantity);
	}

	[TestMethod]
	public void CreateItem_ZeroQuantity_ThrowsArgumentOutOfRangeException()
	{
		var def = this.factory.CreateDefinition(new Name("Health Potion"), ItemType.Consumable, ItemSubtype.None);

		Assert.Throws<ArgumentOutOfRangeException>(() => this.factory.CreateItem(def, quantity: 0));
	}
}
