using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Options;

/// <summary>
///     Options for instantiating a skill.
/// </summary>
public abstract class SkillOptions
{
    /// <summary>
    ///     The name of the skill.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     The logger to use for logging events related to the skill.
    /// </summary>
    public ILogger? Logger { get; set; }

    /// <summary>
    ///     The maximum level that the skill can reach.
    /// </summary>
    public int MaxLevel { get; set; } = 100;

    /// <summary>
    ///     The amount of skill experience points required to reach the first level of the skill. (Affects scaling)
    /// </summary>
    public int PointsForFirstLevel { get; set; } = 1;

    /// <summary>
    ///     The scaling factor for the skill's level progression. This determines how quickly the skill levels up as points are
    ///     accumulated.
    /// </summary>
    public float LevelScaling { get; set; } = 1.0f;
}