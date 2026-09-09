using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Interfaces.Data.Repositories;
using UntitledRpgLogic.Infrastructure.Data.Repositories;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
/// 	Defines the shared service collection extensions used by data adapters.
/// </summary>
public static class CommonServiceCollectionExtensions
{
	/// <summary>
	/// 	Register <see cref="IUnitOfWork" /> and all respositories with an existing <see cref="RpgDbContext" />
	/// </summary>
	public static IServiceCollection AddRpgCommonPersistence(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped(typeof(IEntityRepository), typeof(EntityRepository));
		services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

		return services;
	}
}
