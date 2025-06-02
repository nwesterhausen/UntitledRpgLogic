using UntitledRpgLogic.BaseImplementations;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class NameBehavior : HasNameBase
{
    /// <inheritdoc />
    public NameBehavior(string? name = null)
    {
        if (name != null)
            Name = name;
    }
}