using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     A skill definition in the RPG logic, for usage with a database.
/// </summary>
public record SkillDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes an empty instance of the <see cref="SkillDefinition" /> class (for EF use).
	/// </summary>
	public SkillDefinition() => this.Name = Name.Empty;

	/// <summary>
	///     Initializes a new instance of the <see cref="SkillDefinition" /> class with the specified name.
	/// </summary>
	/// <param name="name">The name of the skill.</param>
	public SkillDefinition(Name name) => this.Name = name;

	/// <summary>
	///     The name of the skill. This is used to identify the skill in the game and should be unique.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     The maximum level the skill can reach.
	/// </summary>
	public int MaxLevel { get; init; } = DefaultValues.SkillDefaultMaxLevel;

	/// <summary>
	///     The primary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorA { get; init; } = DefaultValues.SkillDefaultScalingAlpha;

	/// <summary>
	///     The secondary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorB { get; init; } = DefaultValues.SkillDefaultScalingBeta;

	/// <summary>
	///     The tertiary facing factor. One of three scaling factors used to determine the skill's level scaling.
	/// </summary>
	public float ScalingFactorC { get; init; } = DefaultValues.SkillDefaultScalingGamma;

	/// <summary>
	///     How many points required to go from level 0 to level 1. This is used in level scaling calculations.
	/// </summary>
	public int PointsForFirstLevel { get; init; } = DefaultValues.SkillDefaultPointsForFirstLevel;

	/// <summary>
	///     The type of scaling curve to use for experience requirements (e.g., linear, parabolic, logarithmic).
	/// </summary>
	public ScalingCurveType ScalingCurve { get; init; } = DefaultValues.SkillDefaultScalingCurve;

	/// <summary>
	///     Navigation property for all abilities that belong to this skill discipline.
	/// </summary>
	public virtual ICollection<Ability> Abilities { get; } = new List<Ability>();

	/// <summary>
	///     The unique identifier for the skill definition. This is used to identify the skill in the database.
	/// </summary>
	[Key]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
