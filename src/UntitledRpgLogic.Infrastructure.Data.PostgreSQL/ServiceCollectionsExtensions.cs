using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Infrastructure.Data.PostgreSQL;

/// <summary>
///     Contains extension methods for IServiceCollection to add PostgreSQL data access services.
/// </summary>
public static class ServiceCollectionsExtensions
{
	/// <summary>
	///     Adds PostgreSQL data access services to the <see cref="IServiceCollection" />.
	///     Registers the <see cref="RpgDbContext" />, <see cref="IUnitOfWork" />, and repositories.
	///     Call this method in your application's Startup or Program file.
	/// </summary>
	/// <param name="services">The service collection to add services to.</param>
	/// <param name="connectionString">The PostgreSQL connection string.</param>
	/// <returns>The updated <see cref="IServiceCollection" />.</returns>
	public static IServiceCollection AddPostgresDataAccess(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<RpgDbContext>(options =>
			options.UseNpgsql(connectionString)
				.UseSnakeCaseNamingConvention()); // <-- Only this line changes

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		//services.AddScoped<IEntityRepository, EntityRepository>();

		return services;
	}
}
