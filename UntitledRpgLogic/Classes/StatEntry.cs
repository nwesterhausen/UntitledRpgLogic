using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Classes;

/// <summary>
///     A wrapper for a stat that contains additional information such as damageable behavior, mitigations, and modifiers.
/// </summary>
/// <typeparam name="T">The stat contained within this entry</typeparam>
public class StatEntry<T> where T : StatBase
{
    /// <summary>
    ///     Creates a new instance of the <see cref="StatEntry{T}" /> class with the specified stat.
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="isDamageable"></param>
    public StatEntry(T stat, bool? isDamageable = null)
    {
        Stat = stat;
        if (isDamageable.HasValue && isDamageable.Value) Damageable = new DamageableBehavior<T>(stat);
        stat.BaseValueChanged += () =>
        {
            // When the base value changes, we want the stat to recalculate its current value
            // for each modifier, in order of priority, run Stat.ApplyModifier(modifier)
            foreach (var modifier in Modifiers.OrderBy(m => m.Priority)) stat.ApplyModifier(modifier);
        };
    }

    /// <summary>
    ///     The stat that this entry represents.
    /// </summary>
    public T Stat { get; }

    /// <summary>
    ///     The damageable behavior for the stat, if it is damageable.
    /// </summary>
    private IDamageable? Damageable { get; }

    /// <summary>
    ///     Whether the stat is damageable or not.
    /// </summary>
    public bool IsDamageable => Damageable != null;

    /// <summary>
    ///     The list of mitigations that apply to this stat entry, such as damage reduction or resistance effects.
    /// </summary>
    private List<IAppliesDamageMitigation> Mitigations { get; } = [];

    /// <summary>
    ///     The list of modifiers that apply to this stat entry, such as buffs, debuffs, or other effects that modify the
    ///     stat's value.
    /// </summary>
    private List<IModifier> Modifiers { get; } = []; // For buffs, debuffs, etc.

    /// <summary>
    ///     Apply damage to the stat entry using the provided damage options and damage calculator.
    /// </summary>
    /// <param name="damageOptions"></param>
    /// <param name="damageCalculator"></param>
    public void ApplyDamage(DamageOptions damageOptions, IDamageCalculator damageCalculator)
    {
        if (!IsDamageable) return;

        var damageAmount = damageCalculator.GetPointDamageFromOptions(damageOptions, Stat);
        if (damageAmount <= 0) return;

        var finalDamage = damageCalculator.CalculateFinalDamage(damageAmount, Mitigations);
        Damageable?.TakeDamage(finalDamage);
        DamageTakenEvent?.Invoke(this, new StatDamageEventArgs
        {
            IncomingDamage = damageAmount,
            IncomingDamagePercentage = PointsAsPercentageOfMax(damageAmount),
            FinalDamage = finalDamage,
            FinalDamagePercentage = PointsAsPercentageOfMax(finalDamage),
            SourceId = damageOptions.SourceId,
            StatName = Stat.Name
        });
    }

    /// <summary>
    ///     Returns what percentage of the stat's maximum value a given point value represents.
    /// </summary>
    /// <param name="pointValue"></param>
    /// <returns></returns>
    public float PointsAsPercentageOfMax(int pointValue)
    {
        if (Stat.MaxValue <= 0) return 0f;

        return pointValue / (float)Stat.MaxValue * 100f;
    }

    /// <summary>
    ///     Add a mitigation to this stat entry.
    /// </summary>
    /// <param name="mitigation"></param>
    public void AddMitigation(IAppliesDamageMitigation mitigation)
    {
        Mitigations.Add(mitigation);
    }

    /// <summary>
    ///     Remove a mitigation from this stat entry.
    /// </summary>
    /// <param name="mitigation"></param>
    public void RemoveMitigation(IAppliesDamageMitigation mitigation)
    {
        Mitigations.Remove(mitigation);
    }

    /// <summary>
    ///     Add a modifier to this stat entry, such as buffs, debuffs, or other effects that modify the stat's value.
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(IModifier modifier)
    {
        Modifiers.Add(modifier);
    }

    /// <summary>
    ///     Remove a modifier from this stat entry.
    /// </summary>
    /// <param name="modifier"></param>
    public void RemoveModifier(IModifier modifier)
    {
        Modifiers.Remove(modifier);
    }

    /// <summary>
    ///     Event that is raised when damage is applied to the stat entry.
    /// </summary>
    public event EventHandler<StatDamageEventArgs>? DamageTakenEvent;
}