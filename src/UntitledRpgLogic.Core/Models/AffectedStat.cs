using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines an owned modification delta applied to a character stat (e.g., HP, Mana, Strength).
/// </summary>
/// <remarks>Owned by <see cref="Effect" />.</remarks>
public record AffectedStat
{
	/// <summary>
	///     Foreign key of the target <see cref="StatDefinition" /> being modified.
	/// </summary>
	public Ulid StatId { get; init; }

	/// <summary>
	///     Navigation property to the affected stat definition.
	/// </summary>
	[ForeignKey(nameof(StatId))]
	public StatDefinition? Stat { get; init; }

	/// <summary>
	///     The magnitude of the change (positive for buffs/restoration, negative for damage/drain).
	/// </summary>
	public float AmountChange { get; init; }

	/// <summary>
	///     Indicates whether <see cref="AmountChange" /> is a percentage multiplier (true) or a flat offset (false).
	/// </summary>
	public bool IsPercentage { get; init; }
}
