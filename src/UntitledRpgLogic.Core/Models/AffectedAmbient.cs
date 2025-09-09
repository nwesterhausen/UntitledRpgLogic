using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a modification to a world Ambient Index (e.g., Temperature, Gravity).
/// </summary>
/// <remarks>(Owned by <see cref="Effect" />).</remarks>
public class AffectedAmbient
{
	/// <summary>
	///     The unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	///     The (FK) id of the <see cref="Effect" /> that requires this casting requirement.
	/// </summary>
	public Ulid EffectId { get; set; }

	/// <summary>
	///     A navigation property to the <see cref="Effect" /> that requires this casting requirement.
	/// </summary>
	public virtual Effect Effect { get; set; } = null!;

	/// <summary>
	///     Gets or sets the <see cref="Ulid" /> of the Ambient definition (e.g., LocalTemperature).
	/// </summary>
	public Ulid AmbientId { get; set; }

	/// <summary>
	///     Gets or sets the amount of the change.
	/// </summary>
	public float AmountChange { get; set; }

	/// <summary>
	///     Gets or sets a value indicating whether AmountChange is a percentage modifier (true)
	///     or a flat value (false).
	/// </summary>
	public bool IsPercentage { get; set; }
}
