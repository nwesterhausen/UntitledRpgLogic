using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Core.Progression;

/// <summary>
///     Defines a prerequisite condition that must be met.
/// </summary>
/// <remarks>Extended by <see cref="LearningRequirement" /> and <see cref="CastingRequirement" />.</remarks>
public abstract record RequirementBase
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
	///     The category of requirement (e.g., Level, Stat, Class, Race).
	/// </summary>
	public RequirementType RequirementType { get; init; } = RequirementType.None;

	/// <summary>
	///     The identifier of the required entity or definition (e.g., Stat ID, Class ID, or Race ID).
	/// </summary>
	public Ulid RequiredEntityId { get; init; }

	/// <summary>
	///     The numerical threshold or minimum level required to satisfy this condition. Ignored for implicit requirement types.
	/// </summary>
	public float AmountNeeded { get; set; }
}
