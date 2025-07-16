namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for objects that can be persisted to a database, with complex aggregates.
/// </summary>
/// <typeparam name="TAggregateDbModel">the model used to persist the object in the database</typeparam>
public interface IPersistableAggregate<out TAggregateDbModel>
{
	/// <summary>
	///     Maps the domain aggregate to its composite database model,
	///     including relationships, e.g. for stats and skills.
	/// </summary>
	public TAggregateDbModel ToDbAggregateModel();
}
