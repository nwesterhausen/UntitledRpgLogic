using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Infrastructure.Data.Repositories;

/// <summary>
///     Provides a generic, type-safe implementation of the IRepository interface using Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this repository manages.</typeparam>
/// <typeparam name="TId">The type of the primary key for the entity.</typeparam>
public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IDbEntity<TId> where TId : notnull
{
	/// <summary>
	///     Gets the database context.
	/// </summary>
	protected DbContext Context { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Repository{TEntity, TId}"/> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public Repository(DbContext context) => this.Context = context;

	/// <inheritdoc />
	public ValueTask<TEntity?> GetByIdAsync(TId id) => this.Context.Set<TEntity>().FindAsync(id);

	/// <inheritdoc />
	public async Task<IEnumerable<TEntity>> GetAllAsync() => await this.Context.Set<TEntity>().ToListAsync().ConfigureAwait(false);

	/// <inheritdoc />
	public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
		await this.Context.Set<TEntity>().Where(predicate).ToListAsync().ConfigureAwait(false);

	/// <inheritdoc />
	public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
		await this.Context.Set<TEntity>().SingleOrDefaultAsync(predicate).ConfigureAwait(false);

	/// <inheritdoc />
	public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) =>
		await this.Context.Set<TEntity>().AnyAsync(predicate).ConfigureAwait(false);

	/// <inheritdoc />
	public async Task AddAsync(TEntity entity) => await this.Context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);

	/// <inheritdoc />
	public async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
		await this.Context.Set<TEntity>().AddRangeAsync(entities).ConfigureAwait(false);

	/// <inheritdoc />
	public void Remove(TEntity entity) => this.Context.Set<TEntity>().Remove(entity);

	/// <inheritdoc />
	public void RemoveRange(IEnumerable<TEntity> entities) => this.Context.Set<TEntity>().RemoveRange(entities);
}
