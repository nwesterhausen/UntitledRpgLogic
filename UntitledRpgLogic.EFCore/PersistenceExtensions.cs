using Microsoft.EntityFrameworkCore;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.EFCore;

/// <summary>
///     Extensions for persisting domain objects to a database using Entity Framework Core.
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    ///     Stores the domain object in the database by mapping it to its EF Core model.
    /// </summary>
    /// <typeparam name="TDomain">The domain object type, implementing IPersistable&lt;TDbModel&gt;</typeparam>
    /// <typeparam name="TDbModel">The EF Core database model type</typeparam>
    /// <param name="domainObj">The domain object</param>
    /// <param name="context">An instance of the DbContext</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public static async Task StoreInDatabaseAsync<TDomain, TDbModel>(
        this TDomain domainObj, DbContext context)
        where TDomain : IPersistable<TDbModel>
        where TDbModel : class
    {
        // Map the domain object to its persistence model.
        TDbModel model = domainObj.ToDbModel();
        // Use the DbContext to add the model.
        context.Set<TDbModel>().Add(model);
        // Save changes asynchronously.
        await context.SaveChangesAsync();
    }
}

/// <summary>
///     Persistence extensions for aggregate database models
/// </summary>
public static class AggregatePersistenceExtensions
{
    /// <summary>
    ///     Stores the aggregate model (and its related child objects) in the database.
    /// </summary>
    public static async Task StoreAggregateInDatabaseAsync<TDomain, TAggregateDbModel>(
        this TDomain domainAggregate, DbContext context)
        where TDomain : IPersistableAggregate<TAggregateDbModel>
        where TAggregateDbModel : class
    {
        TAggregateDbModel aggregateModel = domainAggregate.ToDbAggregateModel();
        // If it’s a new aggregate, use Add; if it already exists, you might use Update.
        // Here we assume an Add for simplicity.
        context.Set<TAggregateDbModel>().Add(aggregateModel);
        await context.SaveChangesAsync();
    }
}
