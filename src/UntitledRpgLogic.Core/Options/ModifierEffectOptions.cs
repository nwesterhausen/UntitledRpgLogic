namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Options for creating a modifier effect.
/// </summary>
public class ModifierEffectOptions
{
	/// <summary>
	///     A flat amount that affects the stat.
	/// </summary>
	public int? FlatAmount { get; set; }

	/// <summary>
	///     A percentage of the current value that affects the stat.
	/// </summary>
	public float? Percentage { get; set; }

	/// <summary>
	///     A percentage of the maximum value that affects the stat.
	/// </summary>
	public float? PercentageOfMax { get; set; }

	/// <summary>
	///     Whether the modifier is positive (buff) or negative (debuff).
	/// </summary>
	public bool? IsPositive { get; set; }

	/// <summary>
	///     Whether the modifier is additive (true) or multiplicative (false).
	/// </summary>
	public bool? IsAdditive { get; set; }

	/// <summary>
	///     Whether the modifier scales with the base value (true) or not at all (false).
	/// </summary>
	public bool? ScalesOnBaseValue { get; set; }

	/// <summary>
	///     The scaling factor when scaling on the base value.
	/// </summary>
	public float? ScalingFactor { get; set; }

	/// <summary>
	///     The priority of the modification effect. Useful if multiple types of effects are applied at the same time.
	/// </summary>
	public int? Priority { get; set; }
}
