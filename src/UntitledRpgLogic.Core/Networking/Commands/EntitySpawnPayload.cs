namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Server payload notifying clients to spawn and display an entity in the active world.
/// </summary>
/// <param name="EntityId">The unique identifier of the spawned entity.</param>
/// <param name="DefinitionId">The unique identifier of the entity archetype definition.</param>
/// <param name="X">The initial horizontal world coordinate.</param>
/// <param name="Y">The initial vertical world coordinate.</param>
public record EntitySpawnPayload(Ulid EntityId, Ulid DefinitionId, float X, float Y);
