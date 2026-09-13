using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.Infrastructure.Data.SQLite;
using UntitledRpgLogic.Services;

namespace UntitledRpgLogic.IntegrationTests.Common;

/// <summary>
///     Base fixture providing ephemeral SQLite instances and clean DI container scopes for integration tests.
/// </summary>
public abstract class CoordinatorIntegrationTestBase
{
	public TestContext TestContext { get; set; } = null!;

	protected static ServiceProvider CreateTestProvider(string dbName)
	{
		var services = new ServiceCollection();

		services.AddSqliteDataAccess(opts =>
		{
			opts.ConnectionString = $"Data Source={dbName}.db";
			opts.AutoMigrate = true;
		});

		// Registers all domain services, persistence coordinators, and session registries
		services.AddRpgServerServices();

		return services.BuildServiceProvider();
	}

	protected static async Task InitializeDatabaseAsync(IServiceProvider provider, CancellationToken ct)
	{
		var initializer = provider.GetRequiredService<IDatabaseInitializer>();
		await initializer.InitializeAsync(ct).ConfigureAwait(false);
	}

	protected static async Task CleanupDatabaseAsync(IServiceProvider provider, string dbName)
	{
		var context = provider.GetRequiredService<RpgDbContext>();
		await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

		var fullPath = $"{dbName}.db";
		if (File.Exists(fullPath))
		{
			File.Delete(fullPath);
		}
	}
}
