using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Effects that each stack of a modification can have on a stat.
/// </summary>
public class ModifierEffect : IModifierEffect
{
    /// <summary>
    ///     Creates a new modifier effect with the specified options.
    /// </summary>
    /// <param name="options"></param>
    public ModifierEffect(ModifierEffectOptions options)
    {
        FlatAmount = options.FlatAmount ?? 0;
        Percentage = options.Percentage ?? 0f;
        PercentageOfMax = options.PercentageOfMax ?? 0f;
        IsPositive = options.IsPositive ?? true;
        IsAdditive = options.IsAdditive ?? true;
        ScalesOnBaseValue = options.ScalesOnBaseValue ?? false;
        ScalingFactor = options.ScalingFactor ?? 1f;
        Priority = options.Priority ?? 0;
    }

    /// <inheritdoc />
    public int FlatAmount { get; init; }

    /// <inheritdoc />
    public float Percentage { get; init; }

    /// <inheritdoc />
    public float PercentageOfMax { get; init; }

    /// <inheritdoc />
    public bool IsPositive { get; init; }

    /// <inheritdoc />
    public bool IsAdditive { get; init; }

    /// <inheritdoc />
    public bool ScalesOnBaseValue { get; init; }

    /// <inheritdoc />
    public int Priority { get; init; }


    /// <inheritdoc />
    public float ScalingFactor { get; init; }

    /// <inheritdoc />
    public bool AppliesFlatAmount => FlatAmount > 0;

    /// <inheritdoc />
    public bool AppliesPercentage => Percentage > 0;

    /// <inheritdoc />
    public bool AppliesPercentageOfMax => PercentageOfMax > 0;

    /// <inheritdoc />
    public int ApplyEffect(int baseValue, int currentValue, int maxValue)
    {
        if (IsAdditive) return ApplyAdditiveEffect(baseValue, currentValue, maxValue);

        return ApplyMultiplicativeEffect(baseValue, currentValue, maxValue);
    }

    /// <summary>
    ///     Applies this modifier effect as an additive effect to a stat.
    /// </summary>
    /// <param name="baseValue"></param>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    private int ApplyAdditiveEffect(int baseValue, int currentValue, int maxValue)
    {
        int returnValue = currentValue;
        if (ScalesOnBaseValue)
        {
            float scaledBaseValue = baseValue * ScalingFactor;
            if (IsPositive)
            {
                if (AppliesFlatAmount)
                    returnValue += FlatAmount + (int)Math.Round(scaledBaseValue);
                if (AppliesPercentage)
                    returnValue += (int)(Percentage * scaledBaseValue);
                if (AppliesPercentageOfMax)
                    returnValue += (int)(PercentageOfMax * maxValue * scaledBaseValue);
                return returnValue;
            }

            if (AppliesFlatAmount)
                returnValue -= FlatAmount + (int)Math.Round(scaledBaseValue);
            if (AppliesPercentage)
                returnValue -= (int)(Percentage * scaledBaseValue);
            if (AppliesPercentageOfMax)
                returnValue -= (int)(PercentageOfMax * maxValue * scaledBaseValue);

            return returnValue;
        }

        if (IsPositive)
        {
            if (AppliesFlatAmount)
                returnValue += FlatAmount;
            if (AppliesPercentage)
                returnValue += (int)(Percentage * currentValue);
            if (AppliesPercentageOfMax)
                returnValue += (int)(PercentageOfMax * maxValue);

            return returnValue;
        }

        if (AppliesFlatAmount)
            returnValue -= FlatAmount;
        if (AppliesPercentage)
            returnValue -= (int)(Percentage * currentValue);
        if (AppliesPercentageOfMax)
            returnValue -= (int)(PercentageOfMax * maxValue);

        return returnValue;
    }

    /// <summary>
    ///     Applies this modifier effect as a multiplicative effect to a stat.
    /// </summary>
    /// <param name="baseValue"></param>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    private int ApplyMultiplicativeEffect(int baseValue, int currentValue, int maxValue)
    {
        int returnValue = currentValue;
        if (IsPositive)
        {
            if (ScalesOnBaseValue)
            {
                float scaledBaseValue = baseValue * ScalingFactor;
                if (AppliesFlatAmount)
                    returnValue += (FlatAmount + (int)Math.Round(scaledBaseValue)) * currentValue;
                if (AppliesPercentage)
                    returnValue += (int)(Percentage * scaledBaseValue) * currentValue;
                if (AppliesPercentageOfMax)
                    returnValue += (int)(PercentageOfMax * maxValue * scaledBaseValue) * currentValue;
                return returnValue;
            }

            if (AppliesFlatAmount)
                returnValue += FlatAmount * currentValue;
            if (AppliesPercentage)
                returnValue += (int)(Percentage * currentValue) * currentValue;
            if (AppliesPercentageOfMax)
                returnValue += (int)(PercentageOfMax * maxValue) * currentValue;
            return returnValue;
        }

        if (ScalesOnBaseValue)
        {
            float scaledBaseValue = baseValue * ScalingFactor;
            if (AppliesFlatAmount)
                returnValue -= (FlatAmount + (int)Math.Round(scaledBaseValue)) * currentValue;
            if (AppliesPercentage)
                returnValue -= (int)(Percentage * scaledBaseValue) * currentValue;
            if (AppliesPercentageOfMax)
                returnValue -= (int)(PercentageOfMax * maxValue * scaledBaseValue) * currentValue;
            return returnValue;
        }

        if (AppliesFlatAmount)
            returnValue -= FlatAmount * currentValue;
        if (AppliesPercentage)
            returnValue -= (int)(Percentage * currentValue) * currentValue;
        if (AppliesPercentageOfMax)
            returnValue -= (int)(PercentageOfMax * maxValue) * currentValue;

        return returnValue;
    }

    /// <inheritdoc />
    public string ToDisplay()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public string ToDescription()
    {
        throw new NotImplementedException();
    }
}
