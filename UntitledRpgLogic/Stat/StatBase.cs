namespace UntitledRpgLogic.Stat;

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

/// <summary>
///   Abstract base class for RPG stats.
/// </summary>
public abstract partial class StatBase : IComparable<int>, IComparable<StatBase>
{
    public delegate void StatChanged(object sender, int newValue);

    private const int STAT_DEFAULT_MAX_VALUE = 1024;
    private const int STAT_DEFAULT_MIN_VALUE = 0;

    private int _value;

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
        MinValue = minValue;
        LogStatCreated(this);
    }

    /// <summary>
    ///   The name of the stat.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///   The value of the stat.
    /// </summary>
    public int Value
    {
        get => _value;
        internal set
        {
            if (value == _value)
            {
                return;
            }

            var oldValue = _value;
            _value = value;
            if (_value > MaxValue)
            {
                _value = MaxValue;
            }

            if (_value < MinValue)
            {
                _value = MinValue;
            }

            OnValueChanged(oldValue, _value);
        }
    }

    /// <summary>
    ///   The maximum value of the stat.
    /// </summary>
    public int MaxValue { get; }

    /// <summary>
    ///   The minimum value of the stat.
    /// </summary>
    public int MinValue { get; }

    public int EffectiveMaxValue => MaxValue - MinValue;
    public int EffectiveValue => Value - MinValue;
    public float EffectivePercent => (float)EffectiveValue / EffectiveMaxValue;
    public float Percent => EffectivePercent;

    /// <summary>
    ///   Event that is triggered when the value of the stat changes.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;


    private void OnValueChanged(int oldValue, int newValue)
    {
        var args = new ValueChangedEventArgs(oldValue, newValue);
        LogStatChanged(args.Delta, Name, args.Delta > 0 ? "+" : "");
        ValueChanged?.Invoke(this, args);
    }

    public void AddPoint() => AddPoints(1);

    public virtual void AddPoints(int points)
    {
        if (points < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");
        }

        Value += points;
    }

    public void SubtractPoint() => SubtractPoints(1);

    public virtual void SubtractPoints(int points)
    {
        if (points < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");
        }

        Value -= points;
    }

    public class ValueChangedEventArgs(int oldValue, int newValue) : EventArgs
    {
        public int NewValue { get; } = newValue;
        public int OldValue { get; } = oldValue;
        public int Delta => NewValue - OldValue;
    }

    public StatVariation Variation { get; }

    public void ForceValue(int value)
    {
        Value = value;
    }
}

public enum StatVariation
{
    Major,
    Minor,
    Pseudo,
    Complex,
}