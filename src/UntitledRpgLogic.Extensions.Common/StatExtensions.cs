using UntitledRpgLogic.Core;
using UntitledRpgLogic.Core.Interfaces;

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
	public static string IntoString(this IStat stat)
	{
		ArgumentNullException.ThrowIfNull(stat);
		if (stat.MinValue == DefaultValues.StatDefaultMinValue)
		{
			return
				$"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} ({stat.Value / (float)stat.MaxValue:F2 * 100}";
		}

		return
			$"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} with {stat.MinValue} minimum ({stat.EffectiveValue:F2})%";
	}
}
