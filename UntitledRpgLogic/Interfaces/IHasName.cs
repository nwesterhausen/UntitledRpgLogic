namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that have a name. Provides a way to access the name of the object.
/// </summary>
public interface IHasName
{
    /// <summary>
    ///     The name of the object.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     The singular name of the object, which is the same as the Name property.
    /// </summary>
    string SingularName => Name;

    /// <summary>
    ///     A plural name of the object.
    /// </summary>
    string PluralName { get; }

    /// <summary>
    ///     The name of the object if it was used as an adjective. Most objects will use the singular name as the adjective,
    ///     but some may have a different form (e.g. "Sword" Soup vs "Swordy" Soup).
    /// </summary>
    string NameAsAdjective { get; }
}