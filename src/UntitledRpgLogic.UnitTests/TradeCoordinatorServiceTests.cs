using System.Linq.Expressions;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.UnitTests;

[TestClass]
public sealed class TradeCoordinatorServiceTests
{
	private readonly CurrencyStorageService currencyStorageService;

	private readonly ItemDefinition goldCoinDef = new(new Name("Gold Coin"))
	{
		ItemType = ItemType.Currency,
		ItemSubtype = ItemSubtype.Coin,
		BaseValue = 10,
		MaxStackSize = 100
	};

	private readonly ItemStorageService itemStorageService = new();

	private readonly ItemDefinition swordDef = new(new Name("Iron Sword"))
	{
		ItemType = ItemType.Weapon,
		ItemSubtype = ItemSubtype.Broadsword,
		BaseValue = 30, // Costs 3 gold coins
		MaxStackSize = 1
	};

	public TradeCoordinatorServiceTests() =>
		this.currencyStorageService = new CurrencyStorageService(this.itemStorageService);

	[TestMethod]
	public async Task ExecutePurchaseAsync_SufficientFundsAndSpace_TransfersItemAndCurrency()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();

			// Setup Buyer with 5 Gold Coins (50 value >= 30 cost)
			var buyer = new Entity(new Name("Buyer")) { Inventory = new Inventory { Capacity = 5 } };
			this.currencyStorageService.TryDeposit(buyer.Inventory, this.goldCoinDef, 5);
			entityRepo.Entities[buyer.Id] = buyer;

			// Setup Seller with 1 Iron Sword
			var swordItem = new Item { DefinitionId = this.swordDef.Id, Definition = this.swordDef, Quantity = 1 };
			var seller = new Entity(new Name("Merchant"))
			{
				Inventory = new Inventory { Capacity = 5, Items = [swordItem] }
			};
			entityRepo.Entities[seller.Id] = seller;

			var coordinator = new TradeCoordinatorService(
				uow,
				entityRepo,
				this.itemStorageService,
				this.currencyStorageService);

			var success = await coordinator.ExecutePurchaseAsync(
				buyer.Id,
				seller.Id,
				swordItem.Id,
				1).ConfigureAwait(false);

			Assert.IsTrue(success);
			Assert.IsTrue(uow.TransactionStarted);
			Assert.IsTrue(uow.ChangesSaved);
			Assert.IsTrue(uow.TransactionCommitted);

			// Buyer should have the sword and 2 gold coins left (50 - 30 = 20)
			Assert.AreEqual(20, this.currencyStorageService.GetTotalValue(buyer.Inventory));
			Assert.Contains(i => i.DefinitionId == this.swordDef.Id, buyer.Inventory.Items);

			// Seller should have 0 swords and 3 gold coins (30 value)
			Assert.DoesNotContain(i => i.DefinitionId == this.swordDef.Id, seller.Inventory.Items);
			Assert.AreEqual(30, this.currencyStorageService.GetTotalValue(seller.Inventory));
		}
	}

	[TestMethod]
	public async Task ExecutePurchaseAsync_InsufficientFunds_FailsWithoutMutatingInventories()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();

			// Buyer only has 1 Gold Coin (10 value < 30 cost)
			var buyer = new Entity(new Name("Poor Buyer")) { Inventory = new Inventory { Capacity = 5 } };
			this.currencyStorageService.TryDeposit(buyer.Inventory, this.goldCoinDef, 1);
			entityRepo.Entities[buyer.Id] = buyer;

			var swordItem = new Item { DefinitionId = this.swordDef.Id, Definition = this.swordDef, Quantity = 1 };
			var seller = new Entity(new Name("Merchant"))
			{
				Inventory = new Inventory { Capacity = 5, Items = [swordItem] }
			};
			entityRepo.Entities[seller.Id] = seller;

			var coordinator = new TradeCoordinatorService(
				uow,
				entityRepo,
				this.itemStorageService,
				this.currencyStorageService);

			var success = await coordinator.ExecutePurchaseAsync(
				buyer.Id,
				seller.Id,
				swordItem.Id,
				1).ConfigureAwait(false);

			Assert.IsFalse(success);
			Assert.IsFalse(uow.TransactionStarted);
			Assert.AreEqual(10, this.currencyStorageService.GetTotalValue(buyer.Inventory));
			Assert.HasCount(1, seller.Inventory.Items);
		}
	}

	[TestMethod]
	public async Task ExecutePurchaseAsync_BuyerInventoryFull_FailsWithoutTransfer()
	{
		var uow = new FakeUnitOfWork();
		await using (uow.ConfigureAwait(false))
		{
			var entityRepo = new FakeEntityRepository<Entity>();

			// Buyer has capacity 1, occupied completely by gold coins
			var buyer = new Entity(new Name("Full Buyer")) { Inventory = new Inventory { Capacity = 1 } };
			this.currencyStorageService.TryDeposit(buyer.Inventory, this.goldCoinDef, 10);
			entityRepo.Entities[buyer.Id] = buyer;

			var swordItem = new Item { DefinitionId = this.swordDef.Id, Definition = this.swordDef, Quantity = 1 };
			var seller = new Entity(new Name("Merchant"))
			{
				Inventory = new Inventory { Capacity = 5, Items = [swordItem] }
			};
			entityRepo.Entities[seller.Id] = seller;

			var coordinator = new TradeCoordinatorService(
				uow,
				entityRepo,
				this.itemStorageService,
				this.currencyStorageService);

			var success = await coordinator.ExecutePurchaseAsync(
				buyer.Id,
				seller.Id,
				swordItem.Id,
				1).ConfigureAwait(false);

			Assert.IsFalse(success);
			Assert.IsFalse(uow.TransactionStarted);
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
			this.Entities.TryGetValue(id, out var entity);
			return Task.FromResult(entity);
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
