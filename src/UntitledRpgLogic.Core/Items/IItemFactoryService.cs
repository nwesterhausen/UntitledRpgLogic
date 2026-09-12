using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Service responsible for authoring item catalog definitions and spawning instanced items.
/// </summary>
public interface IItemFactoryService
{
	/// <summary>
	///     Creates a new item template definition.
	/// </summary>
	public ItemDefinition CreateDefinition(
		Name name,
		ItemType type,
		ItemSubtype subtype,
		int baseValue = 0,
		float weight = 1.0f,
		int maxStackSize = 1,
		Ulid? creatorEntityId = null,
		ICollection<ItemMaterialComponent>? materials = null);

	/// <summary>
	///     Spawns an instanced item based on an existing item definition.
	/// </summary>
	public Item CreateItem(ItemDefinition definition, int quantity = 1, Ulid? craftedById = null);

	/// <summary>
	///     Spawns an instanced item referencing an item definition by its catalog ID.
	/// </summary>
	public Item CreateItem(Ulid itemDefinitionId, int quantity = 1, Ulid? craftedById = null);
}
