using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UntitledRpgLogic.Infrastructure.Data.PostgreSQL;

/// <summary>
///     Design-time factory used by the EF Core CLI tools when running migrations against PostgreSQL.
/// </summary>
public sealed class PostgreSqlDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
	private const string DefaultLocalConnectionString =
		"Host=localhost;Port=5432;Database=urpg_dev;Username=postgres;Password=postgres";

	/// <inheritdoc />
	public RpgDbContext CreateDbContext(string[] args)
	{
		// 1. Check if passed as a command-line argument: dotnet ef ... -- "Host=..."
		var connectionString = args is { Length: > 0 } && !string.IsNullOrWhiteSpace(args[0])
			? args[0]
			: Environment.GetEnvironmentVariable("URPG_PG_CONNECTION_STRING");

		// 2. Fall back to local default if neither is specified
		if (string.IsNullOrWhiteSpace(connectionString))
		{
			connectionString = DefaultLocalConnectionString;
		}

		var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
		optionsBuilder.UseNpgsql(
			connectionString, b =>
				b.MigrationsAssembly("UntitledRpgLogic.Infrastructure.Data.PostgreSQL"))
				.UseSnakeCaseNamingConvention();

		return new RpgDbContext(optionsBuilder.Options);
	}
}
