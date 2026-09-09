using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Infrastructure.Data;

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
		var options = new DbContextOptionsBuilder<RpgDbContext>()
			.UseSqlite("Data Source=test_verification.db")
			.Options;

		var context = new RpgDbContext(options);
		await using (context.ConfigureAwait(false))
		{
			await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
			await context.Database.MigrateAsync().ConfigureAwait(false);

			var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
			Assert.IsTrue(canConnect);
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

		var options = new DbContextOptionsBuilder<RpgDbContext>()
			.UseNpgsql(connectionString, b => b.MigrationsAssembly("UntitledRpgLogic.Infrastructure.Data.PostgreSQL"))
			.Options;

		var context = new RpgDbContext(options);
		await using (context.ConfigureAwait(false))
		{
			// Reset the schema cleanly without dropping the database
			await context.Database.ExecuteSqlRawAsync("DROP SCHEMA public CASCADE; CREATE SCHEMA public;").ConfigureAwait(false);

			// Apply fresh migrations
			await context.Database.MigrateAsync().ConfigureAwait(false);

			var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
			Assert.IsTrue(canConnect);
		}
	}
}
