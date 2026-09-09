using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines an environmental, attribute, or situational factor that influences the chance of ability activation failure.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_failure_influences")]
public record FailureInfluence
{
	/// <summary>
	///     The unique database row identifier for this influence entry.
	/// </summary>
	[Key]
	public int Id { get; init; }

	/// <summary>
	///     The category of prerequisite or environmental check (e.g., Stat, Class, Race, Ambient).
	/// </summary>
	public RequirementType RequirementType { get; init; } = RequirementType.None;

	/// <summary>
	///     The identifier of the required entity or definition (e.g., the "Intellect" Stat ID).
	/// </summary>
	public Ulid RequiredEntityId { get; init; }

	/// <summary>
	///     The numerical threshold needed to guarantee success (zero failure chance from this influence).
	/// </summary>
	public float AmountAlwaysSucceed { get; set; }

	/// <summary>
	///     The scaling multiplier applied to calculate failure probability when the threshold is unmet.
	/// </summary>
	public float InfluenceScale { get; set; }
}
