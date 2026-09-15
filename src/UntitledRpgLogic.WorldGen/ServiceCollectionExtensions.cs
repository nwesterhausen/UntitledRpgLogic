using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;

namespace UntitledRpgLogic.WorldGen;

/// <summary>
///     Provides extension methods for registering application services with the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	///     Registers world generation services.
	/// </summary>
	public static IServiceCollection AddWorldGenServices(this IServiceCollection services)
	{
		// World Gen Services
		services.AddSingleton<IChunkGeneratorService, ChunkGeneratorService>();
		services.AddSingleton<IWorldGenContextProvider, WorldGenContextProvider>();

		return services;
	}
}
