using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines the contract for a service that manages and operates on StatEntry objects.
/// </summary>
public interface IStatService : IChangeableValueService<IStat>
{
    /// <summary>
    ///     Raised when a stat takes damage after all calculations.
    /// </summary>
    event EventHandler<StatDamageEventArgs> StatDamageTaken;

    /// <summary>
    ///     Applies damage to a stat entry, calculating final damage based on mitigations.
    /// </summary>
    void ApplyDamage(StatEntry<IStat> statEntry, DamageOptions damageOptions);

    /// <summary>
    ///     Adds a modifier to a stat entry and recalculates the stat's value.
    /// </summary>
    void AddModifier(StatEntry<IStat> statEntry, IModifier modifier);

    /// <summary>
    ///     Removes a modifier and recalculates the stat's value.
    /// </summary>
    void RemoveModifier(StatEntry<IStat> statEntry, IModifier modifier);

    /// <summary>
    ///     Adds a damage mitigation to a stat entry.
    /// </summary>
    void AddMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation);

    /// <summary>
    ///     Removes a damage mitigation from a stat entry.
    /// </summary>
    void RemoveMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation);

    /// <summary>
    ///     Raised when a stat is healed.
    /// </summary>
    event EventHandler<StatHealEventArgs> StatHealed;

    /// <summary>
    ///     Applies healing to a stat entry, reducing its current damage.
    /// </summary>
    void Heal(StatEntry<IStat> statEntry, HealOptions healOptions);

    /// <summary>
    ///     Link a dependent stat to a source stat
    /// </summary>
    /// <param name="sourceStat"></param>
    /// <param name="dependentStat"></param>
    /// <param name="ratio"></param>
    void LinkStats(IStat sourceStat, IStat dependentStat, float ratio);

    /// <summary>
    ///     Trigger a stat recalculation based on the provided modifiers.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="modifiers"></param>
    void RecalculateStatValue(IStat stat, IEnumerable<IModifier> modifiers);
}
