using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Extensions.Common;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Application coordination service managing spatial streaming, chunk persistence,
///     entity world movement, and map transition portals[cite: 2, 4].
/// </summary>
public sealed class WorldCoordinatorService : IWorldCoordinatorService
{
	private readonly IEntityRepository<WorldChunk, Ulid> chunkRepository;
	private readonly IEntityRepository<Entity, Ulid> entityRepository;
	private readonly IEntityRepository<MapDefinition, Ulid> mapRepository;
	private readonly IEntityRepository<MapTransition, Ulid> transitionRepository;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	///     Initializes a new instance of the <see cref="WorldCoordinatorService" /> class.
	/// </summary>
	/// <param name="unitOfWork">The transaction and persistence coordinator[cite: 2, 4].</param>
	/// <param name="entityRepository">The repository for entity aggregates.</param>
	/// <param name="mapRepository">The repository for map definitions[cite: 2, 4].</param>
	/// <param name="chunkRepository">The repository for world chunk grids[cite: 2, 4].</param>
	/// <param name="transitionRepository">The repository for map portals and transitions[cite: 2, 4].</param>
	public WorldCoordinatorService(
		IUnitOfWork unitOfWork,
		IEntityRepository<Entity, Ulid> entityRepository,
		IEntityRepository<MapDefinition, Ulid> mapRepository,
		IEntityRepository<WorldChunk, Ulid> chunkRepository,
		IEntityRepository<MapTransition, Ulid> transitionRepository)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
		this.mapRepository = mapRepository ?? throw new ArgumentNullException(nameof(mapRepository));
		this.chunkRepository = chunkRepository ?? throw new ArgumentNullException(nameof(chunkRepository));
		this.transitionRepository =
			transitionRepository ?? throw new ArgumentNullException(nameof(transitionRepository));
	}

	/// <inheritdoc />
	public async Task<WorldChunk> GetOrLoadChunkAsync(
		Ulid mapId,
		int chunkX,
		int chunkY,
		CancellationToken cancellationToken = default)
	{
		// 1. Check if chunk is already persisted in the database
		var existingChunk = await this.chunkRepository.GetByIdAsync(
			Ulid.Empty,
			q => q.Where(c => c.MapId == mapId && c.ChunkX == chunkX && c.ChunkY == chunkY),
			cancellationToken).ConfigureAwait(false);

		if (existingChunk is not null)
		{
			return existingChunk;
		}

		// 2. Generate a default fallback empty chunk if not present
		var emptyTiles = new Tile2D[ChunkBlobExtensions.TileCount];
		var defaultBlob = emptyTiles.CompressTiles();

		var newChunk = new WorldChunk
		{
			MapId = mapId,
			ChunkX = chunkX,
			ChunkY = chunkY,
			CompressedTileBlob = defaultBlob,
			Version = 1
		};

		await this.chunkRepository.AddAsync(newChunk, cancellationToken).ConfigureAwait(false);
		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		return newChunk;
	}

	/// <inheritdoc />
	public async Task<bool> MoveEntityAsync(
		Ulid entityId,
		Ulid targetMapId,
		float targetX,
		float targetY,
		CancellationToken cancellationToken = default)
	{
		var targetMap = await this.mapRepository.GetByIdAsync(targetMapId, cancellationToken).ConfigureAwait(false);
		if (targetMap is null)
		{
			return false;
		}

		var entity = await this.entityRepository.GetByIdAsync(entityId, cancellationToken).ConfigureAwait(false);
		if (entity is null)
		{
			return false;
		}

		if (entity.Position is null)
		{
			entity.Position = new WorldPosition(targetMapId, targetX, targetY);
		}
		else
		{
			entity.Position = entity.Position with { MapId = targetMapId, X = targetX, Y = targetY };
		}

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		return true;
	}

	/// <inheritdoc />
	public async Task<bool> TriggerTransitionAsync(
		Ulid entityId,
		Ulid transitionId,
		CancellationToken cancellationToken = default)
	{
		var transition = await this.transitionRepository.GetByIdAsync(transitionId, cancellationToken)
			.ConfigureAwait(false);
		if (transition is null)
		{
			return false;
		}

		var entity = await this.entityRepository.GetByIdAsync(entityId, cancellationToken).ConfigureAwait(false);
		if (entity is null)
		{
			return false;
		}

		// Entity must be on the source map
		if (entity.Position is null || entity.Position.MapId != transition.SourceMapId)
		{
			return false;
		}

		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			entity.Position = entity.Position with
			{
				MapId = transition.TargetMapId,
				X = transition.TargetX,
				Y = transition.TargetY
			};

			await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			await this.unitOfWork.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
			return true;
		}
		catch
		{
			await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
			throw;
		}
	}
}
