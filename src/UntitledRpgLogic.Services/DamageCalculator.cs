using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Effects;
using UntitledRpgLogic.Core.Interfaces.Services;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Implements the IDamageCalculator interface to calculate final damage after applying mitigations.
/// </summary>
public class DamageCalculator : IDamageCalculator
{
	/// <inheritdoc />
	public int CalculateFinalDamage(int damageAmount, IEnumerable<IAppliesDamageMitigation> mitigations)
	{
		var modifiedDamage = damageAmount;

		// Sort and apply mitigation effects in order.
		foreach (var mitigation in mitigations.OrderBy(m => m.MitigationPriority))
		{
			modifiedDamage = mitigation.ApplyMitigation(modifiedDamage);
		}

		return modifiedDamage;
	}

	/// <inheritdoc />
	public int GetPointDamageFromOptions(DamageOptions damageOptions, IStat stat)
	{
		ArgumentNullException.ThrowIfNull(damageOptions, nameof(damageOptions));
		ArgumentNullException.ThrowIfNull(stat, nameof(stat));

		if (damageOptions.FlatDamage.HasValue)
		{
			return damageOptions.FlatDamage.Value;
		}

		if (damageOptions.PercentageDamage.HasValue)
		{
			return (int)(stat.Value * (damageOptions.PercentageDamage / 100f));
		}

		if (damageOptions.PercentageDamageOfMax.HasValue)
		{
			return (int)(stat.MaxValue * (damageOptions.PercentageDamageOfMax / 100f));
		}

		// If no damage options are provided, return 0.
		return 0;
	}
}
