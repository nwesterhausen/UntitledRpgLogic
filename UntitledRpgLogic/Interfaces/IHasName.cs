namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a name. Provides a way to access the name of the object.
/// </summary>
public interface IHasName
{
    /// <summary>
    ///     The name of the object.
    /// </summary>
    public string Name { get; }
}