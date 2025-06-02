using Microsoft.Extensions.Logging;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     Represents a minor stat that is dependent on one or more major stats.
/// </summary>
public class MinorStat : StatBase
{
    /// <summary>
    ///     Major stats that this minor stat is dependent on, and the ratio they contribute to this minor stat.
    /// </summary>
    private readonly Dictionary<MajorStat, double> _majorStats = new();

    /// <summary>
    ///     Represents a minor stat that is dependent on one or more major stats.
    /// </summary>
    public MinorStat(string name,
        int maxValue = STAT_DEFAULT_MAX_VALUE,
        int value = STAT_DEFAULT_MIN_VALUE,
        int minValue = STAT_DEFAULT_MIN_VALUE,
        ILogger<StatBase>? logger = null)
        : base(new StatOptions
        {
            Variation = StatVariation.Minor,
            Name = name,
            MaxValue = maxValue,
            Value = value,
            MinValue = minValue,
            Logger = logger
        })
    {
    }

    /// <summary>
    ///     Gets a value indicating whether this minor stat is dependent on any major stats.
    /// </summary>
    public bool IsDependent => _majorStats.Count > 0;

    /// <summary>
    ///     Determines whether this minor stat is dependent on the specified major stat.
    /// </summary>
    /// <param name="majorStat">The major stat to check.</param>
    /// <returns>True if dependent; otherwise, false.</returns>
    public bool IsDependentOn(MajorStat majorStat)
    {
        return _majorStats.ContainsKey(majorStat);
    }

    /// <summary>
    ///     Links a major stat to this minor stat with a specified ratio.
    /// </summary>
    /// <param name="majorStat">The major stat to link.</param>
    /// <param name="ratio">The ratio of contribution.</param>
    public void Link(MajorStat majorStat, double ratio)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Unlinks a major stat from this minor stat.
    /// </summary>
    /// <param name="majorStat">The major stat to unlink.</param>
    public void Unlink(MajorStat majorStat)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Calculate the value of this minor stat based on the major stats it depends on.
    /// </summary>
    private void RecalculateValue()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Alert the minor stat that a major stat has changed.
    /// </summary>
    /// <param name="majorStat">The major stat that changed.</param>
    /// <exception cref="ArgumentException">Thrown if the major stat is not found in dependencies.</exception>
    public void HandleStatChange(MajorStat majorStat)
    {
        throw new NotImplementedException();
    }
}