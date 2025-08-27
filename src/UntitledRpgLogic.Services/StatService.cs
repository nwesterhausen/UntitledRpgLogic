using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Effects;
using UntitledRpgLogic.Core.Interfaces.Services;
using UntitledRpgLogic.Core.Options;
using UntitledRpgLogic.Extensions.Logging;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Provides business logic for managing and operating on Stat objects and StatEntries.
///     This service handles stat modification, damage, healing, and linking.
/// </summary>
public class StatService : IStatService
{
	private readonly IDamageCalculator damageCalculator;
	private readonly ILogger<StatService> logger;

	/// <summary>
	///     Initializes a new instance of the <see cref="StatService" /> class.
	/// </summary>
	/// <param name="damageCalculator">The service used for damage calculations.</param>
	/// <param name="logger">The logger for recording service operations.</param>
	public StatService(IDamageCalculator damageCalculator, ILogger<StatService> logger)
	{
		this.damageCalculator = damageCalculator;
		this.logger = logger;
	}

	/// <inheritdoc />
	public event EventHandler<StatDamageEventArgs>? StatDamageTaken;

	/// <inheritdoc />
	public event EventHandler<StatHealEventArgs>? StatHealed;

	/// <inheritdoc />
	public void Heal(StatEntry<IStat> statEntry, HealOptions healOptions)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		ArgumentNullException.ThrowIfNull(healOptions, nameof(healOptions));

		// Ensure the stat can be healed and is currently damaged
		if (!statEntry.IsHealable || statEntry is not IDamageable { CurrentDamage: > 0 } damageable)
		{
			return;
		}

		var healAmount = healOptions.BaseAmount; // Can be expanded with more complex logic later
		if (healAmount <= 0)
		{
			return;
		}

		var previousDamage = damageable.CurrentDamage;
		var newTotalDamage = Math.Max(0, previousDamage - healAmount);

		damageable.CurrentDamage = newTotalDamage;
		damageable.CurrentPercentageDamage = statEntry.PointsAsPercentageOfMax(newTotalDamage);

		var actualHealAmount = previousDamage - newTotalDamage;
		if (actualHealAmount <= 0)
		{
			return;
		}

		this.StatHealed?.Invoke(this,
			new StatHealEventArgs
			{
				HealAmount = actualHealAmount,
				HealPercentage = statEntry.PointsAsPercentageOfMax(actualHealAmount),
				SourceId = healOptions.SourceId,
				StatName = statEntry.Stat.Name.Singular
			});
	}

	/// <inheritdoc />
	public void AddPoints(IStat target, int points)
	{
		if (points == 0)
		{
			return;
		}

		ArgumentNullException.ThrowIfNull(target);

		target.BaseValue += points;
	}

	/// <inheritdoc />
	public void RemovePoints(IStat target, int points) => this.AddPoints(target, -points);

	/// <inheritdoc />
	public void SetPoints(IStat stat, int points)
	{
		ArgumentNullException.ThrowIfNull(stat, nameof(stat));

		if (stat.Variation is StatVariation.Complex or StatVariation.Minor)
		{
			// this.logger.LogIllegalStatChange(stat.Name.Singular, "Cannot directly set value of a complex/minor stat.");
			return;
		}

		if (stat.Value == points)
		{
			return;
		}

		var oldValue = stat.Value;
		var clampedValue = Math.Clamp(points, stat.MinValue, stat.MaxValue);

		stat.Value = clampedValue;
		stat.BaseValue += clampedValue - oldValue;

		stat.InvokeValueChanged(new ValueChangedEventArgs(oldValue, stat.Value));
		stat.InvokeBaseValueChanged(new ValueChangedEventArgs(stat.BaseValue - (clampedValue - oldValue), stat.BaseValue));
	}

	/// <inheritdoc />
	public void LinkStats(IStat sourceStat, IStat dependentStat, float ratio)
	{
		ArgumentNullException.ThrowIfNull(sourceStat);
		ArgumentNullException.ThrowIfNull(dependentStat);

		if (!dependentStat.LinkedStats.TryAdd(sourceStat.Identifier, ratio))
		{
			this.logger.LogWarning("Stat {DependentStat} is already linked to {SourceStat}.",
				dependentStat.Name, sourceStat.Name);
			return;
		}

		sourceStat.ValueChanged += (sender, args) => this.HandleLinkedStatChange(dependentStat, args, ratio);

		this.logger.LogInformation("Successfully linked {DependentStat} to {SourceStat} with a ratio of {Ratio}.",
			dependentStat.Name, sourceStat.Name, ratio);
	}

	/// <inheritdoc />
	public void ApplyDamage(StatEntry<IStat> statEntry, DamageOptions damageOptions)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		ArgumentNullException.ThrowIfNull(damageOptions, nameof(damageOptions));

		if (!statEntry.IsDamageable || statEntry is not IDamageable damageable)
		{
			return;
		}

		var incomingDamage = this.damageCalculator.GetPointDamageFromOptions(damageOptions, statEntry.Stat);
		if (incomingDamage <= 0)
		{
			return;
		}

		var finalDamage = this.damageCalculator.CalculateFinalDamage(incomingDamage, statEntry.Mitigations);
		if (finalDamage <= 0)
		{
			return;
		}

		var previousDamage = damageable.CurrentDamage;
		var newTotalDamage = Math.Clamp(previousDamage + finalDamage, statEntry.Stat.MinValue, statEntry.Stat.MaxValue);

		damageable.CurrentDamage = newTotalDamage;
		damageable.CurrentPercentageDamage = statEntry.PointsAsPercentageOfMax(newTotalDamage);

		this.StatDamageTaken?.Invoke(this,
			new StatDamageEventArgs
			{
				IncomingDamage = incomingDamage,
				IncomingDamagePercentage = statEntry.PointsAsPercentageOfMax(incomingDamage),
				FinalDamage = finalDamage,
				FinalDamagePercentage = statEntry.PointsAsPercentageOfMax(finalDamage),
				SourceId = damageOptions.SourceId,
				StatName = statEntry.Stat.Name.Singular
			});
	}

	/// <inheritdoc />
	public void AddModifier(StatEntry<IStat> statEntry, IModifier modifier)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		statEntry.Modifiers.Add(modifier);
		this.RecalculateStatValue(statEntry);
	}

	/// <inheritdoc />
	public void RemoveModifier(StatEntry<IStat> statEntry, IModifier modifier)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		_ = statEntry.Modifiers.Remove(modifier);
		this.RecalculateStatValue(statEntry);
	}

	/// <inheritdoc />
	public void AddMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		ArgumentNullException.ThrowIfNull(mitigation, nameof(mitigation));
		statEntry.Mitigations.Add(mitigation);
	}

	/// <inheritdoc />
	public void RemoveMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation)
	{
		ArgumentNullException.ThrowIfNull(statEntry, nameof(statEntry));
		ArgumentNullException.ThrowIfNull(mitigation, nameof(mitigation));
		_ = statEntry.Mitigations.Remove(mitigation);
	}

	/// <summary>
	///     Handles the event when a source stat's value changes, and applies the
	///     proportional change to the dependent stat.
	/// </summary>
	private void HandleLinkedStatChange(IStat dependentStat, ValueChangedEventArgs sourceArgs, float ratio)
	{
		var change = (int)(sourceArgs.Delta * ratio);
		if (change == 0)
		{
			return;
		}

		this.AddPoints(dependentStat, change);

		this.logger.LogDebug("Propagated change from linked stat. {DependentStat} changed by {Change}.",
			dependentStat.Name, change);
	}

	/// <summary>
	///     Recalculates a stat's final value by resetting it to its base value
	///     and then applying all active modifiers in order of priority.
	/// </summary>
	private void RecalculateStatValue(StatEntry<IStat> statEntry)
	{
		var stat = statEntry.Stat;
		var oldValue = stat.Value;

		stat.Value = stat.BaseValue;

		foreach (var modifier in statEntry.Modifiers.OrderBy(m => m.Priority))
		{
			stat.Value = modifier.ApplyModification(stat.BaseValue, stat.Value, stat.MaxValue);
			// this.logger.LogModifierApplied(modifier.Name.Singular, stat.Name.Singular);
		}

		if (oldValue != stat.Value)
		{
			stat.InvokeValueChanged(new ValueChangedEventArgs(oldValue, stat.Value));
		}
	}
}
