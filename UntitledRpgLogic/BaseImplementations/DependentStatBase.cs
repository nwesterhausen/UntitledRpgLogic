using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Classes;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     A stat that depends on other stats or conditions to determine its value. Its value cannot be set directly, but is calculated based on the values of linked stats.
/// </summary>
public abstract class DependentStatBase : IDependentStat
{
    /// <summary>
    ///     Adds a GUID to the stat, which is used for unique identification.
    /// </summary>
    private readonly GuidBehavior _guidBehavior;

    /// <summary>
    ///     List of linked stats that this stat depends on. These are used to calculate the apparent value of the stat.
    /// </summary>
    private readonly List<LinkedStat> _linkedStats = [];

    /// <summary>
    ///     Adds logging for the skill.
    /// </summary>
    private readonly LoggingBehavior _logging;

    /// <summary>
    ///     Adds a name to the stat.
    /// </summary>
    private readonly IHasName _nameBehavior;

    /// <summary>
    ///     A protected field for the apparent value of the stat, which is the value after all modifiers have been applied.
    /// </summary>
    private int _apparentValue;

    /// <summary>
    ///     A protected base value for the stat, which is the raw value before any modifiers are applied.
    /// </summary>
    private int _baseValue;

    /// <summary>
    ///     Create a new instance of a dependent stat with the specified options.
    /// </summary>
    /// <param name="options"></param>
    protected DependentStatBase(StatOptions options)
    {
        _nameBehavior = new NameBehavior(options.Name);
        _guidBehavior = new GuidBehavior(options.KnownGuid);
        _logging = new LoggingBehavior(options.Logger);

        // Set the variation, defaulting to Minor if not specified
        Variation = options.Variation ?? StatVariation.Minor;

        // Set the maximum and minimum values, defaulting to predefined constants if not specified
        MaxValue = options.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;
        MinValue = options.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;
    }

    /// <inheritdoc />
    public string Name => _nameBehavior.Name;

    /// <inheritdoc />
    public int Value
    {
        get => _apparentValue;
        private set
        {
            if (_apparentValue == value) return;
            var oldValue = _apparentValue;
            // Ensure the apparent value is within the defined min and max range
            _apparentValue = value < DefaultValues.STAT_DEFAULT_MIN_VALUE
                ? DefaultValues.STAT_DEFAULT_MIN_VALUE
                : value;
            _apparentValue = value > DefaultValues.STAT_DEFAULT_MAX_VALUE
                ? DefaultValues.STAT_DEFAULT_MAX_VALUE
                : _apparentValue;

            ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, _apparentValue));
        }
    }

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
    public void ApplyModifier(IModifier modifier)
    {
        _apparentValue = modifier.ApplyModification(_baseValue, _apparentValue);
    }

    /// <inheritdoc />
    public event Action? BaseValueChanged;

    /// <inheritdoc />
    public StatVariation Variation { get; }

    /// <inheritdoc />
    public int MaxValue { get; }

    /// <inheritdoc />
    public int MinValue { get; }

    /// <inheritdoc />
    public void LinkTo(IStat stat, LinkedStat.ApplyValue valueCalculation, int priority = 0)
    {
        var linkedStat = new LinkedStat
        {
            StatId = stat.Guid,
            Priority = priority,
            StatValue = stat.Value,
            ValueCalculation = valueCalculation
        };
        if (_linkedStats.All(ls => ls.StatId != linkedStat.StatId))
        {
            _linkedStats.Add(linkedStat);
            stat.ValueChanged += HandleLinkedStatValueChanged;
            LogEvent(EventIds.STAT_LINKED, Name, linkedStat.StatId, linkedStat.Priority);
            return;
        }

        var existsException = new InvalidOperationException(
            $"Stat {Name} is already linked to stat {stat.Name} with ID {stat.Guid}. " +
            "Stat must be removed before it can be linked again.");
#if DEBUG
               throw existsException;
#endif
        // Update what we can the existing linked stat if it already exists;
        LogError(existsException, EventIds.STAT_LINK_EXISTS);
    }

    /// <inheritdoc />
    public void Unlink(IStat stat)
    {
        var linkedStat = _linkedStats.FirstOrDefault(ls => ls.StatId == stat.Guid);
        if (linkedStat == null)
        {
            var notFoundException = new InvalidOperationException(
                $"Linked stat with ID {stat.Guid} not found for {Name}. " +
                "Ensure the stat is linked before trying to unlink it.");
#if DEBUG
            throw notFoundException;
#endif
            LogError(notFoundException, EventIds.STAT_LINK_NOT_FOUND);
        }
        else
        {
            _linkedStats.Remove(linkedStat);
        }

        // Remove the event handler for the stat's value changed event (if it exists)
        stat.ValueChanged -= HandleLinkedStatValueChanged;
        LogEvent(EventIds.STAT_UNLINKED, stat.Name, Name);
    }

    /// <summary>
    ///     Handles the value changed event for linked stats. This method is called when a linked stat's value changes,
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleLinkedStatValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (sender is not IStat stat) return;

        // Find the linked stat that matches the sender's ID
        var linkedStat = _linkedStats.FirstOrDefault(ls => ls.StatId == stat.Guid);
        if (linkedStat == null)
        {
            var notFoundException = new InvalidOperationException(
                $"Linked stat with ID {stat.Guid} not found for {Name}. " +
                "Ensure the stat is linked before trying to update its value.");
#if DEBUG
               throw notFoundException;
#endif
            LogError(notFoundException, EventIds.STAT_LINK_NOT_FOUND);
            stat.ValueChanged -= HandleLinkedStatValueChanged;
            LogEvent(EventIds.STAT_UNLINKED, stat.Name, Name);
            return;
        }

        linkedStat.StatValue = stat.Value;
        RecalculateValue();
    }

    /// <summary>
    ///     Recalculates the base value of the stat based on the linked stats.
    /// </summary>
    private void RecalculateValue()
    {
        // Recalculate the apparent value based on the linked stats
        _baseValue = _linkedStats
            .OrderBy(ls => ls.Priority)
            .Aggregate(0, (current, ls) => current + ls.ValueCalculation(_baseValue));
        // Re-apply any modifiers that might affect the apparent value
        BaseValueChanged?.Invoke();
    }
}