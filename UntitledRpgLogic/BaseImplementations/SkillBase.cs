using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.BaseImplementations;

/// <summary>
///     Represents a base class for skills in the RPG system, providing common functionality such as leveling.
/// </summary>
public abstract class SkillBase : ISkill
{
    /// <summary>
    ///     Provides a GUID for the skill, which is used for unique identification.
    /// </summary>
    private readonly GuidBehavior _guidBehavior;

    /// <summary>
    ///     Provides the leveling behavior for the skill, which includes functionality for tracking experience points and
    ///     levels.
    /// </summary>
    private readonly LevelingBehavior _levelingBehavior;

    /// <summary>
    ///     Adds logging for the skill.
    /// </summary>
    private readonly LoggingBehavior _loggingBehavior;

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
        _guidBehavior = new GuidBehavior();

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

        _loggingBehavior = new LoggingBehavior(options.Logger ?? NullLogger<SkillBase>.Instance);

        LogEvent(LoggingEventIds.SKILL_CREATED, this);

        // Register the ValueChanged event to log changes in skill points
        _levelingBehavior.ValueChanged += (oldValue, newValue) =>
        {
            LogEvent(LoggingEventIds.SKILL_LEVEL_CHANGED, Name, oldValue, newValue);
            ValueChanged?.Invoke(oldValue, newValue);
        };
        // Register the LevelChanged event to log changes in skill level
        _levelingBehavior.LevelChanged += (oldLevel, newLevel) =>
        {
            LogEvent(LoggingEventIds.SKILL_LEVEL_CHANGED, Name, oldLevel, newLevel);
            LevelChanged?.Invoke(oldLevel, newLevel);
        };
    }

    /// <summary>
    ///     How many total points are required for the current level of the skill.
    /// </summary>
    public int CurrentLevelPointsRequirement => _levelingBehavior.ExperienceToNextLevel;

    /// <summary>
    ///     How many points this skill has accumulated, which is typically used to determine the skill's level or proficiency.
    /// </summary>
    public int Points => _levelingBehavior.Value;

    /// <summary>
    ///     The name of the skill
    /// </summary>
    public string Name => _nameBehavior.Name;

    /// <summary>
    ///     The level of the skill, which is typically an integer value representing the skill's proficiency or mastery.
    /// </summary>
    public int Value => _levelingBehavior.Level;

    /// <inheritdoc />
    public void AddPoint()
    {
        _levelingBehavior.AddPoints(1);
    }

    /// <inheritdoc />
    public void RemovePoint()
    {
        _levelingBehavior.RemovePoints(1);
    }

    /// <inheritdoc />
    public void AddPoints(int points)
    {
        _levelingBehavior.AddPoints(points);
    }

    /// <inheritdoc />
    public void RemovePoints(int points)
    {
        _levelingBehavior.RemovePoints(points);
    }

    /// <inheritdoc />
    public void SetPoints(int points)
    {
        _levelingBehavior.SetPoints(points);
    }

    /// <inheritdoc />
    public event Action<int, int>? ValueChanged;

    /// <inheritdoc />
    public Guid Guid => _guidBehavior.Guid;

    /// <inheritdoc />
    public string Id => _guidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => _guidBehavior.ShortGuid;

    /// <inheritdoc />
    public ILogger Logger => _loggingBehavior.Logger;

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        _loggingBehavior.LogEvent(eventId, args);
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        _loggingBehavior.LogError(exception, eventId);
    }

    /// <inheritdoc />
    public int Level => _levelingBehavior.Level;

    /// <inheritdoc />
    public int MaxLevel
    {
        get => _levelingBehavior.MaxLevel;
        set => _levelingBehavior.MaxLevel = value;
    }

    /// <inheritdoc />
    public int ExperienceToNextLevel => _levelingBehavior.ExperienceToNextLevel;

    /// <inheritdoc />
    public float LevelScalingA
    {
        get => _levelingBehavior.LevelScalingA;
        set => _levelingBehavior.LevelScalingA = value;
    }

    /// <inheritdoc />
    public float ScalingFactorB
    {
        get => _levelingBehavior.ScalingFactorB;
        set => _levelingBehavior.ScalingFactorB = value;
    }

    /// <inheritdoc />
    public int ScalingFactorC
    {
        get => _levelingBehavior.ScalingFactorC;
        set => _levelingBehavior.ScalingFactorC = value;
    }

    /// <inheritdoc />
    public ScalingCurveType ScalingCurve
    {
        get => _levelingBehavior.ScalingCurve;
        set => _levelingBehavior.ScalingCurve = value;
    }

    /// <inheritdoc />
    public int PointsForFirstLevel
    {
        get => _levelingBehavior.PointsForFirstLevel;
        set => _levelingBehavior.PointsForFirstLevel = value;
    }

    /// <inheritdoc />
    public float ProgressToNextLevel => _levelingBehavior.ProgressToNextLevel;

    /// <inheritdoc />
    public event Action<int, int>? LevelChanged;
}