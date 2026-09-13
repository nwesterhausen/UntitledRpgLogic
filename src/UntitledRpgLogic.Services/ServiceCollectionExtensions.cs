using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Economy;
using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Networking;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Provides extension methods for registering application services with the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	///     Registers pure domain calculation services used by both client and server.
	/// </summary>
	public static IServiceCollection AddRpgCoreDomainServices(this IServiceCollection services)
	{
		// Pure domain services (Stateless, zero I/O)
		services.AddSingleton<IItemFactoryService, ItemFactoryService>();
		services.AddSingleton<IEffectApplicationService, EffectApplicationService>();
		services.AddSingleton<ISkillProgressionService, SkillProgressionService>();
		services.AddSingleton<IStatCalculationService, StatCalculationService>();
		services.AddSingleton<IModifierApplicationService, ModifierApplicationService>();
		services.AddSingleton<IAbilityValidationService, AbilityValidationService>();
		services.AddSingleton<ISpatialMathService, SpatialMathService>();
		services.AddSingleton<IRespirationDomainService, RespirationDomainService>();

		// Supporting services
		services.AddSingleton<IRandom, Random>();

		// Domain storage mutations on hydrated records
		services.AddScoped<IItemStorageService, ItemStorageService>();
		services.AddScoped<ICurrencyStorageService, CurrencyStorageService>();

		return services;
	}

	/// <summary>
	///     Registers authoritative server coordinators, background tick handlers, and session services.
	/// </summary>
	public static IServiceCollection AddRpgServerServices(this IServiceCollection services)
	{
		services.AddRpgCoreDomainServices();

		// Server Application / Persistence Coordinators (Require IUnitOfWork & IEntityRepository)
		services.AddScoped<IItemCatalogService, ItemCatalogService>();
		services.AddScoped<IInventoryCoordinatorService, InventoryCoordinatorService>();
		services.AddScoped<IPackageLoaderService, PackageLoaderService>();
		services.AddScoped<IAbilityCoordinatorService, AbilityCoordinatorService>();
		services.AddScoped<IProgressionCoordinatorService, ProgressionCoordinatorService>();
		services.AddScoped<IWorldCoordinatorService, WorldCoordinatorService>();
		services.AddScoped<ITradeCoordinatorService, TradeCoordinatorService>();

		// Server Networking & Sessions
		services.AddSingleton<IAreaOfInterestService, AreaOfInterestService>();
		services.AddSingleton<IPlayerSessionService, PlayerSessionService>();

		return services;
	}

	/// <summary>
	///     Registers client-side services (prediction, interpolation, and definition caching).
	/// </summary>
	public static IServiceCollection AddRpgClientServices(this IServiceCollection services)
	{
		services.AddRpgCoreDomainServices();

		// Client-Specific Registries
		services.AddSingleton<IClientDefinitionRegistry, ClientDefinitionRegistry>();

		return services;
	}
}
