using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic;

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
}
