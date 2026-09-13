using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <inheritdoc />
public sealed class ItemCatalogService : IItemCatalogService
{
	private readonly IEntityRepository<ItemDefinition, Ulid> definitionRepository;
	private readonly IItemFactoryService factory;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	/// </summary>
	/// <param name="unitOfWork"></param>
	/// <param name="definitionRepository"></param>
	/// <param name="factory"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public ItemCatalogService(
		IUnitOfWork unitOfWork,
		IEntityRepository<ItemDefinition, Ulid> definitionRepository,
		IItemFactoryService factory)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.definitionRepository =
			definitionRepository ?? throw new ArgumentNullException(nameof(definitionRepository));
		this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
	}

	/// <inheritdoc />
	public async Task<ItemDefinition> RegisterDefinitionAsync(
		Name name,
		ItemType type,
		ItemSubtype subtype,
		int baseValue = 0,
		float weight = 1.0f,
		int maxStackSize = 1,
		Ulid? creatorEntityId = null,
		ICollection<ItemMaterialComponent>? materials = null,
		CancellationToken cancellationToken = default)
	{
		// 1. Domain logic generates and validates the instance
		var definition = this.factory.CreateDefinition(
			name,
			type,
			subtype,
			baseValue,
			weight,
			maxStackSize,
			creatorEntityId,
			materials);

		// 2. Application persistence coordinates the commit
		await this.definitionRepository.AddAsync(definition, cancellationToken).ConfigureAwait(false);
		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		return definition;
	}

	/// <inheritdoc />
	public async Task<Item> SpawnItemFromCatalogAsync(
		Ulid itemDefinitionId,
		int quantity = 1,
		Ulid? craftedById = null,
		CancellationToken cancellationToken = default)
	{
		var definition = await this.definitionRepository
			.GetByIdAsync(itemDefinitionId, cancellationToken)
			.ConfigureAwait(false);

		if (definition is null)
		{
			throw new InvalidOperationException($"Item definition '{itemDefinitionId}' was not found in the catalog.");
		}

		return this.factory.CreateItem(definition, quantity, craftedById);
	}
}
