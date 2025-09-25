using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a requirement to learn an ability.
/// </summary>
/// <remarks>(Owned by <see cref="Ability" />).</remarks>
public class LearningRequirement
{
	/// <summary>
	///     The unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	///     The (FK) id of the <see cref="Ability" /> that requires this casting requirement.
	/// </summary>
	public Ulid AbilityId { get; set; }

	/// <summary>
	///     A navigation property to the <see cref="Ability" /> that requires this casting requirement.
	/// </summary>
	public virtual Ability Ability { get; set; } = null!;

	/// <summary>
	///     The type of requirement (e.g., "Level", "Stat", "Class", "Race").
	/// </summary>
	public RequirementType RequirementType { get; set; }

	/// <summary>
	///     Gets or sets the <see cref="Ulid" /> of the specific requirement (e.g., the <see cref="Ulid" /> for the Stat "Intellect",
	///     the <see cref="Ulid" /> for the "Mage" Class, or the <see cref="Ulid" /> for the "Elf" Race).
	/// </summary>
	public Ulid RequiredEntityId { get; set; }

	/// <summary>
	///     Gets or sets the value needed (e.g., Stat amount 50, Level 10).
	/// </summary>
	public float AmountNeeded { get; set; }
}
