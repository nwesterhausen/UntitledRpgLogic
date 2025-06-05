using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class NameBehavior : IHasName
{
    /// <summary>
    ///     Create a new instance of the NameBehavior class.
    /// </summary>
    /// <param name="name"></param>
    public NameBehavior(string? name = null)
    {
        Name = name ?? string.Empty;
    }

    /// <inheritdoc />
    public string Name { get; set; }
}