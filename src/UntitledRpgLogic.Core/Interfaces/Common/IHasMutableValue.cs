using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Defines the contract for an object whose core integer value can be changed by a service.
///     Inherits from IHasValue.
/// </summary>
public interface IHasMutableValue : IHasValue
{
	/// <summary>
	///     The current integer value of the object, which can be read or written to.
	///     This hides the read-only 'Value' property from the base IHasValue interface.
	/// </summary>
	public new int Value { get; set; }

	/// <summary>
	///     Event that is triggered when the value changes.
	/// </summary>
	public event EventHandler<ValueChangedEventArgs>? ValueChanged;

	/// <summary>
	///     A method for the owning service to invoke the ValueChanged event.
	/// </summary>
	public void InvokeValueChanged(ValueChangedEventArgs args);
}
