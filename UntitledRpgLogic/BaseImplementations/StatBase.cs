using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Extensions;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Abstract base class for RPG stats.
/// </summary>
public abstract class StatBase : IStat, IHasChangeableValue
{
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
    ///     The apparent value of the stat, which is the value after all modifiers have been applied.
    /// </summary>
    private int _apparentValue;

    /// <summary>
    ///     A protected base value for the stat, which is the raw value before any modifiers are applied.
    /// </summary>
    private int _baseValue;

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
        MaxValue = options.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;
        MinValue = options.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;

        // Initialize the logging behavior with the provided logger or a null logger
        _logging = new LoggingBehavior(options.Logger ?? NullLogger<StatBase>.Instance);

        LogEvent(EventIds.STAT_CREATED, this);

        // Register the ValueChanged event to log changes in stat value
        ValueChanged += (oldValue, newValue) => { LogEvent(EventIds.STAT_VALUE_CHANGED, Name, oldValue, newValue); };
    }


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
    ///     The name of the stat.
    /// </summary>
    public string Name => _nameBehavior.Name;

    /// <inheritdoc />
    public int Value
    {
        get => _apparentValue;
        private set
        {
            if (_apparentValue == value) return;
            var valueChange = value - _apparentValue;
            var oldValue = _apparentValue;


            // Ensure the apparent value is within the defined min and max range
            _apparentValue = value < DefaultValues.STAT_DEFAULT_MIN_VALUE
                ? DefaultValues.STAT_DEFAULT_MIN_VALUE
                : value;
            _apparentValue = value > DefaultValues.STAT_DEFAULT_MAX_VALUE
                ? DefaultValues.STAT_DEFAULT_MAX_VALUE
                : _apparentValue;
            // Adjust the base value by the change in apparent value
            _baseValue += valueChange;

            BaseValueChanged?.Invoke();

            // Invoke the ValueChanged event with the old and new values
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, _apparentValue));
        }
    }

    /// <inheritdoc />
    public void ApplyModifier(IModifier modifier)
    {
        _apparentValue = modifier.ApplyModification(_baseValue, _apparentValue);
    }

    /// <inheritdoc />
    public event Action? BaseValueChanged;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

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