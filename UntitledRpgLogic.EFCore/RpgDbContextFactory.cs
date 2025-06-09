using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UntitledRpgLogic.EFCore;

/// <summary>
///     Factory class for creating instances of <see cref="RpgDbContext" /> at design time. Allows for creating migrations
///     that
///     can be used by any application that uses this database context. (Regardless of the database provider used.)
/// </summary>
public class RpgDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
    /// <inheritdoc />
    public RpgDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
        // Use your actual connection string here
        optionsBuilder.UseSqlite("Data Source=rpg_world_reference.db");
        return new RpgDbContext(optionsBuilder.Options);
    }
}