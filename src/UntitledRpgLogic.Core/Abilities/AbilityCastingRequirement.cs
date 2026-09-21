using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     Defines a prerequisite condition that must be met to successfully activate an ability.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_casting_requirements")]
public record AbilityCastingRequirement : RequirementBase
{
	/// <summary>
	///     Foreign key of the owning <see cref="AbilityDefinition" />.
	/// </summary>
	public Ulid AbilityId { get; init; }

	/// <summary>
	///     Navigation property back to the owning ability.
	/// </summary>
	[ForeignKey(nameof(AbilityId))]
	public AbilityDefinition? Ability { get; init; }
}
