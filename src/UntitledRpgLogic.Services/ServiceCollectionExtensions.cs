using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Services;

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
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		// SCOPED SERVICES
		// -- These are created each time they are asked for and disposed of with the creator.
		_ = services.AddScoped(typeof(ILevelingService<>), typeof(LevelingService<>));
		_ = services.AddScoped<IItemStorageService, ItemStorageService>();
		_ = services.AddScoped<ICurrencyStorageService, CurrencyStorageService>();

		// SINGLETON SERVICES
		// -- These are created the first time they're asked for and never disposed (until program ends)
		_ = services.AddSingleton<ICultureService, CultureService>();
		_ = services.AddSingleton<ISkillService, SkillService>();
		_ = services.AddSingleton<IStatService, StatService>();
		_ = services.AddSingleton<IDamageCalculator, DamageCalculator>();

		return services;
	}
}
