using Microsoft.Extensions.Logging;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Game;

/// <summary>
///     Represents a base stat in the RPG logic, implementing the IStat interface.
/// </summary>
public class BaseStat : IStat
{
    /// <summary>
    ///     The current apparent value of the stat, which may be modified by buffs, debuffs, or other effects.
    /// </summary>
    private int _apparentValue;

    /// <summary>
    ///     The base value of the stat, which is the underlying value before any modifications.
    /// </summary>
    private int _baseValue;

    /// <summary>
    ///     Creates a new BaseStat instance with the provided configuration and optional logger.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    public BaseStat(StatDataConfig config, ILogger? logger = null)
    {
        GuidBehavior = new GuidBehavior(config.ExplicitId);
        NameBehavior = new NameBehavior(config.Name);
        LoggingBehavior = new LoggingBehavior(logger);
        MaxValue = config.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;
        MinValue = config.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;
        _apparentValue = MinValue;
        _baseValue = MinValue;

        // Logging event handlers
        ValueChanged += (_, args) => { LogEvent(EventIds.STAT_VALUE_CHANGED, args.Delta, Name, args.Modifier); };
    }

    /// <summary>
    /// </summary>
    private NameBehavior NameBehavior { get; }

    /// <summary>
    /// </summary>
    private GuidBehavior GuidBehavior { get; }

    /// <summary>
    /// </summary>
    private LoggingBehavior LoggingBehavior { get; }

    /// <inheritdoc />
    public string Name => NameBehavior.Name;

    /// <inheritdoc />
    public string PluralName => NameBehavior.PluralName;

    /// <inheritdoc />
    public string NameAsAdjective => NameBehavior.NameAsAdjective;

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

    /// <summary>
    ///     Event triggered when the applied value of the stat changes.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public Guid Guid => GuidBehavior.Guid;

    /// <inheritdoc />
    public string Id => GuidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => GuidBehavior.ShortGuid;

    /// <inheritdoc />
    public ILogger Logger => LoggingBehavior.Logger;

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        LoggingBehavior.LogEvent(eventId, args);
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        LoggingBehavior.LogError(exception, eventId);
    }

    /// <inheritdoc />
    public StatVariation Variation { get; init; }

    /// <inheritdoc />
    public int MaxValue { get; init; }

    /// <inheritdoc />
    public int MinValue { get; init; }

    /// <inheritdoc />
    public void ApplyModifier(IModifier modifier)
    {
        _apparentValue = modifier.ApplyModification(_baseValue, _apparentValue, MaxValue);
    }

    /// <inheritdoc />
    public event Action? BaseValueChanged;

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
}