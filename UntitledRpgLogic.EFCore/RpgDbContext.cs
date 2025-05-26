using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UntitledRpgLogic.Models;

namespace UntitledRpgLogic.EntityFramework;

public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
    public DbSet<StatModel>? Stats { get; set; }
    public DbSet<InstancedStatModel>? InstancedStats { get; set; }
    public DbSet<EntityModel>? Entities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstancedStatModel>()
            .HasKey(x => new { x.EntityId, x.StatId });
    }
}

public class RpgDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
    public RpgDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
        optionsBuilder.UseSqlite("Data Source=rpg.db");
        return new RpgDbContext(optionsBuilder.Options);
    }
}