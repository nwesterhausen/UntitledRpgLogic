using Microsoft.Extensions.Logging;
using UntitledRpgLogic.CompositionBehaviors;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Models;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Game;

/// <summary>
///     Represents a base skill in the RPG logic, implementing the ISkill interface.
/// </summary>
public class BaseSkill : ISkill, ICanCreateFromDb<BaseSkill, SkillDefinition>
{
    /// <summary>
    ///     Creates a new instance of the BaseSkill class with the specified configuration and optional logger.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="logger"></param>
    /// <param name="instanceId"></param>
    public BaseSkill(SkillDataConfig config, ILogger? logger = null, Guid? instanceId = null)
    {
        GuidBehavior = new GuidBehavior(config.ExplicitId);
        NameBehavior = new NameBehavior(config.Name);
        LoggingBehavior = new LoggingBehavior(logger);
        LevelingBehavior = new LevelingBehavior(config.LevelingOptions ?? new LevelingOptions());
        InstanceId = instanceId ?? Guid.Empty;

        LogEvent(EventIds.SKILL_CREATED, this);

        // Register the ValueChanged event to log changes in skill points
        LevelingBehavior.ValueChanged += (_, valueChanges) =>
        {
            LogEvent(EventIds.SKILL_LEVEL_CHANGED, Name, valueChanges.PreviousValue, valueChanges.NewValue);
            ValueChanged?.Invoke(this, valueChanges);
        };
        // Register the LevelChanged event to log changes in skill level
        LevelingBehavior.LevelChanged += (_, levelChanges) =>
        {
            LogEvent(EventIds.SKILL_LEVEL_CHANGED, Name, levelChanges.PreviousValue, levelChanges.NewValue);
            LevelChanged?.Invoke(this, levelChanges);
        };
    }

    /// <summary>
    ///     Provides the name and related behaviors for the skill.
    /// </summary>
    private NameBehavior NameBehavior { get; }

    /// <summary>
    ///     Provides the GUID and related behaviors for the skill.
    /// </summary>
    private GuidBehavior GuidBehavior { get; }

    /// <summary>
    ///     Provides the logging behavior for the skill, allowing it to log events and errors.
    /// </summary>
    private LoggingBehavior LoggingBehavior { get; }

    /// <summary>
    ///     Provides the leveling behavior for the skill, managing its level, experience, and points.
    /// </summary>
    private LevelingBehavior LevelingBehavior { get; }

    /// <inheritdoc />
    public static BaseSkill FromDbModel(SkillDefinition dbModel, ILogger? logger = null, Guid? instanceId = null)
    {
        return new BaseSkill(new SkillDataConfig
        {
            ExplicitId = dbModel.Id,
            Name = dbModel.Name,
            LevelingOptions = new LevelingOptions
            {
                MaxLevel = dbModel.MaxLevel,
                ScalingFactorA = dbModel.ScalingFactorA,
                ScalingFactorB = dbModel.ScalingFactorB,
                ScalingFactorC = dbModel.ScalingFactorC,
                PointsForFirstLevel = dbModel.PointsForFirstLevel,
                ScalingCurve = dbModel.ScalingCurve
            }
        }, logger, instanceId);
    }


    /// <inheritdoc />
    public string Name => NameBehavior.Name;

    /// <inheritdoc />
    public string PluralName => NameBehavior.PluralName;

    /// <inheritdoc />
    public string NameAsAdjective => NameBehavior.NameAsAdjective;

    /// <inheritdoc />
    public Guid Guid => GuidBehavior.Guid;

    /// <inheritdoc />
    public string Id => GuidBehavior.Id;

    /// <inheritdoc />
    public string ShortGuid => GuidBehavior.ShortGuid;

    /// <inheritdoc />
    public ILogger Logger => LoggingBehavior.Logger;

    /// <inheritdoc />
    public void LogErrorEvent(Exception? exception, EventId eventId, params object?[] args)
    {
        LoggingBehavior.LogErrorEvent(exception, eventId, args);
    }

    /// <inheritdoc />
    public void LogEvent(EventId eventId, params object?[] args)
    {
        LogErrorEvent(null, eventId, args);
    }

    /// <inheritdoc />
    public void LogError(Exception exception, EventId eventId)
    {
        LoggingBehavior.LogError(exception, eventId);
    }

    /// <inheritdoc />
    public int Value => LevelingBehavior.Value;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? ValueChanged;

    /// <inheritdoc />
    public void AddPoints(int points)
    {
        LevelingBehavior.AddPoints(points);
    }

    /// <inheritdoc />
    public void RemovePoints(int points)
    {
        LevelingBehavior.RemovePoints(points);
    }

    /// <inheritdoc />
    public void SetPoints(int points)
    {
        LevelingBehavior.SetPoints(points);
    }

    /// <inheritdoc />
    public int Level => LevelingBehavior.Level;

    /// <inheritdoc />
    public int MaxLevel => LevelingBehavior.MaxLevel;

    /// <inheritdoc />
    public int ExperienceToNextLevel => LevelingBehavior.ExperienceToNextLevel;

    /// <inheritdoc />
    public float ScalingFactorA => LevelingBehavior.ScalingFactorA;

    /// <inheritdoc />
    public float ScalingFactorB => LevelingBehavior.ScalingFactorB;

    /// <inheritdoc />
    public float ScalingFactorC => LevelingBehavior.ScalingFactorC;

    /// <inheritdoc />
    public ScalingCurveType ScalingCurve => LevelingBehavior.ScalingCurve;

    /// <inheritdoc />
    public int PointsForFirstLevel => LevelingBehavior.PointsForFirstLevel;

    /// <inheritdoc />
    public event EventHandler<ValueChangedEventArgs>? LevelChanged;

    /// <inheritdoc />
    public Guid InstanceId { get; init; }
}