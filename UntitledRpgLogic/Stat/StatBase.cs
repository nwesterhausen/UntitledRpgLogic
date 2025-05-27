using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace UntitledRpgLogic.Stat;

/// <summary>
///     Abstract base class for RPG stats.
/// </summary>
public abstract partial class StatBase : IComparable<int>, IComparable<StatBase>, INotifyValueChanged
{
    /// <summary>
    ///     Default maximum value for a stat.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public const int STAT_DEFAULT_MAX_VALUE = 1024;

    /// <summary>
    ///     Default minimum value for a stat.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public const int STAT_DEFAULT_MIN_VALUE = 0;

    /// <summary>
    ///     Private reference for the stat value
    /// </summary>
    private int _value;

    /// <summary>
    ///     Represents a stat
    /// </summary>
    /// <param name="variation"></param>
    /// <param name="name"></param>
    /// <param name="maxValue"></param>
    /// <param name="value"></param>
    /// <param name="minValue"></param>
    /// <param name="logger"></param>
    protected StatBase(
        StatVariation variation,
        string name,
        int maxValue = STAT_DEFAULT_MAX_VALUE,
        int value = STAT_DEFAULT_MIN_VALUE,
        int minValue = STAT_DEFAULT_MIN_VALUE,
        ILogger<StatBase>? logger = null)
    {
        _logger = logger ?? NullLogger<StatBase>.Instance;
        Variation = variation;
        Name = name;
        _value = value;
        MaxValue = maxValue;
        MinValue = minValue > MaxValue ? MaxValue : minValue;
        if (value > MaxValue)
            _value = MaxValue;
        else if (value < MinValue)
            _value = MinValue;
        else
            _value = value;
        LogStatCreated(this);
    }

    /// <summary>
    ///     The name of the stat.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The value of the stat.
    /// </summary>
    public int Value
    {
        get => _value;
        internal set
        {
            if (value == _value) return;

            var oldValue = _value;
            _value = value;
            if (_value > MaxValue) _value = MaxValue;

            if (_value < MinValue) _value = MinValue;

            OnValueChanged(oldValue, _value);
        }
    }

    /// <summary>
    ///     The maximum value of the stat.
    /// </summary>
    public int MaxValue { get; }

    /// <summary>
    ///     The minimum value of the stat.
    /// </summary>
    public int MinValue { get; }

    /// <summary>
    ///     The effective maximum value of the stat, which is the difference between MaxValue and MinValue.
    /// </summary>
    public int EffectiveMaxValue => MaxValue - MinValue;

    /// <summary>
    ///     The effective value of the stat, which is the difference between Value and MinValue.
    /// </summary>
    public int EffectiveValue => Value - MinValue;

    /// <summary>
    ///     The effective percentage of the stat, which is the EffectiveValue divided by EffectiveMaxValue.
    /// </summary>
    public float EffectivePercent => (float)EffectiveValue / EffectiveMaxValue;

    /// <summary>
    ///     The percentage of the stat, which is the Value divided by MaxValue.
    /// </summary>
    public float Percent => EffectivePercent;

    public StatVariation Variation { get; }


    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public void OnValueChanged(int oldValue, int newValue)
    {
        var args = new ValueChangedEventArgs(oldValue, newValue);
        LogStatChanged(args.Delta, Name, args.Delta > 0 ? "+" : "");
        ValueChanged?.Invoke(this, args);
    }

    /// <summary>
    ///     Add a single point to the stat's value.
    /// </summary>
    public void AddPoint()
    {
        AddPoints(1);
    }

    /// <summary>
    ///     Add some amount of points to the stat's value.
    /// </summary>
    /// <param name="points"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual void AddPoints(int points)
    {
        if (points < 0) throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");

        Value += points;
    }

    /// <summary>
    ///     Subtract a single point from the stat's value.
    /// </summary>
    public void SubtractPoint()
    {
        SubtractPoints(1);
    }

    /// <summary>
    ///     Subtract some amount of points from the stat's value.
    /// </summary>
    /// <param name="points"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual void SubtractPoints(int points)
    {
        if (points < 0) throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");

        Value -= points;
    }

    /// <summary>
    ///     Force the stat to a specific value, regardless of its current value.
    /// </summary>
    /// <param name="value"></param>
    public void ForceValue(int value)
    {
        Value = value;
    }
}

/// <summary>
///     Represents detail about what kind of stat this is.
/// </summary>
public enum StatVariation
{
    /// <summary>
    ///     Major stats are the primary stat which likely influence many other things. Players should have some agency
    ///     over these stats.
    /// </summary>
    Major,

    /// <summary>
    ///     Minor stats derive value from one or more major stats
    /// </summary>
    Minor,

    /// <summary>
    ///     Represents a fake stat or a stat which is completely contrived
    /// </summary>
    Pseudo,

    /// <summary>
    ///     Represents a stat which is a complex calculation or derived from multiple other stats.
    /// </summary>
    Complex
}