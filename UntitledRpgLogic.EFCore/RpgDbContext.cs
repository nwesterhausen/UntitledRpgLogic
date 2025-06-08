using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Models;

namespace UntitledRpgLogic.EFCore;

/// <summary>
///     The database context for UntitledRpgLogic. This class is used to interact with the database and manage it.
/// </summary>
/// <param name="options">configuration options for the database</param>
public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
    public DbSet<StatDefinition> StatDefinitions { get; set; }
    public DbSet<SkillDefinition> SkillDefinitions { get; set; }
    public DbSet<LinkedStats> LinkedStats { get; set; }
    public DbSet<ModificationEffect> ModificationEffects { get; set; }
    public DbSet<ModifierDefinition> ModifierDefinitions { get; set; }
    public DbSet<InstancedSkill> InstancedSkills { get; set; }
    public DbSet<InstancedStat> InstancedStats { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<EntitySkills> EntitySkills { get; set; }

    public DbSet<EntityStats> EntityStats { get; set; }
    // Add other DbSets as needed

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite keys
        modelBuilder.Entity<LinkedStats>()
            .HasKey(ls => new { ls.DependentStatId, ls.LinkedStatId });

        modelBuilder.Entity<EntitySkills>()
            .HasKey(es => new { es.EntityId, es.InstancedSkillId });

        modelBuilder.Entity<EntityStats>()
            .HasKey(es => new { es.EntityId, es.InstancedStatId });

        // Explicit relationships for LinkedStats
        modelBuilder.Entity<LinkedStats>()
            .HasOne(ls => ls.DependentStat)
            .WithMany(sd => sd.LinkedStats)
            .HasForeignKey(ls => ls.DependentStatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LinkedStats>()
            .HasOne(ls => ls.LinkedStat)
            .WithMany()
            .HasForeignKey(ls => ls.LinkedStatId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}