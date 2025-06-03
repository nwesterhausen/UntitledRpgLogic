namespace UntitledRpgLogic.Stat;

/// <summary>
///     Provides data for events related to stat modifications, such as increases or decreases in value.
/// </summary>
public class StatModificationEventArgs
{
    /// <summary>
    ///     Gets the flat amount by which the stat is modified.
    /// </summary>
    public int Amount { get; init; }

    /// <summary>
    ///     Gets the percentage by which the stat is modified.
    /// </summary>
    public float Percentage { get; init; }

    /// <summary>
    ///     Gets the percentage of the stat's maximum value by which it is modified.
    /// </summary>
    public float PercentageOfMax { get; init; }

    /// <summary>
    ///     Gets the name of the stat being modified.
    /// </summary>
    public string StatName { get; init; } = string.Empty;
}