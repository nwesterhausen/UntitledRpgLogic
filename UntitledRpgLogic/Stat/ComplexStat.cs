using Microsoft.Extensions.Logging;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     Represents a complex stat in the system, either with a complex calculation or multiple linked stats.
/// </summary>
public class ComplexStat : StatBase
{
    /// <summary>
    ///     Represents a complex stat in the system, either with a complex calculation or multiple linked stats.
    /// </summary>
    public ComplexStat(string name,
        int maxValue = STAT_DEFAULT_MAX_VALUE,
        int value = STAT_DEFAULT_MIN_VALUE,
        int minValue = STAT_DEFAULT_MIN_VALUE,
        ILogger<StatBase>? logger = null)
        : base(new StatOptions
        {
            Variation = StatVariation.Complex,
            Name = name,
            MaxValue = maxValue,
            Value = value,
            MinValue = minValue,
            Logger = logger
        })
    {
    }
}