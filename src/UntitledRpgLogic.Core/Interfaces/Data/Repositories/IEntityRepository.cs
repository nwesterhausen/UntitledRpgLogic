using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Data.Repositories;

/// <summary>
///     Represents a repository specifically for Entity objects, extending the generic repository.
/// </summary>
public interface IEntityRepository : IRepository<Entity, Ulid>
{
	/// <summary>
	///     Retrieves an entity along with its associated inventory and item definitions.
	/// </summary>
	/// <param name="id">The unique identifier of the entity.</param>
	/// <returns>The task result contains the entity if found; otherwise, null.</returns>
	public Task<Entity?> GetEntityWithInventoryAsync(Ulid id);

	/// <summary>
	///     Retrieves an entity along with its associated equipment and item definitions.
	/// </summary>
	/// <param name="id">The unique identifier of the entity.</param>
	/// <returns>The task result contains the entity if found; otherwise, null.</returns>
	public Task<Entity?> GetEntityWithEquipmentAsync(Ulid id);
}
