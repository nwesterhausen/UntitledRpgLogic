using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Skills;
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
				var skillRepo =
					writeScope.ServiceProvider.GetRequiredService<IEntityRepository<SkillDefinition, Ulid>>();
				var entityRepo = writeScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await uow.BeginTransactionAsync().ConfigureAwait(false);

				var skillDef = new SkillDefinition
				{
					Id = skillDefId,
					Name = new Name("Pyromancy", "Pyromancies", "Pyromantic")
				};
				await skillRepo.AddAsync(skillDef).ConfigureAwait(false);

				var entity = new Entity(entityId) { Name = new Name("Wolf", "Wolves", "Lupine") };

				// Attaches both the InstancedSkill and the EntitySkills join record
				entity.LearnSkill(skillDef, 5);

				await entityRepo.AddAsync(entity).ConfigureAwait(false);
				await uow.CommitTransactionAsync().ConfigureAwait(false);
			}

			// Read back in a separate scope to ensure no local DbContext caching
			var readScope = provider.CreateAsyncScope();
			await using (readScope.ConfigureAwait(false))
			{
				var entityRepo = readScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				// Eagerly load the join table and its downstream InstancedSkill using the new include builder
				var loadedEntity = await entityRepo.GetByIdAsync(
					entityId,
					q => q.Include(e => e.Skills)
						.ThenInclude(s => s.InstancedSkill)).ConfigureAwait(false);

				Assert.IsNotNull(loadedEntity);
				Assert.AreEqual("Wolf", loadedEntity.Name.Singular);
				Assert.AreEqual("Wolves", loadedEntity.Name.Plural);
				Assert.AreEqual("Lupine", loadedEntity.Name.Adjective);

				// Assert child join and nested entity are both hydrated
				Assert.HasCount(1, loadedEntity.Skills);
				var learnedSkill = loadedEntity.Skills.First().InstancedSkill;
				Assert.IsNotNull(learnedSkill);
				Assert.AreEqual(5, learnedSkill.Level);
				Assert.AreEqual(skillDefId, learnedSkill.DefinitionId);
			}
		}
	}
}
