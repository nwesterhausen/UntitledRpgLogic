using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for a Stat in the RPG logic.
/// </summary>
public interface IStat : IHasName, IHasMutableValue, IHasIdentifier, IInstantiable
{
	#region UI Subscription Events

	/// <summary>
	///     Event raised when the base value of the stat changes. Should trigger recalculation of the apparent value.
	/// </summary>
	public event EventHandler<ValueChangedEventArgs>? BaseValueChanged;

	/// <summary>
	///     A method for the owning service to invoke the BaseValueChanged event.
	/// </summary>
	public void InvokeBaseValueChanged(ValueChangedEventArgs args);

	// note: inherited ValueChanged event handler from IHasValue

	#endregion

	#region Properties managed by IStatService

	/// <summary>
	///     The variation of the stat, which helps qualify how to display the stat in the UI or how it behaves in the game
	///     logic.
	/// </summary>
	public StatVariation Variation { get; }

	/// <summary>
	///     The maximum value the stat can reach. This is used to limit the stat's value and prevent it from exceeding a
	///     certain threshold.
	/// </summary>
	public int MaxValue { get; }

	/// <summary>
	///     The minimum value the stat can have. Only useful if the stat should be starting at a value above zero that it
	///     cannot drop below.
	/// </summary>
	public int MinValue { get; }

	/// <summary>
	///     The current base value of the stat, which is the underlying value before any modifications.
	/// </summary>
	public int BaseValue { get; set; }

	/// <summary>
	///     Get the stats that are linked to this stat. This is used to retrieve all the stats that this stat depends on.
	/// </summary>
	public Dictionary<Ulid, float> LinkedStats { get; }

	#endregion

	#region Default Implementations

	/// <summary>
	///     The effective maximum value of the stat, which is the difference between MaxValue and MinValue.
	/// </summary>
	public int EffectiveMaxValue => this.MaxValue - this.MinValue;

	/// <summary>
	///     The effective value of the stat, which is the difference between Value and MinValue.
	/// </summary>
	public int EffectiveValue => this.Value - this.MinValue;

	/// <summary>
	///     The effective percentage of the stat, which is the EffectiveValue divided by EffectiveMaxValue.
	/// </summary>
	public float EffectivePercent => this.EffectiveMaxValue > 0 ? (float)this.EffectiveValue / this.EffectiveMaxValue : 0f;

	/// <summary>
	///     The percentage of the stat, which is the Value divided by MaxValue.
	/// </summary>
	public float Percent => this.EffectivePercent;

	#endregion
}
