using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container representing a stat. All business logic is handled by a service.
///     This class includes property validation to ensure its internal state remains valid.
/// </summary>
public class Stat : IStat
{
    private int _baseValue;
    private int _maxValue;

    private int _minValue;

    // Private backing fields for properties that require validation logic.
    private int _value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Stat" /> class from a configuration object.
    /// </summary>
    /// <param name="config">The configuration data used to define the stat's properties.</param>
    /// <param name="instanceId">The unique identifier of the entity instance this stat belongs to, if any.</param>
    public Stat(StatDataConfig config, Guid? instanceId = null)
    {
        // Initialize IHasGuid properties
        Guid = config.ExplicitId ?? Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();

        // Initialize other properties from config
        Name = Name.Deserialize(config.Name);
        InstanceId = instanceId ?? Guid.Empty;
        Variation = config.Variation ?? StatVariation.Pseudo;

        // Set backing fields for min/max first to establish the valid range.
        _minValue = config.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;
        // The MaxValue property setter contains validation logic against _minValue, so we call it here.
        MaxValue = config.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;

        // Initialize state using the properties to enforce clamping against the newly set min/max values.
        BaseValue = _minValue;
        Value = _minValue;
    }

    // IHasGuid
    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    // IHasName
    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc cref="IHasMutableValue" />
    public int Value
    {
        get => _value;
        set => _value = Math.Clamp(value, _minValue, _maxValue);
    }

    /// <inheritdoc />
    public int BaseValue
    {
        get => _baseValue;
        set => _baseValue = Math.Clamp(value, _minValue, _maxValue);
    }

    /// <inheritdoc />
    public int MaxValue
    {
        get => _maxValue;
        set
        {
            if (value < _minValue)
                throw new ArgumentOutOfRangeException(nameof(MaxValue), "MaxValue cannot be less than MinValue.");

            _maxValue = value;
            // Re-clamp current values to ensure they remain valid within the new bounds.
            Value = _value;
            BaseValue = _baseValue;
        }
    }

    /// <inheritdoc />
    public int MinValue
    {
        get => _minValue;
        set
        {
            if (value > _maxValue)
                throw new ArgumentOutOfRangeException(nameof(MinValue), "MinValue cannot be greater than MaxValue.");

            _minValue = value;
            // Re-clamp current values to ensure they remain valid within the new bounds.
            Value = _value;
            BaseValue = _baseValue;
        }
    }

    /// <inheritdoc />
    public StatVariation Variation { get; init; }

    /// <inheritdoc />
    public Dictionary<Guid, float> LinkedStats { get; } = [];

    /// <inheritdoc />
    public Guid InstanceId { get; init; }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public event Action? BaseValueChanged;

    /// <inheritdoc />
    public void InvokeValueChanged(ValueChangedEventArgs args)
    {
        ValueChanged?.Invoke(this, args);
    }

    /// <inheritdoc />
    public void InvokeBaseValueChanged()
    {
        BaseValueChanged?.Invoke();
    }
}
