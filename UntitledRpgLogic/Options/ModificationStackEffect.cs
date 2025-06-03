namespace UntitledRpgLogic.Options;

/// <summary>
///     Effects that each stack of a modification can have on a stat.
/// </summary>
public class ModificationStackEffect
{
    /// <summary>
    ///     Increases or decreases the stat by a flat amount.
    /// </summary>
    public int FlatAmount { get; init; }

    /// <summary>
    ///     Increases or decreases the stat by a percentage of its current value.
    /// </summary>
    public float Percentage { get; init; }

    /// <summary>
    ///     Increases or decreases the stat by a percentage of its maximum value.
    /// </summary>
    public float PercentageOfMax { get; init; }

    /// <summary>
    ///     Whether the modification is positive or negative.
    /// </summary>
    public bool Positive { get; init; }

    /// <summary>
    ///     Whether the modification applies a flat amount per stack.
    /// </summary>
    public bool AppliesFlatAmount => FlatAmount > 0;

    /// <summary>
    ///     Whether the modification applies a percentage of the current value per stack.
    /// </summary>
    public bool AppliesPercentage => Percentage > 0;

    /// <summary>
    ///     Whether the modification applies a percentage of the maximum value per stack.
    /// </summary>
    public bool AppliesPercentageOfMax => PercentageOfMax > 0;
}