using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     A factory for creating derived DbContext instances. This is used by the EF Core tools
///     to create a DbContext instance at design time (e.g., when creating migrations).
///     Since this is a class library and not an executable, the tools need a way to instantiate the context.
/// </summary>
public class RpgDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
	/// <summary>
	///     Creates a new instance of a derived context.
	/// </summary>
	/// <param name="args">Arguments provided by the design-time service.</param>
	/// <returns>An instance of <see cref="RpgDbContext" />.</returns>
	public RpgDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();

		// We can use a simple SQLite connection string here for design-time purposes.
		// The actual connection string used at runtime will be configured by the consumer application.
		optionsBuilder.UseSqlite("Data Source=design_time.db");

		return new RpgDbContext(optionsBuilder.Options);
	}
}
