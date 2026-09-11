using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Progression;

/// <summary>
/// 	Defines the leveling formula for a given skill.
/// </summary>
/// <remarks>
/// 	Stored in its own table, as nearly all skills would use the same leveling defintion.
/// </remarks>
[Table("leveling_definitions")]
public record LevelingDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     The unique catalog identifier for the definition. Can be assigned explicitly when loading from config archives.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	/// 	The starting level of the skill
	/// </summary>
	public int StartingLevel { get; init; }

	/// <summary>
	/// 	The maximum level the skill can reach.
	/// </summary>
	public int MaxLevel { get; init; } = int.MaxValue;

	/// <summary>
	///     The primary scaling factor (A) for level progression.
	///     Controls the base rate at which experience requirements increase per level.
	/// </summary>
	public float ScalingFactorA { get; init; }

	/// <summary>
	///     The secondary scaling factor (B) for level progression.
	///     Used as an additional multiplier or offset in the experience formula to fine-tune curve steepness.
	/// </summary>
	public float ScalingFactorB { get; init; }

	/// <summary>
	///     The tertiary scaling factor (C) for level progression.
	///     Used as an exponent or offset in polynomial or logarithmic scaling to adjust curve shape.
	/// </summary>
	public float ScalingFactorC { get; init; }

	/// <summary>
	///     The type of scaling curve used to determine experience requirements for each level.
	/// </summary>
	public ScalingCurveType ScalingCurve { get; init; }

	/// <summary>
	///     The total number of experience points required to advance from <see cref="StartingLevel" /> to <see cref="StartingLevel" /> + 1.
	/// </summary>
	public int PointsForFirstLevel { get; init; }
}
