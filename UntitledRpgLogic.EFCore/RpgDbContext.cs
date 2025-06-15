using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Models;

namespace UntitledRpgLogic.EFCore;

/// <summary>
///     The database context for UntitledRpgLogic. This class is used to interact with the database and manage it.
/// </summary>
/// <param name="options">configuration options for the database</param>
public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
    /// <summary>
    ///     A table for storing definitions of stats used in the game.
    /// </summary>
    public DbSet<StatDefinition> StatDefinitions { get; set; }

    /// <summary>
    ///     A table for storing definitions of skills used in the game.
    /// </summary>
    public DbSet<SkillDefinition> SkillDefinitions { get; set; }

    /// <summary>
    ///     A table for linking stats together, allowing for complex relationships between stats.
    /// </summary>
    public DbSet<LinkedStats> LinkedStats { get; set; }

    /// <summary>
    ///     A table containing the effects of modifications applied to stats or skills.
    /// </summary>
    public DbSet<ModificationEffect> ModificationEffects { get; set; }

    /// <summary>
    ///     A table for storing definitions of modifiers that can be applied to stats or skills.
    /// </summary>
    public DbSet<ModifierDefinition> ModifierDefinitions { get; set; }

    /// <summary>
    ///     A table for storing actual instances of skills which would be attached to an entity (like a player or NPC).
    /// </summary>
    public DbSet<InstancedSkill> InstancedSkills { get; set; }

    /// <summary>
    ///     A table for storing actual instances of stats which would be attached to an entity (like a player or NPC).
    /// </summary>
    public DbSet<InstancedStat> InstancedStats { get; set; }

    /// <summary>
    ///     A table for storing entities in the game, such as players or NPCs.
    /// </summary>
    public DbSet<Entity> Entities { get; set; }

    /// <summary>
    ///     A table connecting entities to their skills. No skill would be attached to multiple entities, but an entity can
    ///     have multiple skills.
    /// </summary>
    public DbSet<EntitySkills> EntitySkills { get; set; }

    /// <summary>
    ///     A table connecting entities to their stats. No stat would be attached to multiple entities, but an entity can have
    ///     multiple stats.
    /// </summary>
    public DbSet<EntityStats> EntityStats { get; set; }
    // Add other DbSets as needed

    /// <inheritdoc />
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
