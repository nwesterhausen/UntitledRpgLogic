using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Interfaces.Data.Repositories;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Repositories;

/// <summary>
///     Implements a repository specifically for Entity objects, extending the generic repository.
/// </summary>
public class EntityRepository : Repository<Entity, Ulid>, IEntityRepository
{
	/// <inheritdoc />
	public EntityRepository(DbContext context) : base(context)
	{
	}

	/// <summary>
	///     A private property to access the DbContext as RpgDbContext.
	/// </summary>
	private RpgDbContext RpgDbContext => (RpgDbContext)this.Context;

	/// <inheritdoc />
	public async Task<Entity?> GetEntityWithInventoryAsync(Ulid id) =>
		await this.RpgDbContext.Entities
			.Include(e => e.Inventory) // 1. Load the Entity's Inventory
			.ThenInclude(i => i!.Items) // 2. Then, load the ItemInstances within that Inventory
			.ThenInclude(ii => ii.ItemDefinition) // 3. Then, for each ItemInstance, load its ItemDefinition
			.SingleOrDefaultAsync(e => e.Id == id)
			.ConfigureAwait(false);

	/// <inheritdoc />
	public Task<Entity?> GetEntityWithEquipmentAsync(Ulid id) => throw new NotImplementedException();
}
