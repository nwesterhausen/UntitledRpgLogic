using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
/// Defines a factor that influences the chance of activation failure.
/// </summary>
/// <remarks>(Owned by <see cref="Ability"/>).</remarks>
public class FailureInfluence
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
	/// Gets or sets the <see cref="Ulid"/> of the specific influence (e.g., the "Intellect" Stat <see cref="Ulid"/>).
	/// </summary>
	public Ulid RequiredEntityId { get; set; }

	/// <summary>
	/// Gets or sets the amount of the requirement needed to guarantee success (zero failure chance).
	/// </summary>
	public float AmountAlwaysSucceed { get; set; }

	/// <summary>
	/// Gets or sets the scaling modifier applied if the requirement is not met, influencing the failure chance.
	/// </summary>
	public float InfluenceScale { get; set; }
}
