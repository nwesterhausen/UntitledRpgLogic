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
}
