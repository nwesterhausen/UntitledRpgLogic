using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions to provide display string functionality for modifiers in the RPG logic.
/// </summary>
public static class ModifierExtensions
{
	/// <summary>
	///     Represents the modifier as a display string for tooltips or UI elements.
	/// </summary>
	/// <param name="modifier">modifier to create short display string for</param>
	/// <returns>string representing the modifier (e.g., "+10%", "-5", "+20% (of base)") </returns>
	public static string ToDisplay(this ModifierDefinition modifier)
	{
		ArgumentNullException.ThrowIfNull(modifier);
		var baseEffectString = string.Empty;
		if (modifier.ModificationEffects != null)
		{
			baseEffectString = modifier.ModificationEffects.ToDisplay();
		}

		if (modifier.StackEffects != null)
		{
			baseEffectString += " Per Stack:";
			foreach (var effect in modifier.StackEffects)
			{
				baseEffectString += $" {effect.ToDisplay()}";
			}
		}

		return baseEffectString;
	}

	/// <summary>
	///     Represents the collection of modifiers as a single string for tooltips or UI elements.
	/// </summary>
	/// <param name="modifiers">Collection of modification effects</param>
	/// <returns>String for use in the UI</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static string ToDisplay(this IEnumerable<ModificationEffect> modifiers)
	{
		ArgumentNullException.ThrowIfNull(modifiers);

		var combinedEffectString = string.Empty;
		foreach (var effect in modifiers)
		{
			combinedEffectString += effect.ToDisplay();
		}

		return combinedEffectString;
	}

	/// <summary>
	///     Represents the modifier effect as a display string for tooltips or UI elements.
	/// </summary>
	/// <param name="effect"></param>
	/// <returns></returns>
	public static string ToDisplay(this ModificationEffect effect)
	{
		ArgumentNullException.ThrowIfNull(effect);
		var sign = effect.IsPositive ? "+" : "-";
		var flatAmount = effect.FlatAmount != 0f ? $"{sign}{effect.FlatAmount}" : string.Empty;
		var percentage = effect.Percentage != 0f ? $"{sign}{effect.Percentage:F2}%" : string.Empty;
		var percentageOfMax = effect.PercentageOfMax != 0f
			? $"{sign}{effect.PercentageOfMax:F2}% of MAX"
			: string
				.Empty;
		var scaling = effect.ScalesOnBaseValue
			? $"(scaled at {effect.ScalingFactor:F2}%)"
			: string
				.Empty;

		string[] strArr =
		[
			flatAmount,
			percentage,
			percentageOfMax,
			scaling
		];
		return string.Join(" ", strArr.Where(s => !string.IsNullOrEmpty(s)).ToArray());
	}
}
