using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Data.Repositories;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.Infrastructure.Data.SQLite;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class AggregatePersistenceTests
{
	private static ServiceProvider CreateSqliteServiceProvider(string dbName)
	{
		var services = new ServiceCollection();
		services.AddSqliteDataAccess(opts =>
		{
			opts.ConnectionString = $"Data Source={dbName}.db";
			opts.AutoMigrate = true;
		});

		return services.BuildServiceProvider();
	}

	[TestMethod]
	public async Task PersistAndRetrieve_EntityWithCustomNameAndComplexGraph_HydratesProperly()
	{
		var dbName = $"aggregate_test_{Guid.NewGuid():N}";
		var provider = CreateSqliteServiceProvider(dbName);

		await using (provider.ConfigureAwait(false))
		{
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var entityId = Ulid.NewUlid();
			var skillDefId = Ulid.NewUlid();

			// 1. Seed and save complex aggregate
			var writeScope = provider.CreateAsyncScope();
			await using (writeScope.ConfigureAwait(false))
			{
				var uow = writeScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var skillRepo = writeScope.ServiceProvider.GetRequiredService<IEntityRepository<SkillDefinition, Ulid>>();
				var entityRepo = writeScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await uow.BeginTransactionAsync().ConfigureAwait(false);

				var skillDef = new SkillDefinition
				{
					Id = skillDefId,
					Name = new Name("Pyromancy", "Pyromancies", "Pyromantic")
				};
				await skillRepo.AddAsync(skillDef).ConfigureAwait(false);

				var entity = new Entity(entityId)
				{
					Name = new Name("Wolf", "Wolves", "Lupine")
				};

				// Attaches both the InstancedSkill and the EntitySkills join record
				entity.LearnSkill(skillDef, initialLevel: 5);

				await entityRepo.AddAsync(entity).ConfigureAwait(false);
				await uow.CommitTransactionAsync().ConfigureAwait(false);
			}

			// 2. Read back in a separate scope to ensure no local DbContext caching
			var readScope = provider.CreateAsyncScope();
			await using (readScope.ConfigureAwait(false))
			{
				var entityRepo = readScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var instancedSkillRepo = readScope.ServiceProvider.GetRequiredService<IEntityRepository<InstancedSkill, Ulid>>();

				// Load entity and its join table links
				var loadedEntity = await entityRepo.GetByIdAsync(
					entityId,
					cancellationToken: default,
					e => e.Skills).ConfigureAwait(false);

				// Assert Name converter preserved all 3 distinct fields
				Assert.IsNotNull(loadedEntity);
				Assert.AreEqual("Wolf", loadedEntity.Name.Singular);
				Assert.AreEqual("Wolves", loadedEntity.Name.Plural);
				Assert.AreEqual("Lupine", loadedEntity.Name.Adjective);

				// Assert join records hydrated
				Assert.HasCount(1, loadedEntity.Skills);
				var joinRecord = loadedEntity.Skills.First();
				Assert.AreEqual(entityId, joinRecord.EntityId);

				// Assert the instanced skill entity was persisted to its own table
				var learnedSkill = await instancedSkillRepo.GetByIdAsync(joinRecord.InstancedSkillId).ConfigureAwait(false);
				Assert.IsNotNull(learnedSkill);
				Assert.AreEqual(5, learnedSkill.Level);
				Assert.AreEqual(skillDefId, learnedSkill.SkillDefinitionId);
			}
		}
	}
}
