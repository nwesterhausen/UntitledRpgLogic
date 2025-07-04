namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Provides data for the StatHealed event.
/// </summary>
public class StatHealEventArgs : EventArgs
{
    /// <summary>
    ///     The actual number of points that were healed after calculations (e.g., clamping).
    /// </summary>
    public int HealAmount { get; init; }

    /// <summary>
    ///     The amount healed, expressed as a percentage of the stat's maximum value.
    /// </summary>
    public float HealPercentage { get; init; }

    /// <summary>
    ///     The unique identifier of the entity or effect that caused the heal.
    /// </summary>
    public Guid SourceId { get; init; }

    /// <summary>
    ///     The name of the stat that was healed.
    /// </summary>
    public string StatName { get; init; } = string.Empty;
}
