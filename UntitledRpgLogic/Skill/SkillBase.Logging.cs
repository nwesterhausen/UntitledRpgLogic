using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Skill;

public abstract partial class SkillBase
{
    public const int SKILL_CREATED = 2000;
    public const int SKILL_LEVEL_CHANGED = 2001;
    public const int SKILL_POINTS_CHANGED = 2002;

    protected internal ILogger _logger;


    /// <summary>
    ///     Log when points are added or removed from a skill.
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="pointsChange"></param>
    /// <param name="progress"></param>
    [LoggerMessage(
        EventId = SKILL_POINTS_CHANGED,
        Level = LogLevel.Debug,
        Message = "{skillName} {pointsChange}: {progress} to next level")]
    private partial void LogSkillPointsChanged(string skillName, string pointsChange, string progress);
}