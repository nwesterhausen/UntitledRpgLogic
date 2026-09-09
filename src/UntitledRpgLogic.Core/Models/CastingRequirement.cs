using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a prerequisite condition that must be met to successfully activate an ability.
/// </summary>
/// <remarks>Owned by <see cref="Ability" />.</remarks>
[Table("ability_casting_requirements")]
public record CastingRequirement
{
	/// <summary>
	///     The unique database row identifier for the requirement entry.
	/// </summary>
	[Key]
	public int Id { get; init; }

	/// <summary>
	///     The type of requirement (e.g., Stat, Class, Race, OngoingSpell).
	/// </summary>
	public RequirementType RequirementType { get; init; } = RequirementType.None;

	/// <summary>
	///     The identifier of the required entity or definition (StatId, ClassId, or ongoing effect Ulid).
	/// </summary>
	public Ulid RequiredEntityId { get; init; }

	/// <summary>
	///     The target threshold or level required to satisfy this condition.
	/// </summary>
	public float AmountNeeded { get; set; }
}
