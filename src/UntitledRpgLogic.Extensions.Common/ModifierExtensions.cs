using UntitledRpgLogic.Core.Interfaces.Effects;

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
	public static string ToDisplay(this IModifier modifier)
	{
		ArgumentNullException.ThrowIfNull(modifier, nameof(modifier));
		var baseEffectString = string.Empty;
		if (modifier.ModificationEffect != null)
		{
			baseEffectString = modifier.ModificationEffect.ToDisplay();
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
	///     Represents the modifier effect as a display string for tooltips or UI elements.
	/// </summary>
	/// <param name="effect"></param>
	/// <returns></returns>
	public static string ToDisplay(this IModifierEffect effect)
	{
		ArgumentNullException.ThrowIfNull(effect, nameof(effect));
		var sign = effect.IsPositive ? "+" : "-";
		var flatAmount = effect.AppliesFlatAmount ? $"{sign}{effect.FlatAmount}" : string.Empty;
		var percentage = effect.AppliesPercentage ? $"{sign}{effect.Percentage:F2}%" : string.Empty;
		var percentageOfMax = effect.AppliesPercentageOfMax
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
