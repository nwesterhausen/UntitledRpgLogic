using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.LibraryFile;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Orchestrates unpacking, parsing, and transactional database insertion of .urpglib content packages.
/// </summary>
public sealed class PackageLoaderService : IPackageLoaderService
{
	private readonly IPackageContentParser contentParser;
	private readonly IEntityRepository<EntityDefinition, Ulid> entityDefinitionRepository;
	private readonly IEntityRepository<ItemDefinition, Ulid> itemRepository;
	private readonly IEntityRepository<MaterialDefinition, Ulid> materialRepository;
	private readonly IEntityRepository<SkillDefinition, Ulid> skillRepository;
	private readonly IEntityRepository<StatDefinition, Ulid> statRepository;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	/// </summary>
	/// <param name="unitOfWork"></param>
	/// <param name="contentParser"></param>
	/// <param name="statRepository"></param>
	/// <param name="skillRepository"></param>
	/// <param name="itemRepository"></param>
	/// <param name="materialRepository"></param>
	/// <param name="entityDefinitionRepository"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public PackageLoaderService(
		IUnitOfWork unitOfWork,
		IPackageContentParser contentParser,
		IEntityRepository<StatDefinition, Ulid> statRepository,
		IEntityRepository<SkillDefinition, Ulid> skillRepository,
		IEntityRepository<ItemDefinition, Ulid> itemRepository,
		IEntityRepository<MaterialDefinition, Ulid> materialRepository,
		IEntityRepository<EntityDefinition, Ulid> entityDefinitionRepository)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.contentParser = contentParser ?? throw new ArgumentNullException(nameof(contentParser));
		this.statRepository = statRepository ?? throw new ArgumentNullException(nameof(statRepository));
		this.skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
		this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
		this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
		this.entityDefinitionRepository = entityDefinitionRepository ??
										  throw new ArgumentNullException(nameof(entityDefinitionRepository));
	}

	/// <inheritdoc />
	public async Task<PackageIngestionResult> IngestPackageAsync(string filePath,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

		// Ensure payload is fully buffered into memory so the file handle is detached
		using var package = await UrpglibReader.ReadAsync(filePath, true).ConfigureAwait(false);
		return await this.ProcessPackageAsync(package, cancellationToken).ConfigureAwait(false);
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

		// Open the TarReader from the library file package
		using var tarReader = package.OpenPayload();

		var content = await this.contentParser.ParsePayloadAsync(tarReader, cancellationToken).ConfigureAwait(false);

		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

		try
		{
			foreach (var mat in content.Materials)
			{
				await this.materialRepository.AddAsync(mat, cancellationToken).ConfigureAwait(false);
			}

			foreach (var stat in content.Stats)
			{
				await this.statRepository.AddAsync(stat, cancellationToken).ConfigureAwait(false);
			}

			foreach (var skill in content.Skills)
			{
				await this.skillRepository.AddAsync(skill, cancellationToken).ConfigureAwait(false);
			}

			foreach (var item in content.Items)
			{
				await this.itemRepository.AddAsync(item, cancellationToken).ConfigureAwait(false);
			}

			foreach (var entity in content.Entities)
			{
				await this.entityDefinitionRepository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
			}

			await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			await this.unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);

			return new PackageIngestionResult
			{
				Manifest =
					package.Manifest ?? throw new InvalidOperationException("Package manifest cannot be null."),
				MaterialsLoaded = content.Materials.Count,
				StatsLoaded = content.Stats.Count,
				SkillsLoaded = content.Skills.Count,
				ItemsLoaded = content.Items.Count,
				EntitiesLoaded = content.Entities.Count
			};
		}
		catch
		{
			await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
			throw;
		}
	}
}
