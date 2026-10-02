using Microsoft.Extensions.Logging;
using UntitledRpg.LibraryFile;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Extensions.Logging;

namespace UntitledRpgLogic.Services.Data;

/// <summary>
///     Orchestrates unpacking, parsing, and transactional database insertion of .urpglib content packages.
/// </summary>
public sealed class PackageLoaderService : IPackageLoaderService
{
	private readonly IEntityRepository<EntityDefinition, Ulid> entityDefinitionRepository;
	private readonly IEntityRepository<ItemDefinition, Ulid> itemRepository;
	private readonly ILogger<PackageLoaderService> logger;
	private readonly IEntityRepository<MaterialDefinition, Ulid> materialRepository;
	private readonly IDefinitionSerializerRouter serializerRouter;
	private readonly IEntityRepository<SkillDefinition, Ulid> skillRepository;
	private readonly IEntityRepository<StatDefinition, Ulid> statRepository;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	///     Initializes a new instance of the <see cref="PackageLoaderService" /> class.
	/// </summary>
	/// <param name="unitOfWork">The transaction and commit coordinator.</param>
	/// <param name="serializerRouter">The parser converting raw tar entries into domain definition collections.</param>
	/// <param name="statRepository">The repository persisting stat definitions.</param>
	/// <param name="skillRepository">The repository persisting skill definitions.</param>
	/// <param name="itemRepository">The repository persisting item definitions.</param>
	/// <param name="materialRepository">The repository persisting material definitions.</param>
	/// <param name="entityDefinitionRepository">The repository persisting entity blueprint definitions.</param>
	/// <exception cref="ArgumentNullException">Thrown if any required dependency is <see langword="null" />.</exception>
	public PackageLoaderService(
		IUnitOfWork unitOfWork,
		IDefinitionSerializerRouter serializerRouter,
		IEntityRepository<StatDefinition, Ulid> statRepository,
		IEntityRepository<SkillDefinition, Ulid> skillRepository,
		IEntityRepository<ItemDefinition, Ulid> itemRepository,
		IEntityRepository<MaterialDefinition, Ulid> materialRepository,
		IEntityRepository<EntityDefinition, Ulid> entityDefinitionRepository,
		ILogger<PackageLoaderService> logger)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.serializerRouter = serializerRouter ?? throw new ArgumentNullException(nameof(serializerRouter));
		this.statRepository = statRepository ?? throw new ArgumentNullException(nameof(statRepository));
		this.skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
		this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
		this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
		this.entityDefinitionRepository = entityDefinitionRepository ??
		                                  throw new ArgumentNullException(nameof(entityDefinitionRepository));
		this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	/// <inheritdoc />
	public async Task<PackageIngestionResult> IngestPackageAsync(string filePath,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

		// Check for validation here, and throw apprpriate error (todo)
		var validationResult = await UrpglibFile.ValidateAsync(filePath, ct: cancellationToken)
			.ConfigureAwait(false);
		if (!validationResult.IsValid)
		{
			throw new InvalidDataException($"{filePath} is invalid urpglib package.");
		}

		// Could log some indicators of success here
		// result.EntryCount entries in payload
		// result.Manifest?.Id result.Manifest?.Name result.Manifest?.Version

		var package = await UrpglibFile.OpenReadAsync(filePath, cancellationToken)
			.ConfigureAwait(false);
		return await this.ProcessPackageAsync(package, cancellationToken)
			.ConfigureAwait(false);
	}

	/// <inheritdoc />
	public async Task<PackageIngestionResult> IngestPackageAsync(Stream packageStream,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(packageStream);

		var tempPath = Path.Combine(Path.GetTempPath(), $"pkg_{Guid.NewGuid():N}.urpglib");
		try
		{
			// 1. Declare the stream and scope its async disposal with ConfigureAwait(false)
			var fileStream = File.Create(tempPath);
			await using (fileStream.ConfigureAwait(false))
			{
				await packageStream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
			}

			using var package = await UrpglibReader.ReadAsync(tempPath, true).ConfigureAwait(false);
			return await this.ProcessPackageAsync(package, cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			if (File.Exists(tempPath))
			{
				File.Delete(tempPath);
			}
		}
	}

	private async Task<PackageIngestionResult> ProcessPackageAsync(UrpglibPackage package,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(package);

		var itemCount = 0;
		var entityCount = 0;
		var skillCount = 0;
		var statCount = 0;
		var materialCount = 0;

		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

		await foreach (var entry in package.ReadEntriesAsync(cancellationToken).ConfigureAwait(false))
		{
			try
			{
				var serializer = this.serializerRouter.GetServiceForEntry(entry);
				var definitionType = this.serializerRouter.GetDefinitionTypeFromEntry(entry);

				var parsedEntry = serializer.DeserializeInto(definitionType, entry.OpenStream());

				if (definitionType == typeof(ItemDefinition))
				{
					await this.itemRepository.AddAsync((ItemDefinition)parsedEntry, cancellationToken)
						.ConfigureAwait(false);
					itemCount++;
				}
				else if (definitionType == typeof(SkillDefinition))
				{
					await this.skillRepository.AddAsync((SkillDefinition)parsedEntry, cancellationToken)
						.ConfigureAwait(false);
					skillCount++;
				}
				else if (definitionType == typeof(EntityDefinition))
				{
					await this.entityDefinitionRepository.AddAsync((EntityDefinition)parsedEntry, cancellationToken)
						.ConfigureAwait(false);
					entityCount++;
				}
				else if (definitionType == typeof(StatDefinition))
				{
					await this.statRepository.AddAsync((StatDefinition)parsedEntry, cancellationToken)
						.ConfigureAwait(false);
					statCount++;
				}
				else if (definitionType == typeof(MaterialDefinition))
				{
					await this.materialRepository.AddAsync((MaterialDefinition)parsedEntry, cancellationToken)
						.ConfigureAwait(false);
					materialCount++;
				}
				//todo: add remaining catalogs and definition ingestions
				else
				{
					this.logger.ConfigurationFileOfUhandledType(definitionType);
					//throw new NotSupportedException($"Unable to handle type {definitionType}");
				}
			}
			catch (InvalidCastException ice)
			{
				await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
				throw;
			}
			catch (NotSupportedException nse)
			{
				await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
				throw;
			}
		}

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		await this.unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);

		return new PackageIngestionResult
		{
			Manifest = package.Manifest ?? throw new InvalidDataException("Provided package has null Manifest"),
			ItemsLoaded = itemCount,
			MaterialsLoaded = materialCount,
			EntitiesLoaded = entityCount,
			SkillsLoaded = skillCount,
			StatsLoaded = statCount
		};
	}
}
