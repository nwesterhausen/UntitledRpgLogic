using System.Linq.Expressions;

namespace UntitledRpgLogic.Core.Interfaces.Data.Repositories;

/// <summary>
///     Specialized repository contract for root entities exposing a standard <see cref="IDbEntity{TId}"/> key.
/// </summary>
/// <typeparam name="TEntity">The concrete entity type.</typeparam>
/// <typeparam name="TId">The identifier type (e.g., <see cref="Ulid"/>).</typeparam>
public interface IEntityRepository<TEntity, TId> : IRepository<TEntity>
	where TEntity : class, IDbEntity<TId>
{
	/// <summary>
	///     Retrieves an entity by its primary key identifier, optionally including related navigation paths.
	/// </summary>
	Task<TEntity?> GetByIdAsync(
		TId id,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes);

	/// <summary>
	///     Retrieves multiple entities by their primary key identifiers.
	/// </summary>
	Task<IReadOnlyList<TEntity>> GetByIdsAsync(
		IEnumerable<TId> ids,
		CancellationToken cancellationToken = default,
		params Expression<Func<TEntity, object?>>[] includes);
}
