using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container representing a stat. All logic is handled by a service.
///     This class includes guardrails to ensure its internal state remains valid.
/// </summary>
public class Stat : IStat
{
    private int _baseValue;
    private int _maxValue;

    private int _minValue;

    // Private backing fields for properties that require validation logic.
    private int _value;

    /// <summary>
    /// </summary>
    /// <param name="config"></param>
    /// <param name="instanceId"></param>
    public Stat(StatDataConfig config, Guid? instanceId = null)
    {
        // Directly initialize properties from config
        Guid = config.ExplicitId ?? Guid.NewGuid();
        // Cache IHasGuid fields
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();

        Name = Name.Deserialize(config.Name);
        InstanceId = instanceId ?? Guid.Empty;

        // Set backing fields first, then use properties to ensure validation logic is applied.
        _minValue = config.MinValue ?? DefaultValues.STAT_DEFAULT_MIN_VALUE;
        MaxValue = config.MaxValue ?? DefaultValues.STAT_DEFAULT_MAX_VALUE;

        // Initialize state using the properties to enforce clamping.
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

    /// <inheritdoc />
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
    public Dictionary<Guid, float> LinkedStats { get; } = new();

    /// <inheritdoc />
    public Guid InstanceId { get; init; }

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public event Action? BaseValueChanged;

    /// <inheritdoc />
    public void ApplyModifier(IModifier modifier)
    {
        // The service calculates the new value and sets it via the Value property,
        // which automatically clamps it.
        Value = modifier.ApplyModification(BaseValue, Value, MaxValue);
    }

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

    /// <inheritdoc />
    public void AddPoints(int points)
    {
        BaseValue += points;
    }


    /// <inheritdoc />
    public void RemovePoints(int points)
    {
        BaseValue -= points;
    }

    /// <inheritdoc />
    public void SetPoints(int points)
    {
        BaseValue = points;
    }
}
