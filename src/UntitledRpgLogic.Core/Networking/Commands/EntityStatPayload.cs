namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Server payload synchronizing apparent stat values and max limits for an entity.
/// </summary>
/// <param name="EntityId">The unique identifier of the entity owning the stat.</param>
/// <param name="StatDefinitionId">The unique identifier of the stat definition.</param>
/// <param name="ApparentValue">The current apparent value of the stat.</param>
/// <param name="MaxValue">The maximum value cap defined for the stat.</param>
public record EntityStatPayload(Ulid EntityId, Ulid StatDefinitionId, int ApparentValue, int MaxValue);
