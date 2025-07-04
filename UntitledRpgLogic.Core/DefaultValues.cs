using System.Diagnostics.CodeAnalysis;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core;

/// <summary>
///     Default values used in the RPG logic.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class DefaultValues
{
    /// <summary>
    ///     Default maximum value for a stat.
    /// </summary>
    public const int STAT_DEFAULT_MAX_VALUE = 1024;

    /// <summary>
    ///     Default minimum value for a stat.
    /// </summary>
    public const int STAT_DEFAULT_MIN_VALUE = 0;

    public const int SKILL_DEFAULT_MAX_LEVEL = 1024;
    public const float SKILL_DEFAULT_SCALING_ALPHA = 1f;
    public const float SKILL_DEFAULT_SCALING_BETA = 1f;
    public const float SKILL_DEFAULT_SCALING_GAMMA = 1f;
    public const int SKILL_DEFAULT_POINTS_FOR_FIRST_LEVEL = 1;
    public const ScalingCurveType SKILL_DEFAULT_SCALING_CURVE = ScalingCurveType.Linear;
}
