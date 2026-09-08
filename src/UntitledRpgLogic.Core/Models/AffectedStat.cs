using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a modification to a character stat (e.g., HP, Strength).
/// </summary>
/// <remarks>(Owned by <see cref="Effect" />).</remarks>
public class AffectedStat
{
	/// <summary>
	///     Gets or sets the <see cref="Ulid" /> of the Stat definition (e.g., Health, Mana).
	/// </summary>
	public Ulid StatId { get; set; }

	/// <summary>
	///     Gets or sets the amount of the change (can be positive or negative).
	/// </summary>
	public float AmountChange { get; set; }

	/// <summary>
	///     Gets or sets a value indicating whether AmountChange is a percentage modifier (true)
	///     or a flat value (false).
	/// </summary>
	public bool IsPercentage { get; set; }
}
