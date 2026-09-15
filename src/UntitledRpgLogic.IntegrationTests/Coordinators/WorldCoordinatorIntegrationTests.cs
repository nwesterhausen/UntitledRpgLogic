using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.IntegrationTests.Common;
using UntitledRpgLogic.WorldGen;

namespace UntitledRpgLogic.IntegrationTests.Coordinators;

[TestClass]
public sealed class WorldCoordinatorIntegrationTests : CoordinatorIntegrationTestBase
{
	[TestMethod]
	public async Task MoveAndTransition_UpdatesEntityCoordinatesAndGeneratesChunks()
	{
		var token = this.TestContext.CancellationToken;
		var dbName = $"world_int_{Guid.NewGuid():N}";
		var worldServices = new ServiceCollection();
		worldServices.AddSingleton<IChunkGeneratorService, ChunkGeneratorService>();
		worldServices.AddSingleton<IWorldGenContextProvider, WorldGenContextProvider>();
		var provider = CreateTestProvider(dbName, worldServices);

		await using (provider.ConfigureAwait(false))
		{
			await InitializeDatabaseAsync(provider, token).ConfigureAwait(false);

			var overworldMapId = Ulid.NewUlid();
			var dungeonMapId = Ulid.NewUlid();
			var entityId = Ulid.NewUlid();
			var transitionId = Ulid.NewUlid();

			// 1. Seed Maps, Transition, and Entity
			var seedScope = provider.CreateAsyncScope();
			await using (seedScope.ConfigureAwait(false))
			{
				var context = seedScope.ServiceProvider.GetRequiredService<RpgDbContext>();

				var overworld =
					new MapDefinition(new Name("Overworld")) { Id = overworldMapId, Type = MapType.Overworld };
				var dungeon = new MapDefinition(new Name("Dungeon")) { Id = dungeonMapId, Type = MapType.Dungeon };

				var transition = new MapTransition
				{
					Id = transitionId,
					SourceMapId = overworldMapId,
					SourceX = 16f,
					SourceY = 16f,
					TargetMapId = dungeonMapId,
					TargetX = 5f,
					TargetY = 5f,
					TransitionTag = "dungeon_gate"
				};

				var entity = new Entity(new Name("Wanderer"))
				{
					Id = entityId, Position = new WorldPosition(overworldMapId, 0f, 0f)
				};

				await context.MapDefinitions.AddRangeAsync([overworld, dungeon], token).ConfigureAwait(false);
				await context.MapTransitions.AddAsync(transition, token).ConfigureAwait(false);
				await context.Entities.AddAsync(entity, token).ConfigureAwait(false);
				await context.SaveChangesAsync(token).ConfigureAwait(false);
			}

			// 2. Move entity and trigger transition through coordinator
			var coordScope = provider.CreateAsyncScope();
			var chunkId = Ulid.Empty;
			await using (coordScope.ConfigureAwait(false))
			{
				var coordinator = coordScope.ServiceProvider.GetRequiredService<IWorldCoordinatorService>();

				var moved = await coordinator.MoveEntityAsync(entityId, overworldMapId, 16f, 16f, token)
					.ConfigureAwait(false);
				Assert.IsTrue(moved);

				var transitioned = await coordinator.TriggerTransitionAsync(entityId, transitionId, token)
					.ConfigureAwait(false);
				Assert.IsTrue(transitioned);

				var chunk = await coordinator.GetOrLoadChunkAsync(dungeonMapId, 0, 0, token).ConfigureAwait(false);
				Assert.IsNotNull(chunk);
				Assert.AreEqual(dungeonMapId, chunk.MapId);

				chunkId = chunk.Id;
			}

			// 3. Verify in independent scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var chunkRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<WorldChunk, Ulid>>();

				var entity = await entityRepo.GetByIdAsync(entityId, token).ConfigureAwait(false);
				Assert.IsNotNull(entity?.Position);
				Assert.AreEqual(dungeonMapId, entity.Position.MapId);
				Assert.AreEqual(5f, entity.Position.X);
				Assert.AreEqual(5f, entity.Position.Y);

				var generatedChunk = await chunkRepo.GetByIdAsync(
					chunkId,
					q => q.Where(c => c.MapId == dungeonMapId && c.ChunkX == 0 && c.ChunkY == 0),
					token).ConfigureAwait(false);

				Assert.IsNotNull(generatedChunk);
				Assert.IsNotEmpty(generatedChunk.CompressedTileBlob);
			}

			await CleanupDatabaseAsync(provider, dbName).ConfigureAwait(false);
		}
	}
}
