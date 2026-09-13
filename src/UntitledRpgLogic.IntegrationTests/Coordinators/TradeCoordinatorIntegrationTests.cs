using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Economy;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.IntegrationTests.Common;

namespace UntitledRpgLogic.IntegrationTests.Coordinators;

[TestClass]
public sealed class TradeCoordinatorIntegrationTests : CoordinatorIntegrationTestBase
{
	[TestMethod]
	public async Task ExecutePurchaseAsync_TransfersItemAndCurrencyAcrossInventoriesInDatabase()
	{
		var token = this.TestContext.CancellationToken;
		var dbName = $"trade_int_{Guid.NewGuid():N}";
		var provider = CreateTestProvider(dbName);

		await using (provider.ConfigureAwait(false))
		{
			await InitializeDatabaseAsync(provider, token).ConfigureAwait(false);

			var buyerId = Ulid.NewUlid();
			var sellerId = Ulid.NewUlid();
			var itemId = Ulid.NewUlid();
			var coinDefId = Ulid.NewUlid();
			var swordDefId = Ulid.NewUlid();

			// 1. Seed Buyer with Currency and Seller with a Weapon
			var seedScope = provider.CreateAsyncScope();
			await using (seedScope.ConfigureAwait(false))
			{
				var context = seedScope.ServiceProvider.GetRequiredService<RpgDbContext>();

				var coinDef = new ItemDefinition(new Name("Silver Coin"))
				{
					Id = coinDefId,
					ItemType = ItemType.Currency,
					ItemSubtype = ItemSubtype.Coin,
					BaseValue = 5,
					MaxStackSize = 100
				};

				var swordDef = new ItemDefinition(new Name("Shortsword"))
				{
					Id = swordDefId,
					ItemType = ItemType.Weapon,
					ItemSubtype = ItemSubtype.ShortSword,
					BaseValue = 20, // Costs 4 silver coins
					MaxStackSize = 1
				};

				var buyer = new Entity(new Name("Buyer"))
				{
					Id = buyerId,
					Inventory = new Inventory
					{
						Capacity = 5,
						Items =
						[
							new Item
							{
								DefinitionId = coinDefId, Definition = coinDef, Quantity = 10 // 50 total silver value
							}
						]
					}
				};

				var seller = new Entity(new Name("Blacksmith"))
				{
					Id = sellerId,
					Inventory = new Inventory
					{
						Capacity = 5,
						Items =
						[
							new Item { Id = itemId, DefinitionId = swordDefId, Definition = swordDef, Quantity = 1 }
						]
					}
				};

				await context.ItemDefinitions.AddRangeAsync([coinDef, swordDef], token).ConfigureAwait(false);
				await context.Entities.AddRangeAsync([buyer, seller], token).ConfigureAwait(false);
				await context.SaveChangesAsync(token).ConfigureAwait(false);
			}

			// 2. Execute Purchase via coordinator
			var tradeScope = provider.CreateAsyncScope();
			await using (tradeScope.ConfigureAwait(false))
			{
				var coordinator = tradeScope.ServiceProvider.GetRequiredService<ITradeCoordinatorService>();
				var success = await coordinator.ExecutePurchaseAsync(buyerId, sellerId, itemId, 1, token)
					.ConfigureAwait(false);
				Assert.IsTrue(success);
			}

			// 3. Verify inventories in independent scope
			var verifyScope = provider.CreateAsyncScope();
			await using (verifyScope.ConfigureAwait(false))
			{
				var entityRepo = verifyScope.ServiceProvider.GetRequiredService<IEntityRepository<Entity, Ulid>>();

				var verifyBuyer = await entityRepo.GetByIdAsync(
					buyerId,
					q => q.Include(e => e.Inventory).ThenInclude(i => i!.Items),
					token).ConfigureAwait(false);

				var verifySeller = await entityRepo.GetByIdAsync(
					sellerId,
					q => q.Include(e => e.Inventory).ThenInclude(i => i!.Items),
					token).ConfigureAwait(false);

				Assert.IsNotNull(verifyBuyer?.Inventory);
				Assert.IsNotNull(verifySeller?.Inventory);

				// Buyer paid 4 silver coins. Remainder: 6 coins. Buyer now holds the sword.
				Assert.HasCount(2, verifyBuyer.Inventory.Items);
				Assert.AreEqual(6, verifyBuyer.Inventory.Items.First(i => i.DefinitionId == coinDefId).Quantity);
				Assert.Contains(i => i.DefinitionId == swordDefId, verifyBuyer.Inventory.Items);

				// Seller sold the sword and gained 4 silver coins.
				Assert.HasCount(1, verifySeller.Inventory.Items);
				Assert.AreEqual(coinDefId, verifySeller.Inventory.Items.First().DefinitionId);
				Assert.AreEqual(4, verifySeller.Inventory.Items.First().Quantity);
			}

			await CleanupDatabaseAsync(provider, dbName).ConfigureAwait(false);
		}
	}
}
