using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database model for a definition of a stat. This is used to define properties of a stat like name, value range
///     and (if its dependent) which stat it depends on. This is not used when a stat belongs to an entity, but rather
///     defines
///     how those stats would behave.
/// </summary>
public record StatDefinition
{
	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class with default values.
	///     This constructor sets the stat ID to a new ULID, the name to an empty value, the variation to "Major",
	///     and initializes the minimum and maximum values to their default values.
	/// </summary>
	public StatDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Variation = StatVariation.Major;
		this.HasChangeableValue = true;
		this.MinValue = DefaultValues.StatDefaultMinValue;
		this.MaxValue = DefaultValues.StatDefaultMaxValue;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class with the specified name.
	///     This constructor sets the stat ID to a new ULID, assigns the provided name, and initializes other
	///     properties to their default values.
	/// </summary>
	/// <param name="name">The name of the stat.</param>
	public StatDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     Initializes a new instance of the <see cref="StatDefinition" /> class using the specified configuration.
	///     This constructor sets the stat ID and name based on the provided configuration and initializes other
	///     properties based on the configuration or their default values if not specified.
	/// </summary>
	/// <param name="config">The configuration object containing stat data.</param>
	/// <exception cref="ArgumentNullException">Thrown if the provided configuration is null.</exception>
	public StatDefinition(StatDataConfig config) : this()
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Id = config.Identifier;
		this.Name = new Name(config.Name);
		this.Variation = config.Variation ?? StatVariation.Major;
		this.MinValue = config.MinValue ?? DefaultValues.StatDefaultMinValue;
		this.MaxValue = config.MaxValue ?? DefaultValues.StatDefaultMaxValue;
	}

	/// <summary>
	///     The ULID for the stat. Any instances of this stat refer to this definition via this ID.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the stat. This is used to identify the stat in the game and is used in the UI.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Whether the stat is able to be directly changed or not.
	/// </summary>
	public bool HasChangeableValue { get; init; }

	/// <summary>
	///     The minimum value for this stat. This is the lowest value the stat can have.
	/// </summary>
	public int MinValue { get; init; }

	/// <summary>
	///     The maximum value for this stat. This is the highest value the stat can have.
	/// </summary>
	public int MaxValue { get; init; }

	/// <summary>
	///     The type of stat this is. This is used to determine how the stat behaves in the game.
	/// </summary>
	public required StatVariation Variation { get; init; }

	/// <summary>
	///     Stats that this stat depends on (if any).
	/// </summary>
	public ICollection<LinkedStats> LinkedStats { get; init; } = [];
}
