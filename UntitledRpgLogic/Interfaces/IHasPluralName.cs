using UntitledRpgLogic.Classes;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that additionally have a plural name.
/// </summary>
public interface IHasPluralName : IHasName
{
    /// <summary>
    ///     The plural name of the object.
    /// </summary>
    PluralName PluralName { get; }
}