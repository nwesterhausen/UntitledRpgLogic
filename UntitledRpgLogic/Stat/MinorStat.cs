using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     Represents a minor stat that is dependent on one or more major stats.
/// </summary>
public class MinorStat(
    string name,
    int maxValue = StatBase.STAT_DEFAULT_MAX_VALUE,
    int value =
        StatBase.STAT_DEFAULT_MIN_VALUE,
    int minValue = StatBase.STAT_DEFAULT_MIN_VALUE,
    ILogger<StatBase>? logger = null)
    : StatBase(StatVariation.Minor, name, maxValue, value, minValue, logger)
{
    /// <summary>
    ///     Stores event handlers for each linked major stat.
    /// </summary>
    private readonly Dictionary<MajorStat, EventHandler<ValueChangedEventArgs>> _majorStatHandlers = new();

    /// <summary>
    ///     Major stats that this minor stat is dependent on, and the ratio they contribute to this minor stat.
    /// </summary>
    private readonly Dictionary<MajorStat, double> _majorStats = new();

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
        if (_majorStats.TryGetValue(majorStat, out var value))
            if (Math.Abs(ratio - value) < double.Epsilon)
                return;

        _majorStats[majorStat] = ratio;
        EventHandler<ValueChangedEventArgs> handler = (sender, args) => RecalculateValue();
        majorStat.ValueChanged += handler;
        _majorStatHandlers[majorStat] = handler;
        LogStatLinked(majorStat.Name, Name, ratio);
        RecalculateValue();
    }

    /// <summary>
    ///     Unlinks a major stat from this minor stat.
    /// </summary>
    /// <param name="majorStat">The major stat to unlink.</param>
    public void Unlink(MajorStat majorStat)
    {
        if (!_majorStats.Remove(majorStat) || !_majorStatHandlers.TryGetValue(majorStat, out var handler)) return;

        majorStat.ValueChanged -= handler;
        _majorStatHandlers.Remove(majorStat);
        LogStatUnlinked(majorStat.Name, Name);
        RecalculateValue();
    }

    /// <summary>
    ///     Calculate the value of this minor stat based on the major stats it depends on.
    /// </summary>
    private void RecalculateValue()
    {
        var total = _majorStats.Sum(majorStat => majorStat.Key.Value * majorStat.Value);
        Value = (int)total;
    }

    /// <summary>
    ///     Alert the minor stat that a major stat has changed.
    /// </summary>
    /// <param name="majorStat">The major stat that changed.</param>
    /// <exception cref="ArgumentException">Thrown if the major stat is not found in dependencies.</exception>
    public void HandleStatChange(MajorStat majorStat)
    {
        if (!_majorStats.ContainsKey(majorStat))
            throw new ArgumentException("Major stat not found in dependencies.", nameof(majorStat));

        foreach (var stat in _majorStats.Where(stat =>
                     stat.Key == majorStat && stat.Key.Value != majorStat.Value))
            stat.Key.Value = majorStat.Value;

        RecalculateValue();
    }

    /// <summary>
    ///     Logs and prevents illegal attempts to add points to a minor stat.
    /// </summary>
    /// <param name="points">The number of points to add.</param>
    public sealed override void AddPoints(int points)
    {
        LogIllegalStatChangeAttempt(Name, $"+{points}");
    }

    /// <summary>
    ///     Logs and prevents illegal attempts to subtract points from a minor stat.
    /// </summary>
    /// <param name="points">The number of points to subtract.</param>
    public sealed override void SubtractPoints(int points)
    {
        LogIllegalStatChangeAttempt(Name, $"-{points}");
    }
}