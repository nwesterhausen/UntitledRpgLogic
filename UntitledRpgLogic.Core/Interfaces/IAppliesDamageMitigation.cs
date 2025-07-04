namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for classes that apply damage mitigation to stats.
/// </summary>
public interface IAppliesDamageMitigation
{
    /// <summary>
    ///     Whether the mitigation is positive or negative. Positive will reduce damage, negative will increase it.
    /// </summary>
    public bool IsPositive { get; }

    /// <summary>
    ///     Used to determine the order in which mitigations are applied. Lower values are applied first.
    /// </summary>
    public int MitigationPriority { get; }

    /// <summary>
    ///     Whether the mitigation is a flat amount or a percentage.
    /// </summary>
    public bool IsPercentage { get; }

    /// <summary>
    ///     How much mitigation is applied. If <see cref="IsPercentage" /> is true, this is a percentage of the damage amount.
    /// </summary>
    public float Amount { get; }

    /// <summary>
    ///     Print a human-readable description of the mitigation, e.g. "-10%" or "+5" or "+20%"
    /// </summary>
    public string Display { get; }

    /// <summary>
    ///     Performs the mitigation calculation on the given damage amount.
    /// </summary>
    /// <param name="damageAmount">incoming damage</param>
    /// <returns>incoming damage after the mitigation is applied</returns>
    public int ApplyMitigation(int damageAmount);

    /// <summary>
    ///     Event that is raised when the mitigation is applied to a damage amount. The event should notify of the total
    ///     amount mitigated.
    /// </summary>
    event EventHandler<int> MitigationApplied;
}
