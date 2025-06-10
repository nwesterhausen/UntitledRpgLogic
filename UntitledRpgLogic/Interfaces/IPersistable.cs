namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for objects that can be persisted to a database.
/// </summary>
/// <typeparam name="T">the model class used for database interactions</typeparam>
public interface IPersistable<out T>
{
    /// <summary>
    ///     Converts the current instance to a database model.
    /// </summary>
    /// <returns>an object that can be directly inserted into the database</returns>
    T ToDbModel();
}