using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Helpers;

/// <summary>
///     Provides utility operations for inspecting and progressing entity character levels.
/// </summary>
public static class LevelProgressionHelper
{
    /// <summary>
    ///     Gets the entity's current level value. Returns 0 if the level stat is not instanced.
    /// </summary>
    public static int GetCurrentLevel(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Stats is null or { Count: 0 }) return 0;

        return entity.Stats
            .FirstOrDefault(s => s.InstancedStat?.StatDefinitionId == WellKnownIdentifiers.PlayerLevel)
            ?.InstancedStat?.ApparentValue ?? 0;
    }

    /// <summary>
    ///     Determines whether an entity is eligible to advance in level under a specified maximum ceiling.
    /// </summary>
    /// <param name="entity">The entity to evaluate.</param>
    /// <param name="maxLevelCeiling">
    ///     The optional maximum level cap imposed by the game consumer.
    ///     If omitted or null, the default unbounded ceiling (<see cref="int.MaxValue" />) is used.
    /// </param>
    public static bool CanLevelUp(Entity entity, int? maxLevelCeiling = null)
    {
        var currentLevel = GetCurrentLevel(entity);
        var ceiling = maxLevelCeiling ?? int.MaxValue;

        return currentLevel < ceiling;
    }

    /// <summary>
    ///     Applies a level change while clamping the resulting value between the minimum floor and the consumer's ceiling.
    /// </summary>
    /// <param name="entity">The target entity.</param>
    /// <param name="levelDelta">The number of levels to add (positive) or subtract (negative).</param>
    /// <param name="maxLevelCeiling">The optional maximum level ceiling.</param>
    /// <param name="minLevelFloor">The minimum level floor (defaults to 1).</param>
    /// <returns><c>true</c> if the level was modified; otherwise, <c>false</c>.</returns>
    public static bool TryAdjustLevel(Entity entity, int levelDelta, int? maxLevelCeiling = null, int minLevelFloor = 1)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var instancedStat = entity.Stats?
            .FirstOrDefault(s => s.InstancedStat?.StatDefinitionId == WellKnownIdentifiers.PlayerLevel)
            ?.InstancedStat;

        if (instancedStat is null) return false;

        var ceiling = maxLevelCeiling ?? int.MaxValue;
        var newLevel = Math.Clamp(instancedStat.BaseValue + levelDelta, minLevelFloor, ceiling);

        if (newLevel == instancedStat.BaseValue) return false;

        instancedStat.BaseValue = newLevel;
        instancedStat.ApparentValue = newLevel;
        return true;
    }
}
