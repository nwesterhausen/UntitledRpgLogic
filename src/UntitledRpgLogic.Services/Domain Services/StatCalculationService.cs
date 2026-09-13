using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for calculating stat apparent values, min-max bounds, and linked dependencies.
/// </summary>
public sealed class StatCalculationService : IStatCalculationService
{
	/// <inheritdoc />
	public int CalculateApparentValue(Stat targetStat, IEnumerable<Stat> dependencyStats)
	{
		ArgumentNullException.ThrowIfNull(targetStat);
		ArgumentNullException.ThrowIfNull(dependencyStats);

		var netOffset = 0.0f;

		if (targetStat.Definition?.LinkedStats is { Count: > 0 })
		{
			var lookup = dependencyStats.ToDictionary(s => s.DefinitionId);

			foreach (var link in targetStat.Definition.LinkedStats)
			{
				if (lookup.TryGetValue(link.DependsOnId, out var sourceStat))
				{
					netOffset += sourceStat.ApparentValue * link.Ratio;
				}
			}
		}

		var candidateValue = targetStat.BaseValue + (int)MathF.Round(netOffset);

		return targetStat.Definition is not null
			? this.ClampToDefinitionBounds(targetStat.Definition, candidateValue)
			: candidateValue;
	}

	/// <inheritdoc />
	public int ClampToDefinitionBounds(StatDefinition definition, int rawValue)
	{
		ArgumentNullException.ThrowIfNull(definition);
		return Math.Clamp(rawValue, definition.MinValue, definition.MaxValue);
	}

	/// <inheritdoc />
	public int CalculatePointChange(StatChangeOptions options, Stat targetStat)
	{
		ArgumentNullException.ThrowIfNull(options);
		ArgumentNullException.ThrowIfNull(targetStat);

		var total = 0f;

		if (options.FlatChange.HasValue)
		{
			total += Math.Max(0, options.FlatChange.Value);
		}

		if (options.PercentageChange is > 0f)
		{
			total += targetStat.ApparentValue * options.PercentageChange.Value;
		}

		if (options.PercentageChangeOfMax is > 0f)
		{
			var maxCap = targetStat.Definition?.MaxValue ?? targetStat.ApparentValue;
			total += maxCap * options.PercentageChangeOfMax.Value;
		}

		return (int)MathF.Round(MathF.Max(0f, total));
	}

	/// <inheritdoc />
	public int CalculateMitigatedPointChange(int rawDamage, float resistancePercent, int flatMitigation)
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
