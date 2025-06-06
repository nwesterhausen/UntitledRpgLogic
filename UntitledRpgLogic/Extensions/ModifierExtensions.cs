using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Extensions;

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
        var sign = modifier.IsPositive ? "+" : "-";
        var value = modifier.IsPercentage ? $"{modifier.EffectiveAmount * 100:F1}" : $"{modifier.EffectiveAmount:F1}";
        var percentage = modifier.IsPercentage ? "%" : string.Empty;
        var baseValue = modifier.ScalesOnBaseValue ? " (of base)" : string.Empty;

        return $"{sign}{value}{percentage}{baseValue}";
    }
}