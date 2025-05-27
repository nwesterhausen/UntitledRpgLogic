using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     A stat meant to represent something abstract that doesn't behave like a normal stat.
/// </summary>
public class PseudoStat(
    string name,
    int maxValue = 100,
    int value =
        StatBase.STAT_DEFAULT_MIN_VALUE,
    int minValue = StatBase.STAT_DEFAULT_MIN_VALUE,
    ILogger<StatBase>? logger = null)
    : StatBase(StatVariation.Pseudo, name, maxValue, value, minValue, logger);