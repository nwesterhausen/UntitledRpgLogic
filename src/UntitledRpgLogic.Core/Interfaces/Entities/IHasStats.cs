using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Defines an interactive entity possessing mutable and apparent numerical attributes.
/// </summary>
public interface IHasStats
{
	/// <summary>
	///     Gets the active collection of instanced stats belonging to this entity.
	/// </summary>
	ICollection<EntityStats> Stats { get; }
}
