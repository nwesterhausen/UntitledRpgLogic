using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Data.SQLite;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class ItemCatalogServiceTests
{
	[TestMethod]
	public async Task RegisterDefinition_And_SpawnItem_PersistsAndHydratesAcrossScopes()
	{
		var dbName = $"catalog_test_{Guid.NewGuid():N}";
		var services = new ServiceCollection();
		services.AddSqliteDataAccess(opts =>
		{
			opts.ConnectionString = $"Data Source={dbName}.db";
			opts.AutoMigrate = true;
		});
		services.AddRpgServices();

		var provider = services.BuildServiceProvider();
		await using (provider.ConfigureAwait(false))
		{
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			Ulid definitionId;

			// Scope 1: Register definition in database
			var scope1 = provider.CreateAsyncScope();
			await using (scope1.ConfigureAwait(false))
			{
				var catalog = scope1.ServiceProvider.GetRequiredService<IItemCatalogService>();
				var def = await catalog.RegisterDefinitionAsync(
					new Name("Mythril Ore"),
					ItemType.Consumable,
					ItemSubtype.Ore,
					baseValue: 500,
					weight: 2.0f,
					maxStackSize: 99).ConfigureAwait(false);

				definitionId = def.Id;
			}

			// Scope 2: Spawn an in-memory item from the persisted catalog entry
			var scope2 = provider.CreateAsyncScope();
			await using (scope2.ConfigureAwait(false))
			{
				var catalog = scope2.ServiceProvider.GetRequiredService<IItemCatalogService>();
				var item = await catalog.SpawnItemFromCatalogAsync(definitionId, quantity: 5).ConfigureAwait(false);

				Assert.IsNotNull(item);
				Assert.AreEqual(definitionId, item.DefinitionId);
				Assert.AreEqual(5, item.Quantity);
				Assert.IsNotNull(item.Definition);
				Assert.AreEqual("Mythril Ore", item.Definition.Name.Singular);
			}
		}
	}
}
