namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Arguments for the ValueChanged event.
/// </summary>
public class ValueChangedEventArgs
{
    /// <summary>
    ///     The constructor for ValueChangedEventArgs.
    /// </summary>
    /// <param name="previousValue">The previous value before the change.</param>
    /// <param name="newValue">The new value after the change.</param>
    public ValueChangedEventArgs(int previousValue, int newValue)
    {
        PreviousValue = previousValue;
        NewValue = newValue;
    }

    /// <summary>
    ///     The previous value before the change.
    /// </summary>
    public int PreviousValue { get; }

    /// <summary>
    ///     The new value after the change.
    /// </summary>
    public int NewValue { get; }

    /// <summary>
    ///     The difference between the new value and the previous value.
    /// </summary>
    public int Delta => NewValue - PreviousValue;

    /// <summary>
    ///     A string representation of the change, indicating whether it is an increase or decrease.
    /// </summary>
    public string Modifier => Delta switch
    {
        > 0 => "+",
        < 0 => "-",
        _ => string.Empty
    };
}
