namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for modifier effects that can be applied to stats.
/// </summary>
public interface IModifierEffect
{
    /// <summary>
    ///     Whether the modification is positive (buff) or negative (debuff).
    /// </summary>
    bool IsPositive { get; }

    /// <summary>
    ///     Whether the modification is additive (true) or multiplicative (false).
    /// </summary>
    bool IsAdditive { get; }

    /// <summary>
    ///     Whether the modification scales with the base value (true) or the current value (false).
    /// </summary>
    bool ScalesOnBaseValue { get; }

    /// <summary>
    ///     The amount by which the modification scales the stat value.
    /// </summary>
    float ScalingFactor { get; }

    /// <summary>
    ///     Whether the modification applies a flat amount per stack.
    /// </summary>
    bool AppliesFlatAmount { get; }

    /// <summary>
    ///     Whether the modification applies a percentage of the current value per stack.
    /// </summary>
    bool AppliesPercentage { get; }

    /// <summary>
    ///     Whether the modification applies a percentage of the maximum value per stack.
    /// </summary>
    bool AppliesPercentageOfMax { get; }

    /// <summary>
    ///     Increases or decreases the stat by a flat amount.
    /// </summary>
    int FlatAmount { get; }

    /// <summary>
    ///     Increases or decreases the stat by a percentage of its current value.
    /// </summary>
    float Percentage { get; }

    /// <summary>
    ///     Increases or decreases the stat by a percentage of its maximum value.
    /// </summary>
    float PercentageOfMax { get; }


    /// <summary>
    ///     The priority of the modificaiton effect. Useful if multiple types of effects are applied at the same time.
    /// </summary>
    int Priority { get; }


    /// <summary>
    ///     Applys the effect to a stat's base value, current value, and maximum value.
    /// </summary>
    /// <param name="baseValue"></param>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    /// <returns>the new value of the stat</returns>
    int ApplyEffect(int baseValue, int currentValue, int maxValue);
}
