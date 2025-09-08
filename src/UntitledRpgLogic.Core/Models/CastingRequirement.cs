using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// Defines a requirement that must be met to successfully activate an ability.
/// </summary>
/// <remarks>(Owned by <see cref="Ability"/>).</remarks>
public class CastingRequirement
{
	/// <summary>
	/// The unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	/// The (FK) id of the <see cref="Ability"/> that requires this casting requirement.
	/// </summary>
	public Ulid AbilityId { get; set; }

	/// <summary>
	/// A navigation property to the <see cref="Ability"/> that requires this casting requirement.
	/// </summary>
	public virtual Ability Ability { get; set; } = null!;

	/// <summary>
	/// The type of requirement (e.g., "Stat", "Class", "Race").
	/// </summary>
	public RequirementType RequirementType { get; set; }

	/// <summary>
	/// Gets or sets the <see cref="Ulid"/> of the specific requirement (e.g., Stat <see cref="Ulid"/>, Class <see cref="Ulid"/>, or the <see cref="Ulid"/> of an "OngoingSpell").
	/// </summary>
	public Ulid RequiredEntityId { get; set; }

	/// <summary>
	/// Gets or sets the value needed.
	/// </summary>
	public float AmountNeeded { get; set; }
}
