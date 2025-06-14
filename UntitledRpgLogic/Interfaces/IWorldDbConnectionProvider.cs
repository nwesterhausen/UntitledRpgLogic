namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Database connection provider which allows for changing database based on which world is loaded.
/// </summary>
public interface IWorldDbConnectionProvider
{
    /// <summary>
    ///     Get the current connection string
    /// </summary>
    /// <returns>current connection string</returns>
    string GetCurrentConnectionString();

    /// <summary>
    ///     Sets the active world to load/create by name
    /// </summary>
    /// <param name="worldName">name of the world</param>
    void SetActiveWorldDb(string worldName);

    /// <summary>
    ///     Sets an active "main reference" database which is an immutable base for the world files.
    /// </summary>
    void SetActiveMainReferenceDb();

    /// <summary>
    ///     Creates a new world database based on the main reference.
    /// </summary>
    /// <param name="worldName">The name of the world to create.</param>
    /// <returns>True if the world database was created successfully, false otherwise.</returns>
    bool CreateNewWorldDb(string worldName);

    /// <summary>
    ///     Get the path to the main reference db
    /// </summary>
    /// <returns>The file path to the main reference database.</returns>
    string GetMainReferenceDbPath();

    /// <summary>
    ///     Get the path to the world save database by name
    /// </summary>
    /// <param name="worldName">The name of the world.</param>
    /// <returns>The file path to the specified world's database.</returns>
    string GetWorldDbPath(string worldName);
}