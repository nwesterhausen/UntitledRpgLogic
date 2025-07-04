using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = EventIds.STAT_CREATED,
        Level = LogLevel.Information,
        Message = "Created stat {StatName} with value {StatValue}/{StatMaxValue}")]
    public static partial void LogStatCreated(this ILogger logger, string statName, int statValue, int statMaxValue);

    [LoggerMessage(
        EventId = EventIds.STAT_DAMAGE_TAKEN,
        Level = LogLevel.Information,
        Message =
            "Stat {StatName} took {FinalDamage} damage ({FinalDamagePercentage}%). Incoming: {IncomingDamage}. Source: {SourceId}")]
    public static partial void LogStatDamageTaken(this ILogger logger, string statName, int finalDamage,
        float finalDamagePercentage, int incomingDamage, Guid sourceId);

    [LoggerMessage(
        EventId = EventIds.STAT_HEALED,
        Level = LogLevel.Information,
        Message = "Stat {StatName} healed for {HealAmount} points ({HealPercentage}%). Source: {SourceId}")]
    public static partial void LogStatHealed(this ILogger logger, string statName, int healAmount, float healPercentage,
        Guid sourceId);

    [LoggerMessage(
        EventId = EventIds.MODIFIER_APPLIED,
        Level = LogLevel.Debug,
        Message = "Applied modifier {ModifierName} to stat {StatName}.")]
    public static partial void LogModifierApplied(this ILogger logger, string modifierName, string statName);

    [LoggerMessage(
        EventId = EventIds.STAT_ILLEGAL_CHANGE,
        Level = LogLevel.Warning,
        Message = "Illegal attempt to modify stat {StatName}. Reason: {Reason}")]
    public static partial void LogIllegalStatChange(this ILogger logger, string statName, string reason);

    [LoggerMessage(
        EventId = EventIds.SKILL_ADD_POINTS_MAX_LEVEL,
        Level = LogLevel.Warning,
        Message = "Attempted to add {Points} points to a max-level skill: {SkillName}.")]
    public static partial void LogAddPointsToMaxLevelSkill(this ILogger logger, int points, string skillName);

    [LoggerMessage(
        EventId = EventIds.LEVELABLE_LEVEL_CHANGED,
        Level = LogLevel.Information,
        Message = "{ItemType} {ItemName} advanced from level {OldLevel} to {NewLevel}.")]
    public static partial void LogLevelableChanged(this ILogger logger, string itemType, string itemName, int oldLevel,
        int newLevel);

    [LoggerMessage(
        EventId = EventIds.LEVELABLE_LEVEL_CHANGED,
        Level = LogLevel.Information,
        Message = "{ItemType} advanced from level {OldLevel} to {NewLevel}.")]
    public static partial void LogLevelableChangedGeneric(this ILogger logger, string itemType, int oldLevel,
        int newLevel);

    [LoggerMessage(
        EventId = EventIds.LEVELABLE_POINTS_CHANGED,
        Level = LogLevel.Debug,
        Message = "{ItemType} {ItemName} gained {PointsGained} points. Total: {TotalPoints}.")]
    public static partial void LogLevelablePointsChanged(this ILogger logger, string itemType, string itemName,
        int pointsGained, int totalPoints);

    [LoggerMessage(
        EventId = EventIds.LEVELABLE_POINTS_CHANGED,
        Level = LogLevel.Debug,
        Message = "{ItemType} gained {PointsGained} points. Total: {TotalPoints}.")]
    public static partial void LogLevelablePointsChangedGeneric(this ILogger logger, string itemType, int pointsGained,
        int totalPoints);
}
