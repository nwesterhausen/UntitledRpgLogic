using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Implements the IDamageCalculator interface to calculate final damage after applying mitigations.
/// </summary>
public class DamageCalculator : IDamageCalculator
{
	/// <inheritdoc />
	public int CalculateFinalDamage(int damageAmount)
	{
		var modifiedDamage = damageAmount;

		// Sort and apply mitigation effects in order.
		// foreach (var mitigation in mitigations.OrderBy(m => m.MitigationPriority))
		// {
		// 	modifiedDamage = mitigation.ApplyMitigation(modifiedDamage);
		// }

		return modifiedDamage;
	}

	/// <inheritdoc />
	public int GetPointDamageFromOptions(DamageOptions damageOptions, Stat stat)
	{
		ArgumentNullException.ThrowIfNull(damageOptions, nameof(damageOptions));
		ArgumentNullException.ThrowIfNull(stat, nameof(stat));
		ArgumentNullException.ThrowIfNull(stat.StatDefinition, nameof(StatDefinition));

		var statDefinition = stat.StatDefinition;

		if (damageOptions.FlatDamage.HasValue)
		{
			return damageOptions.FlatDamage.Value;
		}

		if (damageOptions.PercentageDamage.HasValue)
		{
			return (int)(stat.ApparentValue * (damageOptions.PercentageDamage / 100f));
		}

		if (damageOptions.PercentageDamageOfMax.HasValue)
		{
			return (int)(statDefinition.MaxValue * (damageOptions.PercentageDamageOfMax / 100f));
		}

		// If no damage options are provided, return 0.
		return 0;
	}
}
