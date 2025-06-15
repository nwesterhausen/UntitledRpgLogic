using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for the damage calculator service.
/// </summary>
public interface IDamageCalculator
{
    /// <summary>
    ///     Actual calculation of the final damage after applying mitigations.
    /// </summary>
    /// <param name="damageAmount">point amount of damage to apply</param>
    /// <param name="mitigations">any damage mitigations to be applied</param>
    public int CalculateFinalDamage(int damageAmount, IEnumerable<IAppliesDamageMitigation> mitigations);

    /// <summary>
    ///     Get the point damage attempted to be applied to a stat based on the provided damage options.
    /// </summary>
    /// <param name="damageOptions">the damage options</param>
    /// <param name="stat">the stat damage will be applied to</param>
    /// <returns>the damage amount in points</returns>
    public int GetPointDamageFromOptions(DamageOptions damageOptions, IStat stat);
}
