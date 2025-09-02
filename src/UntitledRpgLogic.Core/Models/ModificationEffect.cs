using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a modification effect that can be applied to stats, such as buffs or debuffs. Consumed by modifiers to
///     finally apply the effect to an entity's stats.
/// </summary>
public class ModificationEffect
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ModificationEffect" /> class.
	///     This parameterless constructor is required by Entity Framework Core for materialization.
	/// </summary>
	public ModificationEffect()
	{
	}

	/// <summary>
	///     Creates a new modifier effect with the specified options.
	/// </summary>
	/// <param name="options"></param>
	public ModificationEffect(ModifierEffectOptions options)
	{
		ArgumentNullException.ThrowIfNull(options, nameof(options));

		this.FlatAmount = options.FlatAmount ?? 0;
		this.Percentage = options.Percentage ?? 0f;
		this.PercentageOfMax = options.PercentageOfMax ?? 0f;
		this.Positive = options.IsPositive ?? true;
	}

	/// <summary>
	///     The unique identifier for the modification effect. This is used to reference the effect in the game.
	/// </summary>
	[Key]
	public Ulid Id { get; set; }

	/// <summary>
	///     The flat amount of modification that is applied.
	/// </summary>
	public int FlatAmount { get; set; }

	/// <summary>
	///     The percentage amount of modification that is applied. This is a value between 0 and 1, where 1 represents 100%
	///     modification.
	/// </summary>
	public float Percentage { get; set; }

	/// <summary>
	///     The percentage of the maximum value that this modification effect represents. This is used to determine how much of
	///     the stat's maximum value is modified.
	///     This is a value between 0 and 1, where 1 represents 100% of the maximum value.
	/// </summary>
	public float PercentageOfMax { get; set; }

	/// <summary>
	///     Whether the modification effect is positive (buff) or negative (debuff).
	/// </summary>
	public bool Positive { get; set; }
}
