using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Services;

public class StatService : IStatService
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly ILogger<StatService> _logger;

    public StatService(IDamageCalculator damageCalculator, ILogger<StatService> logger)
    {
        _damageCalculator = damageCalculator;
        _logger = logger;
    }

    public event EventHandler<StatDamageEventArgs>? StatDamageTaken;

    public void Heal(StatEntry<IStat> statEntry, HealOptions healOptions)
    {
        // Ensure the stat can be healed and is currently damaged
        if (!statEntry.IsHealable || statEntry is not IDamageable { CurrentDamage: > 0 } damageable) return;

        int healAmount = healOptions.BaseAmount; // Can be expanded with more complex logic later
        if (healAmount <= 0) return;

        // --- Logic moved from HealableBehavior ---
        int previousDamage = damageable.CurrentDamage;
        int newTotalDamage = previousDamage - healAmount;

        // Clamp the damage to a minimum of 0. You can't heal more than what's been damaged.
        newTotalDamage = Math.Max(0, newTotalDamage);

        // Update the data container's properties
        damageable.CurrentDamage = newTotalDamage;
        damageable.CurrentPercentageDamage = statEntry.PointsAsPercentageOfMax(newTotalDamage);
        // --- End of moved logic ---

        int actualHealAmount = previousDamage - newTotalDamage;
        if (actualHealAmount <= 0) return; // No healing actually occurred

        // Raise the event with rich context
        StatHealed?.Invoke(this, new StatHealEventArgs
        {
            HealAmount = actualHealAmount,
            HealPercentage = statEntry.PointsAsPercentageOfMax(actualHealAmount),
            SourceId = healOptions.SourceId,
            StatName = statEntry.Stat.Name
        });
    }

    public void SetPoints(IStat stat, int points)
    {
        if (stat.Variation is StatVariation.Complex or StatVariation.Minor)
        {
            _logger.LogIllegalStatChange(stat.Name, "Cannot directly set value of a complex/minor stat.");
            return;
        }

        if (stat.Value == points) return;

        int oldValue = stat.Value;
        int clampedValue = Math.Clamp(points, stat.MinValue, stat.MaxValue);

        stat.Value = clampedValue;
        // The service now decides how BaseValue is affected.
        // For simplicity, let's assume a direct change for now.
        stat.BaseValue += clampedValue - oldValue;

        stat.InvokeValueChanged(new ValueChangedEventArgs(oldValue, stat.Value));
        stat.InvokeBaseValueChanged();
    }

    public void LinkStats(IStat sourceStat, IStat dependentStat, float ratio)
    {
        ArgumentNullException.ThrowIfNull(sourceStat);
        ArgumentNullException.ThrowIfNull(dependentStat);

        if (dependentStat.LinkedStats.ContainsKey(sourceStat.Guid))
        {
            _logger.LogWarning("Stat {DependentStat} is already linked to {SourceStat}.",
                dependentStat.Name, sourceStat.Name);
            return;
        }

        // 1. Record the dependency in the data object.
        dependentStat.LinkedStats.Add(sourceStat.Guid, ratio);

        // 2. The SERVICE subscribes to the source stat's event.
        //    This is the core of the mediator pattern.
        sourceStat.ValueChanged += (sender, args) =>
        {
            // When the source changes, this logic in the service will execute.
            HandleLinkedStatChange(dependentStat, args, ratio);
        };

        _logger.LogInformation("Successfully linked {DependentStat} to {SourceStat} with a ratio of {Ratio}.",
            dependentStat.Name, sourceStat.Name, ratio);
    }


    public void RecalculateStatValue(IStat stat, IEnumerable<IModifier> modifiers)
    {
        // This logic was in the old StatEntry. It's now generalized here.
        // You would need a way to reset the stat's value to its BaseValue first.
        // stat.Value = stat.BaseValue;

        foreach (IModifier modifier in modifiers.OrderBy(m => m.Priority))
        {
            stat.ApplyModifier(modifier);
            _logger.LogModifierApplied(modifier.Name, stat.Name);
        }
    }

    public void ApplyDamage(StatEntry<IStat> statEntry, DamageOptions damageOptions)
    {
        // Ensure the stat is actually damageable
        if (!statEntry.IsDamageable || statEntry is not IDamageable damageable) return;

        // Calculate initial and final damage
        int incomingDamage = _damageCalculator.GetPointDamageFromOptions(damageOptions, statEntry.Stat);
        if (incomingDamage <= 0) return;
        int finalDamage = _damageCalculator.CalculateFinalDamage(incomingDamage, statEntry.Mitigations);
        if (finalDamage <= 0) return;

        // --- Logic moved from DamageableBehavior ---
        int previousDamage = damageable.CurrentDamage;
        int newTotalDamage = previousDamage + finalDamage;

        // Clamp the new total damage within the stat's valid range
        newTotalDamage = Math.Clamp(newTotalDamage, statEntry.Stat.MinValue, statEntry.Stat.MaxValue);

        // Update the data container's properties
        damageable.CurrentDamage = newTotalDamage;
        damageable.CurrentPercentageDamage = statEntry.PointsAsPercentageOfMax(newTotalDamage);
        // --- End of moved logic ---

        // Raise the event with rich context
        StatDamageTaken?.Invoke(this, new StatDamageEventArgs
        {
            IncomingDamage = incomingDamage,
            IncomingDamagePercentage = statEntry.PointsAsPercentageOfMax(incomingDamage),
            FinalDamage = finalDamage,
            FinalDamagePercentage = statEntry.PointsAsPercentageOfMax(finalDamage),
            SourceId = damageOptions.SourceId,
            StatName = statEntry.Stat.Name.Singular
        });
    }

    public void AddModifier(StatEntry<IStat> statEntry, IModifier modifier)
    {
        statEntry.Modifiers.Add(modifier);
        RecalculateStatValue(statEntry);
    }

    public void RemoveModifier(StatEntry<IStat> statEntry, IModifier modifier)
    {
        statEntry.Modifiers.Remove(modifier);
        RecalculateStatValue(statEntry);
    }

    public void AddMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation)
    {
        statEntry.Mitigations.Add(mitigation);
    }

    public void RemoveMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation)
    {
        statEntry.Mitigations.Remove(mitigation);
    }

    public event EventHandler<StatHealEventArgs>? StatHealed;

    /// <summary>
    ///     Handles the event when a source stat's value changes, and applies the
    ///     proportional change to the dependent stat.
    /// </summary>
    private void HandleLinkedStatChange(IStat dependentStat, ValueChangedEventArgs sourceArgs, float ratio)
    {
        // Calculate the proportional change based on the source's delta and the ratio.
        int change = (int)(sourceArgs.Delta * ratio);

        if (change == 0) return;

        // Use the service's own methods to apply the change, ensuring all
        // business logic and clamping is respected.
        this.AddPoints(dependentStat, change);

        _logger.LogDebug("Propagated change from linked stat. {DependentStat} changed by {Change}.",
            dependentStat.Name, change);
    }

    private void RecalculateStatValue(StatEntry<IStat> statEntry)
    {
        // NOTE: You'll need a way to reset the stat's current value before reapplying modifiers.
        // statEntry.Stat.ResetCurrentValue(); 

        foreach (IModifier modifier in statEntry.Modifiers.OrderBy(m => m.Priority))
            statEntry.Stat.ApplyModifier(modifier);
    }
}
