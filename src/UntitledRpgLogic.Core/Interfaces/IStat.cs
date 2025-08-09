using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for a Stat in the RPG logic.
/// </summary>
public interface IStat : IHasName, IHasMutableValue, IHasIdentifier
{
	/// <summary>
	///     The reference to the StatDefinition that this stat is based on. This provides the properties and behavior of the stat.
	///     It is used to determine the stat's type, maximum value, minimum value, and other characteristics.
	/// </summary>
	public StatDefinition StatDefinition { get; }

	/// <summary>
	///     The base value of the stat, which is different than its current (effective) value.
	/// </summary>
	public int BaseValue { get; set; }

	#region UI Subscription Events

	/// <summary>
	///     Event raised when the base value of the stat changes. Should trigger recalculation of the apparent value.
	/// </summary>
	public event EventHandler<ValueChangedEventArgs>? OnBaseValueChanged;

	/// <summary>
	///     A method for the owning service to invoke the BaseValueChanged event.
	/// </summary>
	public void InvokeBaseValueChanged();

	// note: inherited ValueChanged event handler from IHasValue

	#endregion

	#region Default Implementations

	/// <summary>
	///     The minimum value of the stat, which is defined in the StatDefinition.
	/// </summary>
	public int MaxValue => this.StatDefinition.MaxValue;

	/// <summary>
	///     The maximum value of the stat, which is defined in the StatDefinition.
	/// </summary>
	public int MinValue => this.StatDefinition.MinValue;

	/// <summary>
	///     The effective maximum value of the stat, which is the difference between MaxValue and MinValue.
	/// </summary>
	public int EffectiveMaxValue => this.StatDefinition.MaxValue - this.StatDefinition.MinValue;

	/// <summary>
	///     The effective value of the stat, which is the difference between Value and MinValue.
	/// </summary>
	public int EffectiveValue => this.Value - this.StatDefinition.MinValue;

	/// <summary>
	///     The effective percentage of the stat, which is the EffectiveValue divided by EffectiveMaxValue.
	/// </summary>
	public float EffectivePercent => this.EffectiveMaxValue > 0 ? (float)this.EffectiveValue / this.EffectiveMaxValue : 0f;

	/// <summary>
	///     The percentage of the stat, which is the Value divided by MaxValue.
	/// </summary>
	public float Percent => this.EffectivePercent;

	/// <summary>
	///     The name of the stat, which is derived from the StatDefinition. This is used for display purposes in the UI.
	/// </summary>
	public new string Name => this.StatDefinition.Name;

	/// <summary>
	///     The type or variation of stat, can indicate if it's major, minor, or a special type of stat.
	/// </summary>
	public StatVariation Variation => this.StatDefinition.Variation;

	/// <summary>
	///     A list of linked stats and their multipliers. This is used to determine how this stat interacts with other stats in the game.
	/// </summary>
	public ICollection<LinkedStats> LinkedStats => this.StatDefinition.LinkedStats;

	#endregion
}
