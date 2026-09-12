using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Implements the IDamageCalculator interface to calculate final damage after applying mitigations.
/// </summary>
public class DamageCalculator : IDamageCalculator
{
	/// <inheritdoc />
	public int CalculatePointDamage(DamageOptions options, Stat targetStat)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentNullException.ThrowIfNull(targetStat);

		var total = 0f;

		if (options.FlatDamage.HasValue)
		{
			total += Math.Max(0, options.FlatDamage.Value);
		}

		if (options.PercentageDamage is > 0f)
		{
			total += targetStat.ApparentValue * options.PercentageDamage.Value;
		}

		if (options.PercentageDamageOfMax is > 0f)
		{
			var maxCap = targetStat.Definition?.MaxValue ?? targetStat.ApparentValue;
			total += maxCap * options.PercentageDamageOfMax.Value;
		}

		return (int)MathF.Round(MathF.Max(0f, total));
	}

	/// <inheritdoc />
	public int CalculateMitigatedDamage(int rawDamage, float resistancePercent, int flatMitigation)
	{
		if (rawDamage <= 0)
		{
			return 0;
		}

		// Clamp resistance between 0% and 100%
		var clampedResistance = Math.Clamp(resistancePercent, 0f, 1.0f);
		var postResistance = rawDamage * (1.0f - clampedResistance);
		var finalDamage = (int)MathF.Round(postResistance) - flatMitigation;

		return Math.Max(0, finalDamage);
	}
}
