using Microsoft.Extensions.Logging;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     A major stat is one that can be directly modified by the player.
/// </summary>
public class MajorStat : StatBase
{
    /// <summary>
    ///     A major stat is one that can be directly modified by the player.
    /// </summary>
    public MajorStat(string name,
        int maxValue = STAT_DEFAULT_MAX_VALUE,
        int value = STAT_DEFAULT_MIN_VALUE,
        int minValue = STAT_DEFAULT_MIN_VALUE,
        ILogger<StatBase>? logger = null)
        : base(new StatOptions
        {
            Variation = StatVariation.Major,
            Name = name,
            MaxValue = maxValue,
            Value = value,
            MinValue = minValue,
            Logger = logger
        })
    {
    }
}