using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public sealed class ClientDefinitionRegistryTests
{
	private readonly ClientDefinitionRegistry registry = new();

	[TestMethod]
	public void GetDefintion_UnregisteredId_ReturnsNull()
	{
		var result = this.registry.GetDefintion<ItemDefinition>(Ulid.NewUlid());
		Assert.IsNull(result);
	}

	[TestMethod]
	public void RegisterDynamicDefinition_ValidDefinition_CanBeRetrieved()
	{
		var itemDef = new ItemDefinition(new Name("Mithril Dagger"))
		{
			ItemType = ItemType.Weapon, ItemSubtype = ItemSubtype.Dagger, BaseValue = 450
		};

		this.registry.RegisterDynamicDefinition(itemDef);

		var retrieved = this.registry.GetDefintion<ItemDefinition>(itemDef.Id);

		Assert.IsNotNull(retrieved);
		Assert.AreEqual(itemDef.Id, retrieved.Id);
		Assert.AreEqual("Mithril Dagger", retrieved.Name.Singular);
		Assert.AreEqual(450, retrieved.BaseValue);
	}

	[TestMethod]
	public void RegisterDynamicDefinition_OverwritesExistingInstanceWithSameId()
	{
		var id = Ulid.NewUlid();
		var original = new ItemDefinition(new Name("Old Staff")) { Id = id, BaseValue = 10 };
		var updated = new ItemDefinition(new Name("Upgraded Staff")) { Id = id, BaseValue = 50 };

		this.registry.RegisterDynamicDefinition(original);
		this.registry.RegisterDynamicDefinition(updated);

		var retrieved = this.registry.GetDefintion<ItemDefinition>(id);

		Assert.IsNotNull(retrieved);
		Assert.AreEqual("Upgraded Staff", retrieved.Name.Singular);
		Assert.AreEqual(50, retrieved.BaseValue);
	}

	[TestMethod]
	public void LoadPackDefinitions_IngestsAllExtractedDefinitions()
	{
		var material = new MaterialDefinition(new Name("Iron"));
		var stat = new StatDefinition(new Name("Agility"));
		var skill = new SkillDefinition(new Name("Archery"));
		var item = new ItemDefinition(new Name("Shortbow"));
		var entity = new EntityDefinition(new Name("Goblin"));

		var content = new ExtractedPackageContent
		{
			Materials = [material],
			Stats = [stat],
			Skills = [skill],
			Items = [item],
			Entities = [entity]
		};

		this.registry.LoadPackDefinitions(content);

		Assert.IsNotNull(this.registry.GetDefintion<MaterialDefinition>(material.Id));
		Assert.IsNotNull(this.registry.GetDefintion<StatDefinition>(stat.Id));
		Assert.IsNotNull(this.registry.GetDefintion<SkillDefinition>(skill.Id));
		Assert.IsNotNull(this.registry.GetDefintion<ItemDefinition>(item.Id));
		Assert.IsNotNull(this.registry.GetDefintion<EntityDefinition>(entity.Id));
	}
}
