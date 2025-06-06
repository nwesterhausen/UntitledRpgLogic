using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Extensions;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Abstract base class for RPG stats.
/// </summary>
public abstract class StatBase : IStat
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
    ///     Adds a GUID to the stat, which is used for unique identification.
    /// </summary>
    private readonly GuidBehavior _guidBehavior;

    /// <summary>
    ///     Adds logging for the skill.
    /// </summary>
    private readonly LoggingBehavior _logging;

    /// <summary>
    ///     Adds a name to the stat.
    /// </summary>
    private readonly IHasName _nameBehavior;

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
        _nameBehavior = new NameBehavior(options.Name);
        _guidBehavior = new GuidBehavior();

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
    ///     Default maximum value for a stat.
    /// </summary>
    public int DefaultMaxValue => STAT_DEFAULT_MAX_VALUE;

    /// <summary>
    ///     Default minimum value for a stat.
    /// </summary>
    public int DefaultMinValue => STAT_DEFAULT_MIN_VALUE;

    /// <summary>
    ///     Whether this stat is a major, minor, pseudo, or complex stat.
    /// </summary>
    public StatVariation Variation { get; init; }

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

    /// <summary>
    ///     The name of the stat.
    /// </summary>
    public string Name => _nameBehavior.Name;

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

    /// <inheritdoc />
    public Guid Guid => _guidBehavior.Guid;

    /// <inheritdoc />
    public string Id => _guidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => _guidBehavior.ShortGuid;

    /// <inheritdoc />
    public ILogger Logger => _logging.Logger;

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        _logging.LogEvent(eventId, args);
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        _logging.LogError(exception, eventId);
    }


    /// <inheritdoc />
    public override string ToString()
    {
        return this.IntoString();
    }
}