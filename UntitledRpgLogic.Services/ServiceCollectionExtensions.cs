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
        // Register the open generic leveling service.
        // The DI container will automatically create a LevelingService<T> for any T that is requested.
        // For example, it will create a LevelingService<ISkill> when the SkillService requests it.
        services.AddScoped(typeof(ILevelingService<>), typeof(LevelingService<>));

        // Register the concrete services that depend on the generic service.
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IStatService, StatService>();
        services.AddScoped<IDamageCalculator, DamageCalculator>();

        return services;
    }
}
