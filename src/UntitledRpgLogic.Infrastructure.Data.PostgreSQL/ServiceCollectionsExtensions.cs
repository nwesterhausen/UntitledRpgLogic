using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Infrastructure.Data.PostgreSQL;

/// <inheritdoc />
public record PostgreSqlPersistenceOptions : PersistenceOptions;

/// <summary>
///     Contains extension methods for IServiceCollection to add PostgreSQL data access services.
/// </summary>
public static class PostgreSqlServiceCollectionsExtensions
{

	/// <summary>
	///     Registers the PostgreSQL persistence provider and related database services into the service collection.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to which persistence services will be registered.</param>
	/// <param name="configure">
	///     An optional delegate used to configure <see cref="PostgreSqlPersistenceOptions" /> such as the connection string and migration behavior.
	///     If <see langword="null" />, default PostgreSQL options are used.
	/// </param>
	/// <returns>The same <see cref="IServiceCollection" /> instance so that additional calls can be chained.</returns>
	public static IServiceCollection AddPostgreSqlDataAccess(
		this IServiceCollection services,
		Action<PostgreSqlPersistenceOptions>? configure = null)
	{
		var options = new PostgreSqlPersistenceOptions();
		configure?.Invoke(options);

		// Register the DbContext
		_ = services.AddDbContext<RpgDbContext>(dbOptions =>
			dbOptions.UseNpgsql(options.ConnectionString, b =>
				b.MigrationsAssembly("UntitledRpgLogic.Infrastructure.Data.PostgreSQL"))
				.UseSnakeCaseNamingConvention());

		// Register the Unit of Work and Repositories
		_ = services.AddRpgCommonPersistence();

		services.AddScoped<IDatabaseInitializer>(sp =>
			new DatabaseInitializer(
				sp.GetRequiredService<RpgDbContext>(),
				options.AutoMigrate));

		return services;
	}
}
