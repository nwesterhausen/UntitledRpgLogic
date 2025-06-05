using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Abstract base class for RPG stats.
/// </summary>
public abstract class StatBase : IHasValue
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
    ///     Adds logging for the skill.
    /// </summary>
    private readonly LoggingBehavior _logging;

    /// <summary>
    ///     Adds a name to the skill.
    /// </summary>
    private readonly HasMonoNameBase _monoNameBehavior = new MonoNameBehavior();

    /// <summary>
    ///     Internally stores the value of the stat. This is the actual number that represents the stat's current state.
    /// </summary>
    private int _value;

    /// <summary>
    ///     Creates a new instance of <see cref="StatBase" />.
    /// </summary>
    /// <param name="options"></param>
    protected StatBase(StatOptions options)
    {
        if (options.Name != null)
            Name = options.Name;

        // Set the variation, defaulting to Pseudo if not specified
        Variation = options.Variation ?? StatVariation.Pseudo;

        // Set the maximum and minimum values, defaulting to predefined constants if not specified
        MaxValue = options.MaxValue ?? STAT_DEFAULT_MAX_VALUE;
        MinValue = options.MinValue ?? STAT_DEFAULT_MIN_VALUE;

        // Initialize the logging behavior with the provided logger or a null logger
        _logging = new LoggingBehavior(options.Logger ?? NullLogger<StatBase>.Instance);

        _logging.LogEvent(LoggingEventIds.STAT_CREATED, this);

        // Register the ValueChanged event to log changes in stat value
        ValueChanged += (oldValue, newValue) =>
        {
            _logging.LogEvent(LoggingEventIds.STAT_VALUE_CHANGED, Name, oldValue, newValue);
        };
    }

    /// <summary>
    ///     Whether this stat is a major, minor, pseudo, or complex stat.
    /// </summary>
    public StatVariation Variation { get; init; }

    /// <summary>
    ///     The name of the stat.
    /// </summary>
    public string Name
    {
        get => _monoNameBehavior.Name;
        private init => _monoNameBehavior.Name = value;
    }

    /// <summary>
    ///     The minimum value for this stat. This is the lowest value the stat can have.
    /// </summary>
    public int MinValue { get; init; }

    /// <summary>
    ///     The maximum value for this stat. This is the highest value the stat can have.
    /// </summary>
    public int MaxValue { get; init; }


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

    /// <inheritdoc />
    public int Value
    {
        get => _value;
        private set
        {
            if (_value == value) return;
            var oldValue = _value;
            _value = value < STAT_DEFAULT_MIN_VALUE ? STAT_DEFAULT_MIN_VALUE : value;
            _value = value > STAT_DEFAULT_MAX_VALUE ? STAT_DEFAULT_MAX_VALUE : _value;

            ValueChanged?.Invoke(oldValue, _value);
        }
    }

    /// <inheritdoc />
    public void AddPoint()
    {
        Value++;
    }

    /// <inheritdoc />
    public void RemovePoint()
    {
        Value--;
    }

    /// <inheritdoc />
    public void AddPoints(int points)
    {
        Value += points;
    }

    /// <inheritdoc />
    public void RemovePoints(int points)
    {
        Value -= points;
    }

    /// <inheritdoc />
    public void SetPoints(int points)
    {
#if DEBUG
        if (points < 0)
            throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");
#endif
        Value = points;
    }

    /// <inheritdoc />
    public event Action<int, int>? ValueChanged;

    /// <summary>
    ///     Link this stat to another stat with a specific ratio. This is used to create dependencies between stats,
    ///     but only for minor, pseudo, or complex stats. Major stats are designed to be independent and cannot be linked
    ///     to other stats.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="ratio"></param>
    internal void LinkTo(StatBase other, double ratio = 1.0)
    {
        if (Variation == StatVariation.Major)
        {
            // Major stats cannot be linked to other stats.
            var ex = new InvalidOperationException("Major stats cannot be linked to other stats.");
#if DEBUG
            throw ex;
#endif
            _logging.LogError(ex, LoggingEventIds.STAT_LINKED);
            return;
        }

        _logging.LogEvent(LoggingEventIds.STAT_LINKED, Name, other.Name, ratio);
        // Logic to link this stat to another stat with a specific ratio
    }

    /// <summary>
    ///     Explicitly converts a stat to its string representation.
    /// </summary>
    /// <param name="stat">The stat to convert.</param>
    public static explicit operator string(StatBase stat)
    {
        if (stat.MinValue == STAT_DEFAULT_MIN_VALUE)
            return
                $"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} ({stat.Value / (float)stat.MaxValue:F2 * 100}";

        return
            $"{stat.Variation} {stat.Name}: {stat.Value} / {stat.MaxValue} with {stat.MinValue} minimum ({stat.EffectiveValue:F2})%";
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return (string)this;
    }
}