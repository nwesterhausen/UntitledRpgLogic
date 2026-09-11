using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Economy;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Localization;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Provides extension methods for registering application services with the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	///     Adds all the core application logic services to the specified IServiceCollection.
	/// </summary>
	/// <param name="services">The IServiceCollection to add the services to.</param>
	/// <returns>The IServiceCollection so that additional calls can be chained.</returns>
	public static IServiceCollection AddRpgServices(this IServiceCollection services)
	{
		// Pure domain services (Stateless, can be Singleton or Transient)
		services.AddSingleton<IDamageCalculator, DamageCalculator>();
		services.AddSingleton<IItemFactoryService, ItemFactoryService>();

		// Domain storage services (Operate directly on hydrated records)
		services.AddScoped<IItemStorageService, ItemStorageService>();
		services.AddScoped<ICurrencyStorageService, CurrencyStorageService>();

		// Application coordination services (Depend on IUnitOfWork & IEntityRepository)
		services.AddScoped<IItemCatalogService, ItemCatalogService>();

		return services;
	}
}
