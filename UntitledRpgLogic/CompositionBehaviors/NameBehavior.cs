using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class NameBehavior : IHasName
{
    /// <summary>
    ///     Create a new instance of the NameBehavior class.
    /// </summary>
    /// <param name="name">if not provided, will use an empty string</param>
    /// <param name="pluralName">if not provided, the best guess will be used based on <see cref="name"/> and 
    /// <see cref="Utility.BestGuessPlural"/></param>
    /// <param name="nameAsAdjective">if not provided, will just use <see cref="name"/></param>
    public NameBehavior(string? name = null, string? pluralName = null, string? nameAsAdjective = null)
    {
        Name = name ?? string.Empty;
        PluralName = pluralName ?? Utility.BestGuessPlural(Name);
        NameAsAdjective = nameAsAdjective ?? Name;
    }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public string PluralName { get; init; }

    /// <inheritdoc />
    public string NameAsAdjective { get; init; }
}