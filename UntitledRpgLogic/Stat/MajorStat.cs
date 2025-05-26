using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Stat;

/// <summary>
///   A major stat is one that can be directly modified by the player.
/// </summary>
public class MajorStat(
    string name,
    int maxValue = StatBase.STAT_DEFAULT_MAX_VALUE,
    int value = StatBase.STAT_DEFAULT_MIN_VALUE,
    int minValue = StatBase.STAT_DEFAULT_MIN_VALUE,
    ILogger<StatBase>? logger = null)
    : StatBase(StatVariation.Major, name, maxValue, minValue, value, logger);