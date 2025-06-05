using UntitledRpgLogic.BaseImplementations;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class MonoNameBehavior : HasMonoNameBase
{
    /// <inheritdoc />
    public MonoNameBehavior(string? name = null)
    {
        if (name != null)
            Name = name;
    }
}