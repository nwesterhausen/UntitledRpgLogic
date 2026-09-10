namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Provides data for events signaling a change in a generic value.
/// </summary>
/// <typeparam name="T">The type of value being monitored.</typeparam>
public class ValueChangedEventArgs<T> : EventArgs
{
	/// <summary>
	/// 	A value changed event where only the new/current value is specified.
	/// </summary>
	/// <param name="value">The new/current value</param>
	public ValueChangedEventArgs(T value) => this.Value = value;

	/// <summary>
	/// 	A value changed event where old and new values are specified.
	/// </summary>
	/// <param name="newValue">The new/current value</param>
	/// <param name="oldValue">The previous value</param>
	public ValueChangedEventArgs(T oldValue, T newValue)
	{
		this.OldValue = oldValue;
		this.Value = newValue;
	}

	/// <summary>
	/// 	the previous value
	/// </summary>
	public T? OldValue { get; }

	/// <summary>
	/// 	the new/current value
	/// </summary>
	public T Value { get; }
}
