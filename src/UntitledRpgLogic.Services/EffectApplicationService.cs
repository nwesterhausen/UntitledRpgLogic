using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Applies domain effect payloads to target entities, delegating damage calculations
///     and updating apparent and base stat values in memory.
/// </summary>
public sealed class EffectApplicationService(IDamageCalculator damageCalculator) : IEffectApplicationService
{
	private readonly IDamageCalculator damageCalculator =
		damageCalculator ?? throw new ArgumentNullException(nameof(damageCalculator));

	/// <inheritdoc />
	public void ApplyEffect(Effect effect, Entity? caster = null, IEnumerable<Entity>? targets = null)
	{
		ArgumentNullException.ThrowIfNull(effect);

		if (targets is null)
		{
			return;
		}

		foreach (var target in targets)
		{
			switch (effect)
			{
				case DamageEffect damageEffect:
					this.ApplyDamage(damageEffect, target);
					break;

				case HealEffect healEffect:
					ApplyHeal(healEffect, target);
					break;

				case BuffEffect or DebuffEffect:
					ApplyStatDeltas(effect, target);
					break;

				default:
					// Process raw AffectedStats if present on custom or base effects
					ApplyStatDeltas(effect, target);
					break;
			}
		}
	}

	/// <summary>
	///     Apply a damage effect.
	/// </summary>
	/// <param name="effect">Effect to apply</param>
	/// <param name="target">Target entity</param>
	private void ApplyDamage(DamageEffect effect, Entity target)
	{
		// Damage defaults to modifying Health
		var healthStat = FindStat(target, "Health");
		if (healthStat is null)
		{
			return;
		}

		var damageOptions = new DamageOptions { FlatDamage = (int)MathF.Round(effect.BaseDamage) };

		var rawDamage = this.damageCalculator.CalculatePointDamage(damageOptions, healthStat);

		var finalDamage = effect.IgnoresArmor
			? rawDamage
			: this.damageCalculator.CalculateMitigatedDamage(rawDamage, 0f, 0);

		var minAllowed = healthStat.Definition?.MinValue ?? 0;
		healthStat.ApparentValue = Math.Max(minAllowed, healthStat.ApparentValue - finalDamage);
	}

	/// <summary>
	///     Apply a heal effect.
	/// </summary>
	/// <param name="effect">Effect to apply</param>
	/// <param name="target">Target entity</param>
	private static void ApplyHeal(HealEffect effect, Entity target)
	{
		var healthStat = FindStat(target, "Health");
		if (healthStat is null)
		{
			return;
		}

		var healAmount = (int)MathF.Round(effect.BaseHealAmount);
		var targetValue = healthStat.ApparentValue + healAmount;

		if (!effect.CanOverheal && healthStat.Definition is not null)
		{
			targetValue = Math.Min(healthStat.Definition.MaxValue, targetValue);
		}

		healthStat.ApparentValue = targetValue;
	}


	/// <summary>
	///     Generically apply changes to stats from any <see cref="Effect" />.
	/// </summary>
	/// <param name="effect">Effect to apply</param>
	/// <param name="target">Target entity</param>
	private static void ApplyStatDeltas(Effect effect, Entity target)
	{
		if (effect.AffectedStats is null or { Count: 0 })
		{
			return;
		}

		foreach (var delta in effect.AffectedStats)
		{
			var stat = target.Stats
				.FirstOrDefault(es => es.InstancedStat?.DefinitionId == delta.StatId)
				?.InstancedStat;

			if (stat is null)
			{
				continue;
			}

			var change = delta.IsPercentage
				? (int)MathF.Round(stat.ApparentValue * delta.AmountChange)
				: (int)MathF.Round(delta.AmountChange);

			var min = stat.Definition?.MinValue ?? int.MinValue;
			var max = stat.Definition?.MaxValue ?? int.MaxValue;

			stat.ApparentValue = Math.Clamp(stat.ApparentValue + change, min, max);
		}
	}

	/// <summary>
	///     Tries to get a <see cref="Stat" /> that belongs to an <see cref="Entity" />.
	/// </summary>
	/// <param name="target">Entity to find stat on</param>
	/// <param name="statId">The Id of the stat's definition</param>
	/// <returns>The stat if it was found, otherwise null</returns>
	private static Stat? FindStat(Entity target, Ulid statId) =>
		target.Stats
			.FirstOrDefault(s => s.InstancedStat?.DefinitionId == statId)
			?.InstancedStat;

	/// <summary>
	///     Tries to get a <see cref="Stat" /> that belongs to an <see cref="Entity" />.
	/// </summary>
	/// <param name="target">Entity to find stat on</param>
	/// <param name="statName">The name to search the stat by</param>
	/// <returns>The stat if it was found, otherwise null</returns>
	private static Stat? FindStat(Entity target, string statName) =>
		target.Stats
			.FirstOrDefault(s => string.Equals(s.InstancedStat?.Definition?.Name.Singular, statName,
				StringComparison.OrdinalIgnoreCase))
			?.InstancedStat;
}
