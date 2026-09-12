using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Data.SQLite;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class InventoryCoordinatorServiceTests
{
	private static readonly Ulid PotionDefId = Ulid.NewUlid();
	private static readonly Ulid PotionItemId = Ulid.NewUlid();
	private static readonly Ulid ChestId = Ulid.NewUlid();
	private static readonly Ulid HeroId = Ulid.NewUlid();
	private static readonly Ulid FilteredChestId = Ulid.NewUlid();

	private static readonly ItemDefinition PotionDef = new(PotionItemId)
	{
		ItemType = ItemType.Consumable,
		MaxStackSize = 10,
		Name = new Name("Basic Health Potion"),
		ItemSubtype = ItemSubtype.Potion
	};


	private readonly Item acceptedPotion = new()
	{
		Id = Ulid.NewUlid(), DefinitionId = PotionDef.Id, Definition = PotionDef, Quantity = 1
	};

	private readonly Entity chest = new(ChestId)
	{
		Name = new Name("Treasure Chest"), Inventory = new Inventory(Ulid.NewUlid()) { Capacity = 5 }
	};

	private readonly Entity filteredBagEntity = new()
	{
		Id = FilteredChestId,
		Name = new Name("Alchemist Satchel"),
		Inventory = new Inventory(Ulid.NewUlid())
		{
			Capacity = 10,
			Filter = new InventoryFilter
			{
				IsAllowList = true,
				ItemTypes = [ItemType.Consumable],
				ItemSubtypes = [ItemSubtype.Potion, ItemSubtype.Herb]
			}
		}
	};

	private readonly Entity hero = new(HeroId)
	{
		Name = new Name("Hero"),
		Inventory = new Inventory(Ulid.NewUlid())
		{
			Capacity = 10,
			Items =
			[
				new Item { Id = PotionItemId, DefinitionId = PotionDefId, Definition = PotionDef, Quantity = 5 }
			]
		}
	};

	public TestContext TestContext { get; set; } = null!;

	[TestMethod]
	public async Task TransferItemBetweenEntities_PersistsItemMoveAcrossScopes()
	{
		var token = this.TestContext.CancellationToken;
		var dbName = $"inv_persist_test_{Guid.NewGuid():N}";
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
			await initializer.InitializeAsync(token).ConfigureAwait(false);

			// 1. Seed two entities: a Hero and a Chest
			var scope1 = provider.CreateAsyncScope();
			await using (scope1.ConfigureAwait(false))
			{
				var uow = scope1.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var entityRepo = scope1.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await entityRepo.AddAsync(this.hero, token).ConfigureAwait(false);
				await entityRepo.AddAsync(this.chest, token).ConfigureAwait(false);
				await uow.SaveChangesAsync(token).ConfigureAwait(false);
			}

			// 2. Transfer 2 potions from Hero to Chest using the persistence service
			var scope2 = provider.CreateAsyncScope();
			await using (scope2.ConfigureAwait(false))
			{
				var persistenceService = scope2.ServiceProvider.GetRequiredService<IInventoryCoordinatorService>();
				var success = await persistenceService
					.TransferItemBetweenEntitiesAsync(HeroId, ChestId, PotionItemId, 2, token)
					.ConfigureAwait(false);

				Assert.IsTrue(success);
			}

			// 3. Verify final state in a completely fresh DbContext scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				var verifyHero = await entityRepo.GetByIdAsync(
					HeroId,
					q => q.Include(e => e.Inventory).ThenInclude(i => i!.Items),
					token).ConfigureAwait(false);

				var verifyChest = await entityRepo.GetByIdAsync(
					ChestId,
					q => q.Include(e => e.Inventory).ThenInclude(i => i!.Items),
					token).ConfigureAwait(false);

				Assert.IsNotNull(verifyHero?.Inventory);
				Assert.IsNotNull(verifyChest?.Inventory);

				Assert.AreEqual(3, verifyHero.Inventory.Items.First().Quantity);
				Assert.HasCount(1, verifyChest.Inventory.Items);
				Assert.AreEqual(2, verifyChest.Inventory.Items.First().Quantity);
			}
		}
	}

	[TestMethod]
	[SuppressMessage("Maintainability", "CA1506:Avoid excessive class coupling",
		Justification = "Integration test requires end-to-end service, repository, and entity configuration.")]
	public async Task InventoryWithFilter_PersistsJsonStructure_AndFiltersCorrectlyAfterHydration()
	{
		var dbName = $"inv_filter_test_{Guid.NewGuid():N}";
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
			await initializer.InitializeAsync(this.TestContext.CancellationToken).ConfigureAwait(false);

			// 1. Seed entity with configured filter
			var scope1 = provider.CreateAsyncScope();
			await using (scope1.ConfigureAwait(false))
			{
				var uow = scope1.ServiceProvider.GetRequiredService<IUnitOfWork>();
				var entityRepo = scope1.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				await entityRepo.AddAsync(this.filteredBagEntity, this.TestContext.CancellationToken)
					.ConfigureAwait(false);
				await uow.SaveChangesAsync(this.TestContext.CancellationToken).ConfigureAwait(false);
			}

			// 2. Read back in a new scope and verify deserialized filter
			var scope2 = provider.CreateAsyncScope();
			await using (scope2.ConfigureAwait(false))
			{
				var entityRepo = scope2.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();
				var storageService = scope2.ServiceProvider.GetRequiredService<IItemStorageService>();

				var loaded = await entityRepo.GetByIdAsync(
					FilteredChestId,
					q => q.Include(e => e.Inventory),
					this.TestContext.CancellationToken).ConfigureAwait(false);

				Assert.IsNotNull(loaded?.Inventory);
				Assert.IsNotNull(loaded.Inventory.Filter);
				Assert.IsTrue(loaded.Inventory.Filter.IsAllowList);

				// Verify the nested enum lists deserialized from JSON
				Assert.HasCount(1, loaded.Inventory.Filter.ItemTypes);
				Assert.HasCount(2, loaded.Inventory.Filter.ItemSubtypes);
				Assert.Contains(ItemSubtype.Potion, loaded.Inventory.Filter.ItemSubtypes);

				// Verify the hydrated filter actively gates items via the domain service
				Assert.IsTrue(storageService.CanStoreItem(loaded.Inventory, this.acceptedPotion));
			}
		}
	}
}
