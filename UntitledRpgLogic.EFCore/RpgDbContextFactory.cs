using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UntitledRpgLogic.EFCore;

public class RpgDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
    public RpgDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
        // Use your actual connection string here
        optionsBuilder.UseSqlite("Data Source=rpg_world_reference.db");
        return new RpgDbContext(optionsBuilder.Options);
    }
}