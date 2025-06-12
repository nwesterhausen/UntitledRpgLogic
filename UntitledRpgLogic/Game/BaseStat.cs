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
    ///     Creates a new BaseStat instance with the provided configuration and optional logger.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    public BaseStat(StatDataConfig config, ILogger? logger = null, Guid? instanceId = null)
    {
        GuidBehavior = new GuidBehavior(config.ExplicitId);
        NameBehavior = new NameBehavior(config.Name);
        LoggingBehavior = new LoggingBehavior(logger);
        MaxValue = config.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;
        MinValue = config.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;
        LinkedStats = new Dictionary<Guid, float>();
        InstanceId = instanceId ?? Guid.Empty;
        _apparentValue = MinValue;
        BaseValue = MinValue;

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
            if (Variation == StatVariation.Complex || Variation == StatVariation.Minor)
            {
                // These stats are not meant to be directly set, so we do not let it get changed here.
                var exception =
                    new InvalidOperationException("Cannot directly set the value of a complex or minor stat.");
                LogErrorEvent(exception, EventIds.STAT_ILLEGAL_CHANGE, Name,
                    "Setting value directly is not allowed for this stat type.");
                return;
            }

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
            BaseValue += valueChange;

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
    public void LogErrorEvent(Exception? exception, EventId eventId, params object?[] args)
    {
        LoggingBehavior.LogErrorEvent(exception, eventId, args);
    }

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        LogErrorEvent(null, eventId, args);
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
    public int BaseValue { get; private set; }

    /// <inheritdoc />
    public void ApplyModifier(IModifier modifier)
    {
        _apparentValue = modifier.ApplyModification(BaseValue, _apparentValue, MaxValue);
    }

    /// <inheritdoc />
    public event Action? BaseValueChanged;

    /// <inheritdoc />
    public Dictionary<Guid, float> LinkedStats { get; }

    /// <inheritdoc />
    public void LinkStat(IStat stat, float ratio = 1)
    {
        // Major stats are independent and do not depend on other stats.
        if (Variation == StatVariation.Major)
        {
            Logger.LogWarning("Cannot link a stat to a major stat. Stat {StatName} is a major stat.", Name);
            return;
        }

        // Ensure the stat is not already linked
        if (LinkedStats.Any(s => s.Key == stat.Guid))
        {
            Logger.LogWarning("Stat {StatName} is already linked to {LinkedStatName}.", Name, stat.Name);
            return;
        }

        stat.ValueChanged += UpdateValueFromDependentStatChange;
        LinkedStats.Add(stat.Guid, ratio);
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
    public Guid InstanceId { get; init; }

    /// <summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateValueFromDependentStatChange(object? sender, ValueChangedEventArgs e)
    {
        // short-circuit if this is a major stat
        if (Variation == StatVariation.Major) return;

        // Modify the apparent value based on the change.
        if (sender is not IStat stat)
        {
            LogError(new ArgumentException("Sender is not a valid stat."), EventIds.STAT_INVALID_SENDER);
            return;
        }

        if (!LinkedStats.TryGetValue(stat.Guid, out var ratio))
        {
            Logger.LogWarning("Stat {StatName} is not linked to {LinkedStatName}.", Name, stat.Name);
            return;
        }

        // Calculate the change in value
        var change = (int)(e.Delta * ratio);
        // Update the apparent value based on the change
        _apparentValue += change;
    }
}