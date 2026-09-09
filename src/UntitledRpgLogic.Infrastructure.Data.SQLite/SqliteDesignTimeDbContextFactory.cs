using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UntitledRpgLogic.Infrastructure.Data.SQLite;

/// <summary>
/// 	Design time DB context factory.
/// </summary>
public sealed class SqliteDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
	/// <inheritdoc />
	public RpgDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
		optionsBuilder.UseSqlite(
			"Data Source=design_time.db",
			b => b.MigrationsAssembly(typeof(SqliteDesignTimeDbContextFactory).Assembly.FullName));

		return new RpgDbContext(optionsBuilder.Options);
	}
}
