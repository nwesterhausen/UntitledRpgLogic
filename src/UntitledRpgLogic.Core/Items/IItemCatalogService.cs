using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Application coordination service managing database persistence and catalog lookups for items.
/// </summary>
public interface IItemCatalogService
{
	/// <summary>
	///     Creates a new item definition via the domain factory, persists it to the database,
	///     and commits the transaction.
	/// </summary>
	public Task<ItemDefinition> RegisterDefinitionAsync(
		Name name,
		ItemType type,
		ItemSubtype subtype,
		int baseValue = 0,
		float weight = 1.0f,
		int maxStackSize = 1,
		Ulid? creatorEntityId = null,
		ICollection<ItemMaterialComponent>? materials = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Loads an item definition from the database catalog and instantiates an in-memory item token.
	/// </summary>
	public Task<Item> SpawnItemFromCatalogAsync(
		Ulid itemDefinitionId,
		int quantity = 1,
		Ulid? craftedById = null,
		CancellationToken cancellationToken = default);
}
