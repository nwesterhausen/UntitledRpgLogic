namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines the contract for an object that can take damage.
///     The logic for applying damage is handled by a service.
/// </summary>
public interface IDamageable
{
    /// <summary>
    ///     The stat that is being damaged.
    /// </summary>
    IStat Stat { get; }

    /// <summary>
    ///     The current damage applied to the stat, in points.
    /// </summary>
    int CurrentDamage { get; set; }

    /// <summary>
    ///     The current damage as a percentage of the stat's maximum value.
    /// </summary>
    float CurrentPercentageDamage { get; set; }
}
