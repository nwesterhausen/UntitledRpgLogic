using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a prerequisite condition that must be met to permanently learn an ability.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_learning_requirements")]
public record LearningRequirement
{
	/// <summary>
	///     The unique database row identifier for this requirement entry.
	/// </summary>
	[Key]
	public int Id { get; init; }

	/// <summary>
	///     Foreign key of the owning <see cref="Ability" />.
	/// </summary>
	public Ulid AbilityId { get; init; }

	/// <summary>
	///     Navigation property back to the owning ability.
	/// </summary>
	[ForeignKey(nameof(AbilityId))]
	public Ability? Ability { get; init; }

	/// <summary>
	///     The category of requirement (e.g., Level, Stat, Class, Race).
	/// </summary>
	public RequirementType RequirementType { get; init; } = RequirementType.None;

	/// <summary>
	///     The identifier of the required entity or definition (e.g., Stat ID, Class ID, or Race ID).
	/// </summary>
	public Ulid RequiredEntityId { get; init; }

	/// <summary>
	///     The numerical threshold or minimum level required to satisfy this condition.
	/// </summary>
	public float AmountNeeded { get; set; }
}
