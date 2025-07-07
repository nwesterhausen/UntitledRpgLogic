using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Extensions.Logging;

/// <summary>
///     Contains high-performance, strongly-typed logging extension methods for ILogger.
/// </summary>
public static partial class LoggerExtensions
{
    /// <summary>
    ///     Logs the creation of a stat with its name, value, and maximum value.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="statName"></param>
    /// <param name="statValue"></param>
    /// <param name="statMaxValue"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_CREATED,
        Level = LogLevel.Information,
        Message = "Created stat {StatName} with value {StatValue}/{StatMaxValue}")]
    public static partial void LogStatCreated(this ILogger logger, string statName, int statValue, int statMaxValue);

    /// <summary>
    ///     Logs the damage taken by a stat, including the final damage, percentage of final damage, incoming damage,
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="statName"></param>
    /// <param name="finalDamage"></param>
    /// <param name="finalDamagePercentage"></param>
    /// <param name="incomingDamage"></param>
    /// <param name="sourceId"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_DAMAGE_TAKEN,
        Level = LogLevel.Information,
        Message =
            "Stat {StatName} took {FinalDamage} damage ({FinalDamagePercentage}%). Incoming: {IncomingDamage}. Source: {SourceId}")]
    public static partial void LogStatDamageTaken(this ILogger logger, string statName, int finalDamage,
        float finalDamagePercentage, int incomingDamage, Guid sourceId);

    /// <summary>
    ///     Logs the healing of a stat, including the heal amount, percentage of heal, and source ID.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="statName"></param>
    /// <param name="healAmount"></param>
    /// <param name="healPercentage"></param>
    /// <param name="sourceId"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_HEALED,
        Level = LogLevel.Information,
        Message = "Stat {StatName} healed for {HealAmount} points ({HealPercentage}%). Source: {SourceId}")]
    public static partial void LogStatHealed(this ILogger logger, string statName, int healAmount, float healPercentage,
        Guid sourceId);

    /// <summary>
    ///     Logs the application of a modifier to a stat, including the modifier name and stat name.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="modifierName"></param>
    /// <param name="statName"></param>
    [LoggerMessage(
        EventId = EventIds.MODIFIER_APPLIED,
        Level = LogLevel.Debug,
        Message = "Applied modifier {ModifierName} to stat {StatName}.")]
    public static partial void LogModifierApplied(this ILogger logger, string modifierName, string statName);

    /// <summary>
    ///     Logs an illegal attempt to modify a stat, including the stat name and reason for the illegal change.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="statName"></param>
    /// <param name="reason"></param>
    [LoggerMessage(
        EventId = EventIds.STAT_ILLEGAL_CHANGE,
        Level = LogLevel.Warning,
        Message = "Illegal attempt to modify stat {StatName}. Reason: {Reason}")]
    public static partial void LogIllegalStatChange(this ILogger logger, string statName, string reason);

    /// <summary>
    ///     Logs the creation of a skill, including its name, level, and maximum level.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="points"></param>
    /// <param name="skillName"></param>
    [LoggerMessage(
        EventId = EventIds.SKILL_ADD_POINTS_MAX_LEVEL,
        Level = LogLevel.Warning,
        Message = "Attempted to add {Points} points to a max-level skill: {SkillName}.")]
    public static partial void LogAddPointsToMaxLevelSkill(this ILogger logger, int points, string skillName);

    /// <summary>
    ///     Logs the creation of a skill, including its name, level, and maximum level.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="itemType"></param>
    /// <param name="itemName"></param>
    /// <param name="oldLevel"></param>
    /// <param name="newLevel"></param>
    [LoggerMessage(
        EventId = EventIds.LEVELABLE_LEVEL_CHANGED,
        Level = LogLevel.Information,
        Message = "{ItemType} {ItemName} advanced from level {OldLevel} to {NewLevel}.")]
    public static partial void LogLevelableChanged(this ILogger logger, string itemType, string itemName, int oldLevel,
        int newLevel);

    /// <summary>
    ///     Logs the level change of a generic levelable item, such as a skill or character, including the item type,
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="itemType"></param>
    /// <param name="oldLevel"></param>
    /// <param name="newLevel"></param>
    [LoggerMessage(
        EventId = EventIds.LEVELABLE_LEVEL_CHANGED,
        Level = LogLevel.Information,
        Message = "{ItemType} advanced from level {OldLevel} to {NewLevel}.")]
    public static partial void LogLevelableChangedGeneric(this ILogger logger, string itemType, int oldLevel,
        int newLevel);

    /// <summary>
    ///     Logs the change in points for a levelable item, such as a skill or character, including the item type,
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="itemType"></param>
    /// <param name="itemName"></param>
    /// <param name="pointsGained"></param>
    /// <param name="totalPoints"></param>
    [LoggerMessage(
        EventId = EventIds.LEVELABLE_POINTS_CHANGED,
        Level = LogLevel.Debug,
        Message = "{ItemType} {ItemName} gained {PointsGained} points. Total: {TotalPoints}.")]
    public static partial void LogLevelablePointsChanged(this ILogger logger, string itemType, string itemName,
        int pointsGained, int totalPoints);

    /// <summary>
    ///     Logs the change in points for a generic levelable item, such as a skill or character, including the item type,
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="itemType"></param>
    /// <param name="pointsGained"></param>
    /// <param name="totalPoints"></param>
    [LoggerMessage(
        EventId = EventIds.LEVELABLE_POINTS_CHANGED,
        Level = LogLevel.Debug,
        Message = "{ItemType} gained {PointsGained} points. Total: {TotalPoints}.")]
    public static partial void LogLevelablePointsChangedGeneric(this ILogger logger, string itemType, int pointsGained,
        int totalPoints);
}
