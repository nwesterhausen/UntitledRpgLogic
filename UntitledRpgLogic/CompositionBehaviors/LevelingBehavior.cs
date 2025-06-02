using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class LevelingBehavior : HasLevelingBase
{
    /// <summary>
    ///     Constructor for the LevelingBehavior class.
    /// </summary>
    /// <param name="options"></param>
    public LevelingBehavior(LevelingOptions options) : base(options)
    {
    }
}