namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines a generic contract for services that can modify an object's core integer value.
/// </summary>
/// <typeparam name="T">The type of object being modified, which must implement IHasValue.</typeparam>
public interface IChangeableValueService<T> where T : IHasMutableValue
{
	/// <summary>
	///     Adds a specified number of points to the target object's value.
	/// </summary>
	public void AddPoints(T target, int points);

	/// <summary>
	///     Removes a specified number of points from the target object's value.
	/// </summary>
	public void RemovePoints(T target, int points);

	/// <summary>
	///     Sets the target object's value to a specific number of points.
	/// </summary>
	public void SetPoints(T target, int points);
}
