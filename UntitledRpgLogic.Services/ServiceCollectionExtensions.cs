using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces;

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
        services.AddScoped(typeof(ILevelingService<>), typeof(LevelingService<>));

        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IStatService, StatService>();
        services.AddScoped<IDamageCalculator, DamageCalculator>();
        services.AddScoped<IItemStorageService, ItemStorageService>();
        services.AddScoped<ICurrencyStorageService, CurrencyStorageService>();

        return services;
    }
}
