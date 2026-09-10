using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a prerequisite condition that must be met to permanently learn an ability.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_learning_requirements")]
public record LearningRequirement : RequirementBase
{
	/// <summary>
	///     Navigation property back to the owning ability.
	/// </summary>
	[ForeignKey(nameof(AbilityId))]
	public Ability? Ability { get; init; }
}
