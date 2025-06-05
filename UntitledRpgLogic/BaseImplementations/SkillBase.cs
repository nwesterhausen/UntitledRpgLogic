using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Represents a base class for skills in the RPG system, providing common functionality such as leveling.
/// </summary>
public abstract class SkillBase
{
    /// <summary>
    ///     Provides the leveling behavior for the skill, which includes functionality for tracking experience points and
    ///     levels.
    /// </summary>
    private readonly HasLevelingBase _levelingBehavior;

    /// <summary>
    ///     Adds logging for the skill.
    /// </summary>
    private readonly LoggingBehavior _logging;

    /// <summary>
    ///     Adds a name to the skill.
    /// </summary>
    private readonly IHasName _nameBehavior;

    /// <summary>
    ///     Creates a new instance of <see cref="SkillBase" />.
    /// </summary>
    protected SkillBase(SkillOptions options)
    {
        _nameBehavior = new NameBehavior(options.Name);

        // Create a LevelingOptions with what is provided in the SkillOptions
        var levelingOptions = new LevelingOptions
        {
            MaxLevel = options.MaxLevel,
            PointsForFirstLevel = options.PointsForFirstLevel,
            LevelScalingA = options.LevelScaling,
            ScalingCurve = ScalingCurveType.Linear
        };
        // Initialize the leveling behavior with the provided options
        _levelingBehavior = new LevelingBehavior(levelingOptions);

        _logging = new LoggingBehavior(options.Logger ?? NullLogger<SkillBase>.Instance);

        _logging.LogEvent(LoggingEventIds.SKILL_CREATED, this);

        // Register the ValueChanged event to log changes in skill points
        _levelingBehavior.ValueChanged += (oldValue, newValue) =>
        {
            _logging.LogEvent(LoggingEventIds.SKILL_LEVEL_CHANGED, Name, oldValue, newValue);
        };
        // Register the LevelChanged event to log changes in skill level
        _levelingBehavior.LevelChanged += (oldLevel, newLevel) =>
        {
            _logging.LogEvent(LoggingEventIds.SKILL_LEVEL_CHANGED, Name, oldLevel, newLevel);
        };
    }

    /// <summary>
    ///     The name of the skill
    /// </summary>
    public string Name => _nameBehavior.Name;

    /// <summary>
    ///     How many total points are required for the current level of the skill.
    /// </summary>
    public int CurrentLevelPointsRequirement => _levelingBehavior.ExperienceToNextLevel;

    /// <summary>
    ///     How many points this skill has accumulated, which is typically used to determine the skill's level or proficiency.
    /// </summary>
    public int Points => _levelingBehavior.Value;

    /// <summary>
    ///     The level of the skill, which is typically an integer value representing the skill's proficiency or mastery.
    /// </summary>
    public int Value => _levelingBehavior.Level;
}