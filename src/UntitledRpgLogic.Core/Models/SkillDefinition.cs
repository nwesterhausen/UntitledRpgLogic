using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     A skill definition in the RPG logic, for usage with a database.
/// </summary>
public record SkillDefinition
{
	/// <summary>
	///     The unique identifier for the skill definition. This is used to identify the skill in the database.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	///     The name of the skill. This is used to identify the skill in the game and should be unique.
	/// </summary>
	public required string Name { get; init; }

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
