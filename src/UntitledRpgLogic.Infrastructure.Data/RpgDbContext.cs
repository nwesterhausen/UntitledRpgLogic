using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Elements;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Infrastructure.Data.ValueConverters;

namespace UntitledRpgLogic.Infrastructure.Data;

/// <summary>
///     Represents the database context for the RPG application, providing access to all game data.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="RpgDbContext" /> class.
/// </remarks>
/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
	/// <summary>
	///     Gets or sets the DbSet for all Ability definitions.
	/// </summary>
	public DbSet<Ability> Abilities { get; set; } = null!;


	/// <summary>
	///     Table for applied modifiers, which represent modifiers that have been applied to stats or skills.
	/// </summary>
	public DbSet<AppliedModifier> AppliedModifiers { get; set; } = null!;

	/// <summary>
	///     Gets or sets the DbSet for all Effects (including derived types like DamageEffect, etc.).
	///     This single DbSet queries the entire "Effects" table hierarchy.
	/// </summary>
	public DbSet<Effect> Effects { get; set; } = null!;

	/// <summary>
	/// 	Table for fantastical elements
	/// </summary>
	public DbSet<Element> Elements { get; set; } = null!;

	/// <summary>
	///     Table for entities, which represent characters, NPCs, mobs, etc. in the game world.
	/// </summary>
	public DbSet<Entity> Entities { get; set; } = null!;

	/// <summary>
	///     Table for entity inventories, which link entities to the item instances they own.
	/// </summary>
	public DbSet<Inventory> Inventories { get; set; } = null!;

	/// <summary>
	///     Table for entity skills, which link entities to their instanced skills.
	/// </summary>
	public DbSet<EntitySkills> EntitySkills { get; set; } = null!;

	/// <summary>
	///     Table for entity stats, which link entities to their instanced stats.
	/// </summary>
	public DbSet<EntityStats> EntityStats { get; set; } = null!;

	// Core Game Data Definitions
	/// <summary>
	///     Table for item definitions, which are the "blueprints" for items in the game.
	/// </summary>
	public DbSet<ItemDefinition> ItemDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for item instances, which are specific instances of item definitions owned by entities.
	/// </summary>
	public DbSet<Item> ItemInstances { get; set; } = null!;

	/// <summary>
	///     Table for instanced skills, which are specific instances of skill definitions assigned to entities.
	/// </summary>
	public DbSet<Skill> InstancedSkills { get; set; } = null!;

	/// <summary>
	///     Table for instanced stats, which are specific instances of stat definitions assigned to entities.
	/// </summary>
	public DbSet<Stat> InstancedStats { get; set; } = null!;

	// Linking Tables
	/// <summary>
	///     Table for linked stats, which define relationships between different stats (e.g., one stat affecting another).
	/// </summary>
	public DbSet<LinkedStats> LinkedStats { get; set; } = null!;

	/// <summary>
	///     Table for log entries, which store application logs for auditing and debugging purposes.
	/// </summary>
	public DbSet<LogEntry> LogEntries { get; set; } = null!;

	/// <summary>
	///     Table for leveling definitions, which define how leveling is applied.
	/// </summary>
	public DbSet<LevelingDefinition> LevelingDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for material definitions, which define the materials that items can be made from.
	/// </summary>
	public DbSet<MaterialDefinition> MaterialDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for modifier definitions, which define how stats and skills can be modified.
	/// </summary>
	public DbSet<ModifierDefinition> ModifierDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for modification effects, which define the specific effects of modifiers.
	/// </summary>
	public DbSet<ModificationEffect> ModificationEffects { get; set; } = null!;

	/// <summary>
	///     Table for skill definitions, which define the skills that entities can possess.
	/// </summary>
	public DbSet<SkillDefinition> SkillDefinitions { get; set; } = null!;

	/// <summary>
	///     Table for stat definitions, which define the various stats that entities can have.
	/// </summary>
	public DbSet<StatDefinition> StatDefinitions { get; set; } = null!;

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

		// Configure lookup tables
		// Configure advanced table relationships (1 -> M, M -> M, additional FK, composite PK)
		// (automatically pull table definitions from `Configurations` via `IEntityTypeConfiguration`)
		_ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(RpgDbContext).Assembly);
	}

	/// <inheritdoc />
	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		ArgumentNullException.ThrowIfNull(configurationBuilder, nameof(configurationBuilder));

		// This is where the value converters are registered.
		// Tell EF Core to use our custom converter for every property of type Ulid.
		_ = configurationBuilder.Properties<Ulid>()
			.HaveConversion<UlidToBytesConverter>();
		// Tell EF Core to use our custom converter for every property of type ICollection<Ulid>.
		_ = configurationBuilder.Properties<ICollection<Ulid>>()
			.HaveConversion<UlidCollectionToBytesConverter>();

		// Tell EF Core to use our custom converter for every property of type Name.
		_ = configurationBuilder.Properties<Name>()
			.HaveConversion<NameToSimpleStringConverter>();

		base.ConfigureConventions(configurationBuilder);
	}
}
