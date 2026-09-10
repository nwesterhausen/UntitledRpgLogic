using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.Infrastructure.Data.PostgreSQL;
using UntitledRpgLogic.Infrastructure.Data.SQLite;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class AdapterVerificationTests
{
	private static string? GetPostgreSqlConnectionString()
	{
		// 1. Check environment variable first
		var envConnection = Environment.GetEnvironmentVariable("URPG_PG_CONNECTION_STRING");
		if (!string.IsNullOrWhiteSpace(envConnection))
		{
			return envConnection;
		}

		// 2. Optional: return null if not running in an environment with your DB server
		return null;
	}

	[TestMethod]
	public async Task CanConnectAndMigrateSqlite()
	{
		var services = new ServiceCollection();

		services.AddSqliteDataAccess(opts =>
		{
			opts.ConnectionString = "Data Source=test_verification.db";
			opts.AutoMigrate = true;
		});

		var provider = services.BuildServiceProvider();
		await using (provider.ConfigureAwait(false))
		{
			var context = provider.GetRequiredService<RpgDbContext>();
			await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

			// Uses IDatabaseInitializer or context.Database.MigrateAsync directly
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
			Assert.IsTrue(canConnect);

			// Verifies the schema actually created tables
			Assert.IsFalse(await context.Entities.AnyAsync().ConfigureAwait(false));
		}
	}
	[TestMethod]
	public async Task CanConnectAndMigratePostgreSql()
	{
		var connectionString = GetPostgreSqlConnectionString();
		if (string.IsNullOrWhiteSpace(connectionString))
		{
			Assert.Inconclusive("Skipping PostgreSQL test: 'URPG_PG_CONNECTION_STRING' is not set.");
		}

		var services = new ServiceCollection();

		services.AddPostgreSqlDataAccess(opts =>
		{
			opts.ConnectionString = connectionString;
			opts.AutoMigrate = true;
		});

		var provider = services.BuildServiceProvider();
		await using (provider.ConfigureAwait(false))
		{
			var context = provider.GetRequiredService<RpgDbContext>();

			// Reset the schema cleanly without dropping the database
			await context.Database.ExecuteSqlRawAsync("DROP SCHEMA public CASCADE; CREATE SCHEMA public;").ConfigureAwait(false);

			// Run migrations via IDatabaseInitializer or context.Database.MigrateAsync directly
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
			Assert.IsTrue(canConnect);

			// Verify that the tables actually migrated
			Assert.IsFalse(await context.Entities.AnyAsync().ConfigureAwait(false));
		}
	}
}
