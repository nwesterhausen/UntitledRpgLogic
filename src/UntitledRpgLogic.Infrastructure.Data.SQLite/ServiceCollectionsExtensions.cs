using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Infrastructure.Data.SQLite;

/// <summary>
///     Contains extension methods for IServiceCollection to add SQLite data access services.
/// </summary>
public static class ServiceCollectionsExtensions
{
	/// <summary>
	///     Adds SQLite data access services to the <see cref="IServiceCollection" />.
	///     Registers the <see cref="RpgDbContext" />, <see cref="IUnitOfWork" />, and repositories.
	///     Call this method in your application's Startup or Program file.
	/// </summary>
	/// <param name="services">The service collection to add services to.</param>
	/// <param name="connectionString">The SQLite connection string.</param>
	/// <returns>The updated <see cref="IServiceCollection" />.</returns>
	public static IServiceCollection AddSqliteDataAccess(this IServiceCollection services, string connectionString)
	{
		// 1. Register the DbContext
		services.AddDbContext<RpgDbContext>(options =>
			options.UseSqlite(connectionString));

		// 2. Register the Unit of Work and Repositories
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		//services.AddScoped<IEntityRepository, EntityRepository>();

		return services;
	}
}
