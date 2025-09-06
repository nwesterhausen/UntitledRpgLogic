using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     A skill definition in the RPG logic, for usage with a database.
/// </summary>
public record SkillDefinition
{
	/// <summary>
	///     Initializes an empty instance of the <see cref="SkillDefinition" /> class (for EF use).
	/// </summary>
	public SkillDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.MaxLevel = DefaultValues.SkillDefaultMaxLevel;
		this.ScalingFactorA = DefaultValues.SkillDefaultScalingAlpha;
		this.ScalingFactorB = DefaultValues.SkillDefaultScalingBeta;
		this.ScalingFactorC = DefaultValues.SkillDefaultScalingGamma;
		this.PointsForFirstLevel = DefaultValues.SkillDefaultPointsForFirstLevel;
		this.ScalingCurve = DefaultValues.SkillDefaultScalingCurve;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SkillDefinition" /> class with the specified name.
	///     This constructor sets the skill ID to a new <see cref="Ulid" />, assigns the provided name, and initializes
	///     other properties to their default values.
	/// </summary>
	/// <param name="name">The name of the skill.</param>
	public SkillDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     Initializes a new instance of the <see cref="SkillDefinition" /> class using the specified configuration.
	///     This constructor sets the skill ID and name based on the provided configuration and initializes
	///     other properties based on the configuration or their default values if not specified.
	/// </summary>
	/// <param name="config">The configuration object containing skill data.</param>
	/// <exception cref="ArgumentNullException">Thrown if the provided configuration is null.</exception>
	public SkillDefinition(SkillDataConfig config) : this()
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Id = config.Id;
		this.Name = new Name(config.Name);

		if (config.LevelingOptions is null)
		{
			return;
		}

		this.MaxLevel = config.LevelingOptions.MaxLevel ?? DefaultValues.SkillDefaultMaxLevel;
		this.ScalingFactorA = config.LevelingOptions.ScalingFactorA ?? DefaultValues.SkillDefaultScalingAlpha;
		this.ScalingFactorB = config.LevelingOptions.ScalingFactorB ?? DefaultValues.SkillDefaultScalingBeta;
		this.ScalingFactorC = config.LevelingOptions.ScalingFactorC ?? DefaultValues.SkillDefaultScalingGamma;
		this.PointsForFirstLevel = config.LevelingOptions.PointsForFirstLevel ?? DefaultValues.SkillDefaultPointsForFirstLevel;
		this.ScalingCurve = config.LevelingOptions.ScalingCurve ?? DefaultValues.SkillDefaultScalingCurve;
	}

	/// <summary>
	///     The unique identifier for the skill definition. This is used to identify the skill in the database.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the skill. This is used to identify the skill in the game and should be unique.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     The maximum level the skill can reach.
	/// </summary>
	public int MaxLevel { get; init; }

	/// <summary>
	///     The primary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorA { get; init; }

	/// <summary>
	///     The secondary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorB { get; init; }

	/// <summary>
	///     The tertiary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorC { get; init; }

	/// <summary>
	///     How many points required to go from level 0 to level 1. This is used in level scaling calculations.
	/// </summary>
	public int PointsForFirstLevel { get; init; }

	/// <summary>
	///     The type of scaling curve to use for experience requirements (e.g., linear, parabolic, logarithmic).
	/// </summary>
	public ScalingCurveType ScalingCurve { get; init; }
}
