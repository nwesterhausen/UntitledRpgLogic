using UntitledRpgLogic.Core.Abilities.Effects;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Defines an owned modification delta applied to a character stat (e.g., HP, Mana, Strength).
/// </summary>
/// <remarks>Owned by <see cref="Effect" /> and serialized as JSON.</remarks>
public record AffectedStat
{
	/// <summary>
	///     Identifier of the target <see cref="StatDefinition" /> being modified.
	/// </summary>
	public Ulid StatId { get; init; }

	/// <summary>
	///     The magnitude of the change (positive for buffs/restoration, negative for damage/drain).
	/// </summary>
	public float AmountChange { get; init; }

	/// <summary>
	///     Indicates whether <see cref="AmountChange" /> is a percentage multiplier (true) or a flat offset (false).
	/// </summary>
	public bool IsPercentage { get; init; }
}
