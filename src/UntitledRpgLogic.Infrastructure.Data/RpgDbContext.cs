using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.ValueConverters;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     Represents the database context for the RPG application, providing access to all game data.
/// </summary>
public class RpgDbContext : DbContext
{
	/// <summary>
	///     Initializes a new instance of the <see cref="RpgDbContext" /> class.
	/// </summary>
	/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
	public RpgDbContext(DbContextOptions<RpgDbContext> options) : base(options)
	{
	}

	// Core Game Data Definitions
	/// <summary>
	///     Table for item definitions, which are the "blueprints" for items in the game.
	/// </summary>
	public DbSet<ItemDefinition> ItemDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for material definitions, which define the materials that items can be made from.
	/// </summary>
	public DbSet<MaterialDefinition> MaterialDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for stat definitions, which define the various stats that entities can have.
	/// </summary>
	public DbSet<StatDefinition> StatDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for skill definitions, which define the skills that entities can possess.
	/// </summary>
	public DbSet<SkillDefinition> SkillDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for modifier definitions, which define how stats and skills can be modified.
	/// </summary>
	public DbSet<ModifierDefinition> ModifierDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for modification effects, which define the specific effects of modifiers.
	/// </summary>
	public DbSet<ModificationEffect> ModificationEffects { get; set; } = null!;

	// Instanced and World Data
	/// <summary>
	///     Table for entities, which represent characters, NPCs, mobs, etc. in the game world.
	/// </summary>
	public DbSet<Entity> Entities { get; set; } = null!;

	/// <summary>
	///     Table for item instances, which are specific instances of item definitions owned by entities.
	/// </summary>
	public DbSet<ItemInstance> ItemInstances { get; set; } = null!;

	/// <summary>
	///     Table for instanced stats, which are specific instances of stat definitions assigned to entities.
	/// </summary>
	public DbSet<InstancedStat> InstancedStats { get; set; } = null!;

	/// <summary>
	///     Table for instanced skills, which are specific instances of skill definitions assigned to entities.
	/// </summary>
	public DbSet<InstancedSkill> InstancedSkills { get; set; } = null!;

	/// <summary>
	///     Table for applied modifiers, which represent modifiers that have been applied to stats or skills.
	/// </summary>
	public DbSet<AppliedModifier> AppliedModifiers { get; set; } = null!;

	// Linking Tables
	/// <summary>
	///     Table for linked stats, which define relationships between different stats (e.g., one stat affecting another).
	/// </summary>
	public DbSet<LinkedStats> LinkedStats { get; set; } = null!;

	/// <summary>
	///     Table for entity inventories, which link entities to the item instances they own.
	/// </summary>
	public DbSet<EntityInventory> EntityInventories { get; set; } = null!;

	/// <summary>
	///     Table for entity stats, which link entities to their instanced stats.
	/// </summary>
	public DbSet<EntityStats> EntityStats { get; set; } = null!;

	/// <summary>
	///     Table for entity skills, which link entities to their instanced skills.
	/// </summary>
	public DbSet<EntitySkills> EntitySkills { get; set; } = null!;

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

		// Configure composite primary keys for linking tables
		modelBuilder.Entity<EntityInventory>().HasKey(ei => new { ei.EntityId, ei.ItemInstanceId });
		modelBuilder.Entity<EntityStats>().HasKey(es => new { es.EntityId, es.InstancedStatId });
		modelBuilder.Entity<EntitySkills>().HasKey(es => new { es.EntityId, es.InstancedSkillId });
		modelBuilder.Entity<LinkedStats>().HasKey(ls => new { ls.DependentStatId, ls.LinkedStatId });

		// Configure relationships
		modelBuilder.Entity<ItemInstance>()
			.HasOne(i => i.ItemDefinition)
			.WithMany()
			.HasForeignKey(i => i.ItemDefinitionId);

		modelBuilder.Entity<ItemInstance>()
			.HasOne(i => i.PrimaryMaterial)
			.WithMany()
			.HasForeignKey(i => i.PrimaryMaterialId);

		modelBuilder.Entity<LinkedStats>()
			.HasOne(ls => ls.DependentStat)
			.WithMany() // StatDefinition does not have a collection of LinkedStats, so this is empty.
			.HasForeignKey(ls => ls.DependentStatId)
			.OnDelete(DeleteBehavior.Restrict); // Prevent deleting a StatDefinition if it's in use.

		modelBuilder.Entity<LinkedStats>()
			.HasOne(ls => ls.LinkedStat)
			.WithMany()
			.HasForeignKey(ls => ls.LinkedStatId)
			.OnDelete(DeleteBehavior.Restrict);

		// Note: Additional relationship configurations will be needed here as the system grows.
	}

	/// <inheritdoc />
	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		ArgumentNullException.ThrowIfNull(configurationBuilder, nameof(configurationBuilder));

		// This is where the value converters are registered.
		// This convention tells EF Core to use our custom converter for every property of type Ulid.
		configurationBuilder.Properties<Ulid>()
			.HaveConversion<UlidToBytesConverter>();

		// This convention tells EF Core to use our custom converter for every property of type Name.
		configurationBuilder.Properties<Name>()
			.HaveConversion<NameToSimpleStringConverter>();

		base.ConfigureConventions(configurationBuilder);
	}
}
