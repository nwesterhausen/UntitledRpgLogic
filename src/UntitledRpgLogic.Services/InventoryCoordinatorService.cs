using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Services;

/// <inheritdoc />
public sealed class InventoryCoordinatorService : IInventoryCoordinatorService
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IEntityRepository<Entity, Ulid> entityRepository;
	private readonly IItemStorageService storageService;

	/// <summary>
	///		Creates the Inventory coordinator service
	/// </summary>
	/// <param name="unitOfWork"></param>
	/// <param name="entityRepository"></param>
	/// <param name="storageService"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public InventoryCoordinatorService(
		IUnitOfWork unitOfWork,
		IEntityRepository<Entity, Ulid> entityRepository,
		IItemStorageService storageService)
	{
		this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
		this.storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
	}

	/// <inheritdoc />
	public async Task<bool> StoreItemInEntityInventoryAsync(
		Ulid entityId,
		Item item,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(item);

		var entity = await this.entityRepository.GetByIdAsync(
			entityId,
			q => q.Include(e => e.Inventory)
				  .ThenInclude(inv => inv!.Items)
				  .ThenInclude(i => i.Definition),
			cancellationToken).ConfigureAwait(false);

		if (entity?.Inventory is null)
		{
			return false;
		}

		if (!this.storageService.TryStoreItem(entity.Inventory, item))
		{
			return false;
		}

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		return true;
	}

	/// <inheritdoc />
	public async Task<Item?> RemoveItemFromEntityInventoryAsync(
		Ulid entityId,
		Ulid itemId,
		int quantity,
		CancellationToken cancellationToken = default)
	{
		var entity = await this.entityRepository.GetByIdAsync(
			entityId,
			q => q.Include(e => e.Inventory)
				  .ThenInclude(inv => inv!.Items),
			cancellationToken).ConfigureAwait(false);

		if (entity?.Inventory is null)
		{
			return null;
		}

		if (!this.storageService.TryRemoveItem(entity.Inventory, itemId, quantity, out var removedItem))
		{
			return null;
		}

		await this.unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		return removedItem;
	}

	/// <inheritdoc />
	public async Task<bool> TransferItemBetweenEntitiesAsync(
		Ulid sourceEntityId,
		Ulid targetEntityId,
		Ulid itemId,
		int quantity,
		CancellationToken cancellationToken = default)
	{
		await this.unitOfWork.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

		try
		{
			var source = await this.entityRepository.GetByIdAsync(
				sourceEntityId,
				q => q.Include(e => e.Inventory)
					  .ThenInclude(inv => inv!.Items)
					  .ThenInclude(i => i.Definition),
				cancellationToken).ConfigureAwait(false);

			var target = await this.entityRepository.GetByIdAsync(
				targetEntityId,
				q => q.Include(e => e.Inventory)
					  .ThenInclude(inv => inv!.Items)
					  .ThenInclude(i => i.Definition),
				cancellationToken).ConfigureAwait(false);

			if (source?.Inventory is null || target?.Inventory is null)
			{
				await this.unitOfWork.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
				return false;
			}

			var success = this.storageService.TryTransferItem(source.Inventory, target.Inventory, itemId, quantity);
			if (!success)
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
}
