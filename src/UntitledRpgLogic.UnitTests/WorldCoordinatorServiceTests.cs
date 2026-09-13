using System.Linq.Expressions;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public sealed class WorldCoordinatorServiceTests
{
	[TestMethod]
	public async Task MoveEntityAsync_ValidEntityAndMap_UpdatesWorldPositionAndSaves()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();
			var mapRepo = new FakeEntityRepository<MapDefinition>();
			var chunkRepo = new FakeEntityRepository<WorldChunk>();
			var transRepo = new FakeEntityRepository<MapTransition>();

			var map = new MapDefinition(new Name("Overworld"));
			mapRepo.Entities[map.Id] = map;

			var entity = new Entity(new Name("Player"));
			entityRepo.Entities[entity.Id] = entity;

			var coordinator = new WorldCoordinatorService(uow, entityRepo, mapRepo, chunkRepo, transRepo);

			var success = await coordinator.MoveEntityAsync(entity.Id, map.Id, 32.5f, 48.0f).ConfigureAwait(false);

			Assert.IsTrue(success);
			Assert.IsNotNull(entity.Position);
			Assert.AreEqual(map.Id, entity.Position.MapId);
			Assert.AreEqual(32.5f, entity.Position.X);
			Assert.AreEqual(48.0f, entity.Position.Y);
			Assert.IsTrue(uow.ChangesSaved);
		}
	}

	[TestMethod]
	public async Task TriggerTransitionAsync_ValidTransition_TeleportsToTargetCoordinates()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();
			var mapRepo = new FakeEntityRepository<MapDefinition>();
			var chunkRepo = new FakeEntityRepository<WorldChunk>();
			var transRepo = new FakeEntityRepository<MapTransition>();

			var sourceMapId = Ulid.NewUlid();
			var targetMapId = Ulid.NewUlid();

			var transition = new MapTransition
			{
				SourceMapId = sourceMapId,
				SourceX = 10f,
				SourceY = 10f,
				TargetMapId = targetMapId,
				TargetX = 2f,
				TargetY = 4f,
				TransitionTag = "dungeon_entrance"
			};
			transRepo.Entities[transition.Id] = transition;

			var entity = new Entity(new Name("Explorer")) { Position = new WorldPosition(sourceMapId, 10f, 10f) };
			entityRepo.Entities[entity.Id] = entity;

			var coordinator = new WorldCoordinatorService(uow, entityRepo, mapRepo, chunkRepo, transRepo);

			var result = await coordinator.TriggerTransitionAsync(entity.Id, transition.Id).ConfigureAwait(false);

			Assert.IsTrue(result);
			Assert.AreEqual(targetMapId, entity.Position.MapId);
			Assert.AreEqual(2f, entity.Position.X);
			Assert.AreEqual(4f, entity.Position.Y);
			Assert.IsTrue(uow.TransactionStarted);
			Assert.IsTrue(uow.TransactionCommitted);
		}
	}

	[TestMethod]
	public async Task GetOrLoadChunkAsync_ChunkDoesNotExist_GeneratesNewAndPersists()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();
			var mapRepo = new FakeEntityRepository<MapDefinition>();
			var chunkRepo = new FakeEntityRepository<WorldChunk>();
			var transRepo = new FakeEntityRepository<MapTransition>();

			var mapId = Ulid.NewUlid();
			var coordinator = new WorldCoordinatorService(uow, entityRepo, mapRepo, chunkRepo, transRepo);

			var chunk = await coordinator.GetOrLoadChunkAsync(mapId, 2, 3).ConfigureAwait(false);

			Assert.IsNotNull(chunk);
			Assert.AreEqual(mapId, chunk.MapId);
			Assert.AreEqual(2, chunk.ChunkX);
			Assert.AreEqual(3, chunk.ChunkY);
			Assert.IsNotEmpty(chunk.CompressedTileBlob);
			Assert.IsTrue(uow.ChangesSaved);
			Assert.HasCount(1, chunkRepo.Entities);
		}
	}

	private sealed class FakeUnitOfWork : IUnitOfWork
	{
		public bool TransactionStarted { get; private set; }
		public bool TransactionCommitted { get; private set; }
		public bool ChangesSaved { get; private set; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			this.ChangesSaved = true;
			return Task.FromResult(1);
		}

		public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			this.TransactionStarted = true;
			return Task.CompletedTask;
		}

		public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
		{
			this.TransactionCommitted = true;
			return Task.CompletedTask;
		}

		public Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
		public void Dispose() { }
		public ValueTask DisposeAsync() => ValueTask.CompletedTask;
	}

	private sealed class FakeEntityRepository<TEntity> : IEntityRepository<TEntity, Ulid>
		where TEntity : class, IDbEntity<Ulid>
	{
		public Dictionary<Ulid, TEntity> Entities { get; } = [];

		public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			this.Entities[entity.Id] = entity;
			return Task.CompletedTask;
		}

		public Task<TEntity?> GetByIdAsync(
			Ulid id,
			CancellationToken cancellationToken = default,
			params Expression<Func<TEntity, object?>>[] includes)
		{
			this.Entities.TryGetValue(id, out var entity);
			return Task.FromResult(entity);
		}

		public Task<TEntity?> GetByIdAsync(
			Ulid id,
			Func<IQueryable<TEntity>, IQueryable<TEntity>> include,
			CancellationToken cancellationToken = default)
		{
			var query = this.Entities.Values.AsQueryable();
			var result = include(query).FirstOrDefault();
			return Task.FromResult(result);
		}

		public Task<IReadOnlyList<TEntity>> GetByIdsAsync(
			IEnumerable<Ulid> ids,
			CancellationToken cancellationToken = default,
			params Expression<Func<TEntity, object?>>[] includes)
		{
			var list = ids.Where(this.Entities.ContainsKey).Select(id => this.Entities[id]).ToList();
			return Task.FromResult<IReadOnlyList<TEntity>>(list);
		}

		public void Update(TEntity entity) => this.Entities[entity.Id] = entity;
		public void Remove(TEntity entity) => this.Entities.Remove(entity.Id);
	}
}
