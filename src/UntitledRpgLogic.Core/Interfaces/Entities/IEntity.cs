using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Base contract for any concrete instance spawned into the game world (Characters, Chests, Vehicles).
/// </summary>
public interface IEntity : IDbEntity<Ulid>
{
	/// <summary>
	/// 	The name of the spawned instance
	/// </summary>
	Name Name { get; }

	/// <summary>
	/// 	The id of the definition (or blueprint) for this entity
	/// </summary>
	Ulid? DefinitionId { get; }
}
