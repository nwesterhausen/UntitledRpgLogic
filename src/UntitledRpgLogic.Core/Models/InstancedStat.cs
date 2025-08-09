using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;
// Required for [NotMapped]

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents a specific instance of a stat for an entity.
///     It holds the dynamic data that can change during gameplay, while referencing its immutable definition.
/// </summary>
public sealed class InstancedStat : IStat, IInstance
{
	// ReSharper disable once InconsistentNaming
	private int _baseValue;

	// ReSharper disable once InconsistentNaming
	private Name _name = Name.Empty;

	// ReSharper disable once InconsistentNaming
	private int _value;

	/// <summary>
	///     This constructor is intended for use by Entity Framework Core.
	/// </summary>
	private InstancedStat()
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedStat" /> class for game logic.
	/// </summary>
	/// <param name="statDefinition">stat definition to base this instance on.</param>
	public InstancedStat(StatDefinition statDefinition) : this()
	{
		ArgumentNullException.ThrowIfNull(statDefinition, nameof(statDefinition));

		this.Identifier = Guid.NewGuid();
		this.StatDefinition = statDefinition;
		this.StatDefinitionId = statDefinition.Id; // Set the foreign key
		this.Value = statDefinition.BaseValue;
		this._name = new Name(statDefinition.Name);
	}

	// Foreign Key to the StatDefinition table
	/// <summary>
	///     The ID of the stat's definition.
	///     This is used to link this instance to its immutable definition.
	/// </summary>
	public Guid StatDefinitionId { get; private set; }

	/// <inheritdoc />
	public Guid InstanceId => this.Identifier;

	/// <inheritdoc />
	public Guid Identifier { get; init; }

	/// <inheritdoc />
	public event EventHandler<ValueChangedEventArgs>? OnBaseValueChanged;

	/// <inheritdoc />
	public void InvokeBaseValueChanged() =>
		this.OnBaseValueChanged?.Invoke(this, new ValueChangedEventArgs(this.StatDefinition.BaseValue, this.Value));

	/// <summary>
	///     Gets the immutable definition for this stat.
	///     EF Core will use the StatDefinitionId to "lazy load" this property.
	/// </summary>
	public StatDefinition StatDefinition { get; } = null!; // Changed to private set, virtual for lazy loading

	/// <inheritdoc cref="IHasMutableValue" />
	public int Value
	{
		get => this._value;
		set
		{
			var clampedValue = Math.Clamp(value, this.StatDefinition.MinValue, this.StatDefinition.MaxValue);
			if (this._value == clampedValue)
			{
				return;
			}

			var oldValue = this._value;
			this._value = clampedValue;
			this.OnValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, this._value));
		}
	}

	/// <inheritdoc />
	[field: NotMapped] // Tell EF Core to ignore this property
	public event EventHandler<ValueChangedEventArgs>? OnValueChanged;

	/// <inheritdoc />
	public void InvokeValueChanged(ValueChangedEventArgs args) =>
		this.OnValueChanged?.Invoke(this, args);

	/// <inheritdoc />
	[field: NotMapped] // Tell EF Core to ignore this property
	public Name Name
	{
		get
		{
			if (this._name != Name.Empty)
			{
				return this._name;
			}

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (this.StatDefinition != null)
			{
				this._name = new Name(this.StatDefinition.Name);
			}

			return this._name;
		}
	}

	/// <inheritdoc />
	public int BaseValue
	{
		get => this._baseValue;
		set
		{
			var clampedValue = Math.Clamp(value, this.StatDefinition.MinValue, this.StatDefinition.MaxValue);
			if (this._baseValue == clampedValue)
			{
				return;
			}

			var oldValue = this._baseValue;
			this._baseValue = clampedValue;
			this.OnBaseValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, this._value));
		}
	}
}
