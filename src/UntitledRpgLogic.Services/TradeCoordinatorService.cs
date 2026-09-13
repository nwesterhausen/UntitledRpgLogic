using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Economy;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Application coordinator service managing atomic exchanges of items and currency
///     between buyer and seller entities[cite: 2, 4].
/// </summary>
public sealed class TradeCoordinatorService : ITradeCoordinatorService
{
	private readonly ICurrencyStorageService currencyStorageService;
	private readonly IEntityRepository<Entity, Ulid> entityRepository;
	private readonly IItemStorageService itemStorageService;
	private readonly IUnitOfWork unitOfWork;

	/// <summary>
	///     Initializes a new instance of the <see cref="TradeCoordinatorService" /> class.
	/// </summary>
	/// <param name="unitOfWork">The transaction boundary coordinator[cite: 2, 4].</param>
	/// <param name="entityRepository">The repository for entity aggregates[cite: 1, 2].</param>
	/// <param name="itemStorageService">The pure domain service managing item placement and transfers[cite: 2, 3].</param>
	/// <param name="currencyStorageService">The domain service managing currency calculations and denominations[cite: 2, 3].</param>
	public TradeCoordinatorService(
		IUnitOfWork unitOfWork,
		IEntityRepository<Entity, Ulid> entityRepository,
		IItemStorageService itemStorageService,
		ICurrencyStorageService currencyStorageService)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
		this.itemStorageService = itemStorageService ?? throw new ArgumentNullException(nameof(itemStorageService));
		this.currencyStorageService =
			currencyStorageService ?? throw new ArgumentNullException(nameof(currencyStorageService));
	}

	/// <inheritdoc />
	public async Task<bool> ExecutePurchaseAsync(
		Ulid buyerEntityId,
		Ulid sellerEntityId,
		Ulid itemInstanceId,
		int quantity,
		CancellationToken cancellationToken = default)
	{
		if (quantity <= 0 || buyerEntityId == sellerEntityId)
		{
			return false;
		}

		var buyer = await this.LoadEntityWithInventoryAsync(buyerEntityId, cancellationToken).ConfigureAwait(false);
		var seller = await this.LoadEntityWithInventoryAsync(sellerEntityId, cancellationToken).ConfigureAwait(false);

		if (buyer?.Inventory is null || seller?.Inventory is null)
		{
			return false;
		}

		var itemToBuy = seller.Inventory.Items.FirstOrDefault(i => i.Id == itemInstanceId);
		if (itemToBuy is null || itemToBuy.Quantity < quantity)
		{
			return false;
		}

		var totalCost = (long)(itemToBuy.Definition?.BaseValue ?? 0) * quantity;
		if (totalCost > 0 && this.currencyStorageService.GetTotalValue(buyer.Inventory) < totalCost)
		{
			return false;
		}

		// Validate that the buyer can receive the purchased item
		var candidateTransfer = itemToBuy with { Quantity = quantity };
		if (!this.itemStorageService.CanStoreItem(buyer.Inventory, candidateTransfer))
		{
			return false;
		}

		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			if (totalCost > 0 && !this.ProcessPayment(buyer.Inventory, seller.Inventory, totalCost))
			{
				await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
				return false;
			}

			var itemMoved = this.itemStorageService.TryTransferItem(
				seller.Inventory,
				buyer.Inventory,
				itemInstanceId,
				quantity);

			if (!itemMoved)
			{
				await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
				return false;
			}

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

	private bool ProcessPayment(Inventory buyerInventory, Inventory sellerInventory, long totalCost)
	{
		var remainingCost = totalCost;

		// Consume buyer's currency items from lowest to highest denomination to satisfy payment
		var buyerCurrencies = buyerInventory.Items
			.Where(i => i.Definition?.ItemType == ItemType.Currency)
			.OrderBy(i => i.Definition?.BaseValue ?? 0)
			.ToList();

		foreach (var currencyItem in buyerCurrencies)
		{
			var def = currencyItem.Definition;
			var coinValue = def?.BaseValue ?? 0;
			if (coinValue <= 0)
			{
				continue;
			}

			var neededCount = (int)Math.Ceiling((double)remainingCost / coinValue);
			var toTake = Math.Min(neededCount, currencyItem.Quantity);

			if (this.currencyStorageService.TryWithdraw(buyerInventory, currencyItem.DefinitionId, toTake,
					out var withdrawn) && withdrawn is not null)
			{
				var transferredValue = (long)coinValue * toTake;
				remainingCost -= transferredValue;

				// Deposit payment into seller's inventory
				if (def is not null)
				{
					this.currencyStorageService.TryDeposit(sellerInventory, def, toTake);
				}
			}

			if (remainingCost <= 0)
			{
				break;
			}
		}

		return remainingCost <= 0;
	}

	private Task<Entity?> LoadEntityWithInventoryAsync(Ulid entityId, CancellationToken cancellationToken) =>
		this.entityRepository.GetByIdAsync(
			entityId,
			q => q.Include(e => e.Inventory)
				.ThenInclude(inv => inv!.Items)
				.ThenInclude(i => i.Definition),
			cancellationToken);
}
