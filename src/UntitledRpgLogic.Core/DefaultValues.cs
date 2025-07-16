using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core;

/// <summary>
///     Default values used in the RPG logic.
/// </summary>
public static class DefaultValues
{
	/// <summary>
	///     Default maximum value for a stat.
	/// </summary>
	public const int StatDefaultMaxValue = 1024;

	/// <summary>
	///     Default minimum value for a stat.
	/// </summary>
	public const int StatDefaultMinValue = 0;

	/// <summary>
	///     Default maximum level for a skill.
	/// </summary>
	public const int SkillDefaultMaxLevel = 1024;

	/// <summary>
	///     Default scaling factor a for skill leveling.
	/// </summary>
	public const float SkillDefaultScalingAlpha = 1f;

	/// <summary>
	///     Default scaling factor b for skill leveling.
	/// </summary>
	public const float SkillDefaultScalingBeta = 1f;

	/// <summary>
	///     Default scaling factor c for skill leveling.
	/// </summary>
	public const float SkillDefaultScalingGamma = 1f;

	/// <summary>
	///     Default points required for the first level of a skill.
	/// </summary>
	public const int SkillDefaultPointsForFirstLevel = 1;

	/// <summary>
	///     Default scaling curve type for skill leveling.
	/// </summary>
	public const ScalingCurveType SkillDefaultScalingCurve = ScalingCurveType.Linear;
}
