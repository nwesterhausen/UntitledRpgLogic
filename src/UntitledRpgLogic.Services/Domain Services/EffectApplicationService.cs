using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Applies domain effect payloads to target entities, delegating damage calculations
///     and updating apparent and base stat values in memory.
/// </summary>
public sealed class EffectApplicationService(IStatCalculationService statCalculationService) : IEffectApplicationService
{
	private readonly IStatCalculationService statCalculationService =
		statCalculationService ?? throw new ArgumentNullException(nameof(statCalculationService));

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
			this.ApplyStatDeltas(effect, target);
		}
	}


	/// <summary>
	///     Generically apply changes to stats from any <see cref="Effect" />.
	/// </summary>
	/// <param name="effect">Effect to apply</param>
	/// <param name="target">Target entity</param>
	private void ApplyStatDeltas(Effect effect, Entity target)
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

			var change = this.statCalculationService.CalculatePointChange(delta, stat);

			var min = stat.Definition?.MinValue ?? int.MinValue;
			var max = stat.Definition?.MaxValue ?? int.MaxValue;

			if (effect is HealEffect { CanOverheal: true } && delta.IsPositive)
			{
				stat.ApparentValue = Math.Clamp(stat.ApparentValue + change, min, int.MaxValue);
				return;
			}

			stat.ApparentValue = delta.IsPositive
				? Math.Clamp(stat.ApparentValue + change, min, max)
				: Math.Clamp(stat.ApparentValue - change, min, max);
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
