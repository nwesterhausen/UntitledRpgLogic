namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines a generic contract for a service that manages leveling for any object
///     that implements IHasLeveling.
/// </summary>
/// <typeparam name="T">The type of object being managed, which must implement IHasLeveling.</typeparam>
public interface ILevelingService<T> : IChangeableValueService<T> where T : IHasLeveling
{
	// AddPoints, RemovePoints, and SetPoints are now inherited from IChangeableValueService<T>

	/// <summary>
	///     Calculates the TOTAL accumulated experience points required to attain a specific level.
	/// </summary>
	public int GetTotalPointsForLevel(T target, int targetLevel);

	/// <summary>
	///     Calculates the remaining experience points needed for the target to reach the next level.
	/// </summary>
	public int GetPointsToNextLevel(T target);

	/// <summary>
	///     Calculates the target's progress towards the next level as a percentage (0.0f to 1.0f).
	/// </summary>
	public float GetProgressToNextLevel(T target);
}
