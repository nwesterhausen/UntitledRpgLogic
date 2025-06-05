using System.Numerics;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that notify when their value changes.
/// </summary>
public interface INotifyValueChanged<T> where T : ISubtractionOperators<T, T, T>
{
    /// <summary>
    ///     Event that is triggered when the value changes.
    /// </summary>
    event EventHandler<ValueChangedEventArgs<T>>? ValueChanged;

    /// <summary>
    ///     Method to call when the value changes.
    /// </summary>
    /// <param name="oldValue">previous value</param>
    /// <param name="newValue">current (new) value</param>
    void OnValueChanged(T oldValue, T newValue);
}

/// <summary>
///     Class to hold the arguments for the ValueChanged event.
/// </summary>
public abstract class ValueChangedEventArgs<T>(T oldValue, T newValue)
    : EventArgs where T : ISubtractionOperators<T, T, T>
{
    /// <summary>
    ///     The new value after the change.
    /// </summary>
    public T NewValue { get; } = newValue;

    /// <summary>
    ///     The previous value before the change.
    /// </summary>
    public T OldValue { get; } = oldValue;

    /// <summary>
    ///     The difference between the new value and the old value.
    /// </summary>
    public T Delta => NewValue - OldValue;

    /// <summary>
    ///     Which sign the delta has (+ or -). For zero, it returns an empty string.
    /// </summary>
    public string Sign
        => Comparer<T>.Default.Compare(Delta, default!) > 0 ? "+" :
            Comparer<T>.Default.Compare(Delta, default!) < 0 ? "-" : "";


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Sign}{Delta}";
    }
}

/// <summary>
///     Interface for classes that notify when their value changes.
/// </summary>
public interface INotifyValueChangedWithName<T> where T : IHasName, ISubtractionOperators<T, T, T>
{
    /// <summary>
    ///     Event that is triggered when the value changes.
    /// </summary>
    event EventHandler<ValueChangedEventArgsWithName<T>>? ValueChanged;

    /// <summary>
    ///     Method to call when the value changes.
    /// </summary>
    /// <param name="oldValue">previous value</param>
    /// <param name="newValue">current (new) value</param>
    void OnValueChanged(T oldValue, T newValue);
}

/// <summary>
///     Class to hold the arguments for the ValueChanged event.
/// </summary>
public abstract class ValueChangedEventArgsWithName<T>(T oldValue, T newValue) : EventArgs where T :
    ISubtractionOperators<T, T, T>, IHasName
{
    /// <summary>
    ///     The new value after the change.
    /// </summary>
    public T NewValue { get; } = newValue;

    /// <summary>
    ///     The previous value before the change.
    /// </summary>
    public T OldValue { get; } = oldValue;

    /// <summary>
    ///     The difference between the new value and the old value.
    /// </summary>
    public T Delta => NewValue - OldValue;

    /// <summary>
    ///     Which sign the delta has (+ or -). For zero, it returns an empty string.
    /// </summary>
    public string Sign
        => Comparer<T>.Default.Compare(Delta, default!) > 0 ? "+" :
            Comparer<T>.Default.Compare(Delta, default!) < 0 ? "-" : "";


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Sign}{Delta} {NewValue.Name}";
    }
}