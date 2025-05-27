namespace UntitledRpgLogic;

/// <summary>
///     Interface for classes that notify when their value changes.
/// </summary>
public interface INotifyValueChanged
{
    /// <summary>
    ///     Event that is triggered when the value of the stat changes.
    /// </summary>
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <summary>
    ///     Method to call when the value changes
    /// </summary>
    /// <param name="oldValue">previous value</param>
    /// <param name="newValue">current (new) value</param>
    public void OnValueChanged(int oldValue, int newValue);
}

/// <summary>
///     Class to hold the arguments for the ValueChanged event.
/// </summary>
/// <param name="oldValue">previous value</param>
/// <param name="newValue">current value</param>
public class ValueChangedEventArgs(int oldValue, int newValue) : EventArgs
{
    /// <summary>
    ///     The new value
    /// </summary>
    public int NewValue { get; } = newValue;

    /// <summary>
    ///     The previous value
    /// </summary>
    public int OldValue { get; } = oldValue;

    /// <summary>
    ///     The change in value
    /// </summary>
    public int Delta => NewValue - OldValue;

    /// <summary>
    ///     The sign of the change in value. ("+" for positive, "" for zero, "-" for negative)
    /// </summary>
    public string Sign => Delta > 0 ? "+" : Delta < 0 ? "-" : "";

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Sign}{Math.Abs(Delta)}";
    }
}