using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     Defines an environmental, attribute, or situational factor that influences the chance of ability activation failure.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_failure_influences")]
public record FailureInfluence : RequirementBase
{
	/// <summary>
	///     Navigation property back to the owning ability.
	/// </summary>
	[ForeignKey(nameof(AbilityId))]
	public AbilityDefinition? Ability { get; init; }

	/// <summary>
	///     The numerical threshold needed to guarantee success (zero failure chance from this influence).
	/// </summary>
	public float AmountAlwaysSucceed { get; set; }

	/// <summary>
	///     The scaling multiplier applied to calculate failure probability when the threshold is unmet.
	/// </summary>
	public float InfluenceScale { get; set; }
}
