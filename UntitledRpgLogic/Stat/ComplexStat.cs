using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Stat;

/// <summary>
/// Represents a complex stat in the system.
/// </summary>
public class ComplexStat(
    string name,
    int maxValue = StatBase.STAT_DEFAULT_MAX_VALUE,
    int value = StatBase.STAT_DEFAULT_MIN_VALUE,
    int minValue = StatBase.STAT_DEFAULT_MIN_VALUE,
    ILogger<StatBase>? logger = null)
    : StatBase(StatVariation.Complex, name, maxValue, value, minValue, logger);