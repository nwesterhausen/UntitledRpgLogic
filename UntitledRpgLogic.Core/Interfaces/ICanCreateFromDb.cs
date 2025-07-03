using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Defines a contract for types that can be created from a database model.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface and can be created.</typeparam>
/// <typeparam name="TModel">The type of the database model to create from.</typeparam>
public interface ICanCreateFromDb<out TSelf, in TModel>
    where TSelf : ICanCreateFromDb<TSelf, TModel>
{
    /// <summary>
    ///     Creates an instance of <typeparamref name="TSelf" /> from the given database model.
    /// </summary>
    /// <param name="dbModel">The database model.</param>
    /// <param name="logger">optional logger for created object to use</param>
    /// <param name="instanceId">optional instance ID, if applicable.</param>
    /// <returns>A new instance of <typeparamref name="TSelf" />.</returns>
    static abstract TSelf FromDbModel(TModel dbModel, ILogger? logger = null, Guid? instanceId = null);
}
