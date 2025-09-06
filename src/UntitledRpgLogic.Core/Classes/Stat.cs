using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container representing a stat. All business logic is handled by a service.
///     This class includes property validation to ensure its internal state remains valid.
/// </summary>
public class Stat : IStat
{
	private int baseValue;
	private int maxValue;

	private int minValue;

	// Private backing fields for properties that require validation logic.
	private int value;

	/// <summary>
	///     Initializes a new instance of the <see cref="Stat" /> class from a configuration object.
	/// </summary>
	/// <param name="config">The configuration data used to define the stat's properties.</param>
	/// <param name="instanceId">The unique identifier of the entity instance this stat belongs to, if any.</param>
	public Stat(StatDataConfig config, Ulid? instanceId = null)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));
		// Initialize IHasGuid properties
		this.Id = config.Id;

		// Initialize other properties from config
		this.Name = Name.Deserialize(config.Name);
		this.InstanceId = instanceId ?? Ulid.NewUlid();
		this.Variation = config.Variation ?? StatVariation.Pseudo;

		// Set backing fields for min/max first to establish the valid range.
		this.minValue = config.MinValue ?? DefaultValues.StatDefaultMinValue;
		// The MaxValue property setter contains validation logic against _minValue, so we call it here.
		this.MaxValue = config.MaxValue ?? DefaultValues.StatDefaultMaxValue;

		// Initialize state using the properties to enforce clamping against the newly set min/max values.
		this.BaseValue = this.minValue;
		this.Value = this.minValue;
	}

	/// <inheritdoc />
	public Ulid InstanceId { get; init; }

	/// <inheritdoc />
	public Ulid Id { get; }

	// IHasName
	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc cref="IHasMutableValue" />
	public int Value
	{
		get => this.value;
		set => this.value = Math.Clamp(value, this.minValue, this.maxValue);
	}

	/// <inheritdoc />
	public int BaseValue
	{
		get => this.baseValue;
		set => this.baseValue = Math.Clamp(value, this.minValue, this.maxValue);
	}

	/// <inheritdoc />
	public int MaxValue
	{
		get => this.maxValue;
		set
		{
			if (value < this.minValue)
			{
				throw new ArgumentOutOfRangeException(nameof(this.MaxValue), "MaxValue cannot be less than MinValue.");
			}

			this.maxValue = value;
			// Re-clamp current values to ensure they remain valid within the new bounds.
			this.Value = this.value;
			this.BaseValue = this.baseValue;
		}
	}

	/// <inheritdoc />
	public int MinValue
	{
		get => this.minValue;
		set
		{
			if (value > this.maxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(this.MinValue), "MinValue cannot be greater than MaxValue.");
			}

			this.minValue = value;
			// Re-clamp current values to ensure they remain valid within the new bounds.
			this.Value = this.value;
			this.BaseValue = this.baseValue;
		}
	}

	/// <inheritdoc />
	public StatVariation Variation { get; init; }

	/// <inheritdoc />
	public Dictionary<Ulid, float> LinkedStats { get; } = [];

	/// <inheritdoc />
	public event EventHandler<ValueChangedEventArgs>? ValueChanged;

	/// <inheritdoc />
	public event EventHandler<ValueChangedEventArgs>? BaseValueChanged;

	/// <inheritdoc />
	public void InvokeValueChanged(ValueChangedEventArgs args) => this.ValueChanged?.Invoke(this, args);

	/// <inheritdoc />
	public void InvokeBaseValueChanged(ValueChangedEventArgs args) => this.BaseValueChanged?.Invoke(this, args);
}
