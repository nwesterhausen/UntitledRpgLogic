using Microsoft.Extensions.Logging;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     A stat meant to represent something abstract that doesn't behave like a normal stat.
/// </summary>
public class PseudoStat : StatBase
{
    /// <summary>
    ///     A stat meant to represent something abstract that doesn't behave like a normal stat.
    /// </summary>
    public PseudoStat(string name,
        int maxValue = 100,
        int value = STAT_DEFAULT_MIN_VALUE,
        int minValue = STAT_DEFAULT_MIN_VALUE,
        ILogger<StatBase>? logger = null)
        : base(new StatOptions
        {
            Variation = StatVariation.Pseudo,
            Name = name,
            MaxValue = maxValue,
            Value = value,
            MinValue = minValue,
            Logger = logger
        })
    {
    }
}