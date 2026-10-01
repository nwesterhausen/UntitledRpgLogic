using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Infrastructure.Data.SQLite;
using UntitledRpgLogic.Networking;
using UntitledRpgLogic.Services;
using UntitledRpgLogic.WorldGen;

namespace UntitledRpgLogic.Extensions.Godot;

public static class GodotServiceCollectionExtensions
{
	public static IServiceCollection AddGodotRpgEngine(this IServiceCollection services, string dbPath)
	{
		// Add the core logic domains using the existing extensions
		services.AddRpgClientServices(); // "From UntitledRpgLogic.Services
		services.AddRpgServerServices(); // "
		services.AddWorldGenServices(); // From UntitledRpgLogic.WorldGen
		services.AddNetworkingServices(); // From UntitledRpgLogic.Networking

		// Configure SQLite with a Godot-friendly path
		services.AddSqliteDataAccess(options =>
		{
			options.ConnectionString = $"Data Source={dbPath}";
			options.AutoMigrate = true;
		});

		// Register Godot-specific implementations of your Core interfaces
		// services.AddSingleton<ISpatialMathService, GodotSpatialMathAdapter>();
		// services.AddLogging(builder => builder.AddGodotLogger());

		return services;
	}
}
