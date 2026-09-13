using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Pure domain service responsible for generating, validating, and instantiating
///     item catalog definitions and in-memory item tokens.
/// </summary>
public sealed class ItemFactoryService : IItemFactoryService
{
	/// <inheritdoc />
	public ItemDefinition CreateDefinition(
		Name name,
		ItemType type,
		ItemSubtype subtype,
		int baseValue = 0,
		float weight = 1.0f,
		int maxStackSize = 1,
		Ulid? creatorEntityId = null,
		ICollection<ItemMaterialComponent>? materials = null)
	{
		ArgumentNullException.ThrowIfNull(name);

		if (maxStackSize <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(maxStackSize), "Max stack size must be at least 1.");
		}

		return new ItemDefinition(name)
		{
			Id = Ulid.NewUlid(),
			ItemType = type,
			ItemSubtype = subtype,
			BaseValue = Math.Max(0, baseValue),
			Weight = Math.Max(0f, weight),
			MaxStackSize = maxStackSize,
			CreatorEntityId = creatorEntityId,
			Materials = materials ?? [],
			Name = name
		};
	}

	/// <inheritdoc />
	public Item CreateItem(ItemDefinition definition, int quantity = 1, Ulid? craftedById = null)
	{
		ArgumentNullException.ThrowIfNull(definition);

		if (quantity <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
		}

		return new Item
		{
			Id = Ulid.NewUlid(),
			DefinitionId = definition.Id,
			Definition = definition,
			Quantity = quantity,
			CraftedById = craftedById
		};
	}

	/// <inheritdoc />
	public Item CreateItem(Ulid itemDefinitionId, int quantity = 1, Ulid? craftedById = null)
	{
		if (quantity <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
		}

		return new Item
		{
			Id = Ulid.NewUlid(), DefinitionId = itemDefinitionId, Quantity = quantity, CraftedById = craftedById
		};
	}
}
