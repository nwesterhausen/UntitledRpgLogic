namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     An interface for objects with a value that exposes methods to change that value.
/// </summary>
public interface IHasChangeableValue : IHasValue
{
    /// <summary>
    ///     Increases the value by one point.
    /// </summary>
    public void AddPoint();

    /// <summary>
    ///     Decreases the value by one point.
    /// </summary>
    public void RemovePoint();

    /// <summary>
    ///     Increases the value by the specified number of points.
    /// </summary>
    /// <param name="points">The number of points to add.</param>
    public void AddPoints(int points);

    /// <summary>
    ///     Decreases the value by the specified number of points.
    /// </summary>
    /// <param name="points">The number of points to remove.</param>
    public void RemovePoints(int points);

    /// <summary>
    ///     Sets the value to the specified number of points.
    /// </summary>
    /// <param name="points">The value to set.</param>
    public void SetPoints(int points);
}