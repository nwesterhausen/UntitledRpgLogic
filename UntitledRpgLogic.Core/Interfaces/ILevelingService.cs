namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines a generic contract for a service that manages leveling for any object
///     that implements IHasLeveling.
/// </summary>
/// <typeparam name="T">The type of object being managed, which must implement IHasLeveling.</typeparam>
public interface ILevelingService<T> where T : IHasLeveling
{
    /// <summary>
    ///     Adds experience points to the target object and handles any resulting level-ups.
    /// </summary>
    void AddPoints(T target, int points);

    /// <summary>
    ///     Removes experience points from the target object and handles any resulting level changes.
    /// </summary>
    void RemovePoints(T target, int points);

    /// <summary>
    ///     Sets the total experience points for the target object and recalculates its level from scratch.
    /// </summary>
    void SetPoints(T target, int points);

    /// <summary>
    ///     Calculates the TOTAL accumulated experience points required to attain a specific level.
    /// </summary>
    int GetTotalPointsForLevel(T target, int targetLevel);

    /// <summary>
    ///     Calculates the remaining experience points needed for the target to reach the next level.
    /// </summary>
    int GetPointsToNextLevel(T target);

    /// <summary>
    ///     Calculates the target's progress towards the next level as a percentage (0.0f to 1.0f).
    /// </summary>
    float GetProgressToNextLevel(T target);
}
