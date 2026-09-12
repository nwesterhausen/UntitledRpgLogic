using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data.SQLite;

/// <summary>
///     Contains extension methods for IServiceCollection to add SQLite data access services.
/// </summary>
public static class SqliteServiceCollectionsExtensions
{
	/// <summary>
	///     Registers the SQLite persistence provider and related database services into the service collection.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to which persistence services will be registered.</param>
	/// <param name="configure">
	///     An optional delegate used to configure <see cref="SqlitePersistenceOptions" /> such as the connection string and
	///     migration behavior.
	///     If <see langword="null" />, default SQLite options are used.
	/// </param>
	/// <returns>The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.</returns>
	public static IServiceCollection AddSqliteDataAccess(
		this IServiceCollection services,
		Action<SqlitePersistenceOptions>? configure = null)
	{
		var options = new SqlitePersistenceOptions();
		configure?.Invoke(options);

		// Register the DbContext
		_ = services.AddDbContext<RpgDbContext>(dbOptions =>
			dbOptions.UseSqlite(options.ConnectionString, b =>
					b.MigrationsAssembly("UntitledRpgLogic.Infrastructure.Data.SQLite"))
				.UseSnakeCaseNamingConvention());

		// Register the Unit of Work and Repositories
		_ = services.AddRpgCommonPersistence();

		// Register the initializer with the AutoMigrate flag evaluated
		services.AddScoped<IDatabaseInitializer>(sp =>
			new DatabaseInitializer(
				sp.GetRequiredService<RpgDbContext>(),
				options.AutoMigrate));

		return services;
	}
}
