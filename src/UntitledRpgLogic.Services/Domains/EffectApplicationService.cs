using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services.Domains;

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
			this.ApplyStatDelta(effect, target);
		}
	}


	/// <summary>
	///     Generically apply changes to a stat from any <see cref="Effect" />.
	/// </summary>
	/// <param name="effect">Effect to apply</param>
	/// <param name="target">Target entity</param>
	private void ApplyStatDelta(Effect effect, Entity target)
	{
		if (effect.AffectedStat is null)
		{
			return;
		}

		var stat = target.Stats
			.FirstOrDefault(es => es.InstancedStat?.DefinitionId == effect.AffectedStat.StatId)
			?.InstancedStat;

		if (stat is null)
		{
			return;
		}

		var change = this.statCalculationService.CalculatePointChange(effect.AffectedStat, stat);

		var min = stat.Definition?.MinValue ?? int.MinValue;
		var max = stat.Definition?.MaxValue ?? int.MaxValue;

		if (effect is HealEffect { CanOverheal: true } && effect.AffectedStat.IsPositive)
		{
			stat.ApparentValue = Math.Clamp(stat.ApparentValue + change, min, int.MaxValue);
			return;
		}

		stat.ApparentValue = effect.AffectedStat.IsPositive
			? Math.Clamp(stat.ApparentValue + change, min, max)
			: Math.Clamp(stat.ApparentValue - change, min, max);
	}
}
