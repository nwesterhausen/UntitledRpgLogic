using UntitledRpgLogic.Events;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a value
/// </summary>
public interface IHasValue
{
    /// <summary>
    ///     Gets the current value.
    /// </summary>
    public int Value { get; }


    /// <summary>
    ///     Event that is triggered when the value changes.
    /// </summary>
    event EventHandler<ValueChangedEventArgs> ValueChanged;
}