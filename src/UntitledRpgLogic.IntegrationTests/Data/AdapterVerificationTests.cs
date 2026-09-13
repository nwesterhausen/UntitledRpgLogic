using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Infrastructure.Data;
using UntitledRpgLogic.Infrastructure.Data.PostgreSQL;
using UntitledRpgLogic.Infrastructure.Data.SQLite;

namespace UntitledRpgLogic.IntegrationTests;

[TestClass]
public class AdapterVerificationTests
{
	private static string? GetPostgreSqlConnectionString()
	{
		var envConnection = Environment.GetEnvironmentVariable("URPG_PG_CONNECTION_STRING");
		return !string.IsNullOrWhiteSpace(envConnection) ? envConnection : null;
	}

	[TestMethod]
	public async Task CanConnectAndMigrateSqlite()
	{
		var dbFile = $"test_verification_{Guid.NewGuid():N}.db";
		var services = new ServiceCollection();

		services.AddSqliteDataAccess(opts =>
		{
			opts.ConnectionString = $"Data Source={dbFile}";
			opts.AutoMigrate = true;
		});

		var provider = services.BuildServiceProvider();
		await using (provider.ConfigureAwait(false))
		{
			var context = provider.GetRequiredService<RpgDbContext>();

			try
			{
				// Run migrations via IDatabaseInitializer
				var initializer = provider.GetRequiredService<IDatabaseInitializer>();
				await initializer.InitializeAsync().ConfigureAwait(false);

				var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
				Assert.IsTrue(canConnect);

				// Verify tables exist
				Assert.IsFalse(await context.Entities.AnyAsync().ConfigureAwait(false));

				// Verify round-trip persistence with custom converters (Name and Ulid)
				var testEntity = new Entity(Ulid.NewUlid())
				{
					Name = new Name("VerificationDummy", "VerificationDummies", "Dummy")
				};

				await context.Entities.AddAsync(testEntity).ConfigureAwait(false);
				await context.SaveChangesAsync().ConfigureAwait(false);

				var retrieved = await context.Entities.FirstOrDefaultAsync(e => e.Id == testEntity.Id)
					.ConfigureAwait(false);
				Assert.IsNotNull(retrieved);
				Assert.AreEqual("VerificationDummy", retrieved.Name.Singular);
			}
			finally
			{
				await context.Database.EnsureDeletedAsync().ConfigureAwait(false);

				if (File.Exists(dbFile))
				{
					File.Delete(dbFile);
				}
			}
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

			// Reset the public schema cleanly
			await context.Database.ExecuteSqlRawAsync("DROP SCHEMA public CASCADE; CREATE SCHEMA public;")
				.ConfigureAwait(false);

			// Run migrations via IDatabaseInitializer
			var initializer = provider.GetRequiredService<IDatabaseInitializer>();
			await initializer.InitializeAsync().ConfigureAwait(false);

			var canConnect = await context.Database.CanConnectAsync().ConfigureAwait(false);
			Assert.IsTrue(canConnect);

			// Verify tables migrated
			Assert.IsFalse(await context.Entities.AnyAsync().ConfigureAwait(false));

			// Verify PostgreSQL round-trip: confirms bytea (Ulid) and snake_case mapping work natively
			var testEntity = new Entity(Ulid.NewUlid())
			{
				Name = new Name("PostgresDummy", "PostgresDummies", "Dummy")
			};

			await context.Entities.AddAsync(testEntity).ConfigureAwait(false);
			await context.SaveChangesAsync().ConfigureAwait(false);

			var retrieved = await context.Entities.FirstOrDefaultAsync(e => e.Id == testEntity.Id)
				.ConfigureAwait(false);
			Assert.IsNotNull(retrieved);
			Assert.AreEqual("PostgresDummy", retrieved.Name.Singular);
		}
	}
}
