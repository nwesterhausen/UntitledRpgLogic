using UntitledRpgLogic.Core;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions to provide tooltip-related functionality for various objects in the RPG logic.
/// </summary>
public static class StatExtensions
{
	/// <summary>
	///     Explicitly converts a stat to its string representation.
	/// </summary>
	/// <param name="stat">The stat to convert.</param>
	public static string IntoString(this Stat stat)
	{
		ArgumentNullException.ThrowIfNull(stat);
		ArgumentNullException.ThrowIfNull(stat.Definition);

		var definition = stat.Definition;

		if (stat.MinValue == DefaultValues.StatDefaultMinValue)
		{
			return
				$"{definition.Variation} {definition.Name}: {stat.ApparentValue} / {stat.MaxValue} ({stat.ApparentValue / (float)stat.MaxValue:F2 * 100}";
		}

		return
			$"{definition.Variation} {definition.Name}: {stat.ApparentValue} / {stat.MaxValue} with {stat.MinValue} minimum ({stat.EffectiveValue / (float)(stat.MaxValue - stat.MinValue):F2})%";
	}
}
