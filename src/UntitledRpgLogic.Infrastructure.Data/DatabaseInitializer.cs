using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     Provides schema initialization and automated migration execution for <see cref="RpgDbContext" />.
/// </summary>
public sealed class DatabaseInitializer : IDatabaseInitializer
{
	private readonly bool _autoMigrate;
	private readonly RpgDbContext _context;

	/// <summary>
	///     Initializes a new instance of the <see cref="DatabaseInitializer" /> class.
	/// </summary>
	/// <param name="context">The database context used to apply migrations.</param>
	/// <param name="autoMigrate">
	///     Indicates whether pending migrations should automatically be executed when <see cref="InitializeAsync" /> is
	///     invoked.
	///     Defaults to <see langword="true" />.
	/// </param>
	public DatabaseInitializer(RpgDbContext context, bool autoMigrate = true)
	{
		this._context = context ?? throw new ArgumentNullException(nameof(context));
		this._autoMigrate = autoMigrate;
	}

	/// <inheritdoc />
	public async Task InitializeAsync(CancellationToken cancellationToken = default)
	{
		if (this._autoMigrate)
		{
			await this._context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
		}

		var migrations = this._context.Database.GetMigrations();

		if (migrations.Any())
		{
			// Apply migrations if migration history is present
			await this._context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
		}
		else
		{
			// Fall back to direct schema creation if no migrations are scaffolded for this provider
			await this._context.Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
