using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.Infrastructure.Data.SQLite;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class UnitOfWorkInteractionTests
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
	public async Task RollbackTransaction_DiscardsUncommittedChangesAcrossRepositories()
	{
		var dbName = $"uow_test_{Guid.NewGuid():N}";
		var provider = CreateSqliteServiceProvider(dbName);
		await using (provider.ConfigureAwait(false))
		{
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var entityId = Ulid.NewUlid();

			var scope = provider.CreateAsyncScope();
			await using (scope.ConfigureAwait(false))
			{
				var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var entityRepo = scope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await uow.BeginTransactionAsync().ConfigureAwait(false);

				await entityRepo.AddAsync(new Entity(entityId)
				{
					Name = new Name("Uncommitted Actor")
				}).ConfigureAwait(false);

				await uow.SaveChangesAsync().ConfigureAwait(false);
				await uow.RollbackTransactionAsync().ConfigureAwait(false);
			}

			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var entity = await entityRepo.GetByIdAsync(entityId).ConfigureAwait(false);

				Assert.IsNull(entity, "Entity should have been discarded on transaction rollback.");
			}
		}
	}

	[TestMethod]
	public async Task CommitTransaction_PersistsAggregateGraphAtomically()
	{
		var dbName = $"uow_test_{Guid.NewGuid():N}";
		var provider = CreateSqliteServiceProvider(dbName);
		await using (provider.ConfigureAwait(false))
		{
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var entityId = Ulid.NewUlid();
			var statDefId = Ulid.NewUlid();

			// 1. Create a StatDefinition and Entity with child stats in one transaction
			var scope = provider.CreateAsyncScope();
			await using (scope.ConfigureAwait(false))
			{
				var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var statRepo = scope.ServiceProvider.GetRequiredService<IEntityRepository<StatDefinition, Ulid>>();
				var entityRepo = scope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await uow.BeginTransactionAsync().ConfigureAwait(false);

				var statDef = new StatDefinition(statDefId, new Name("Strength"));
				await statRepo.AddAsync(statDef).ConfigureAwait(false);

				var entity = new Entity(entityId)
				{
					Name = new Name("Hero")
				};
				entity.SetStat(statDef, initialValue: 15);
				await entityRepo.AddAsync(entity).ConfigureAwait(false);

				await uow.CommitTransactionAsync().ConfigureAwait(false);
			}

			// 2. Verify graph persisted in an isolated scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var hero = await entityRepo.GetByIdAsync(
					entityId,
					cancellationToken: default,
					e => e.Stats).ConfigureAwait(false);

				Assert.IsNotNull(hero);
				Assert.AreEqual("Hero", hero.Name.Singular);
				Assert.HasCount(1, hero.Stats);
			}
		}
	}
}
