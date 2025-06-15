namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for a unit of work pattern which is designed for handling the interfaces defined in this project.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    ///     Commits all changes made in the unit of work to the database.
    /// </summary>
    /// <returns>number of state entries written to the database</returns>
    int Commit();
}
