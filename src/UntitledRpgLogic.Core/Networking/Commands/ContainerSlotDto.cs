namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Lightweight data transfer object describing an instanced item within a container slot.
/// </summary>
/// <param name="InstanceId">The unique identifier of the specific item instance.</param>
/// <param name="DefinitionId">The unique identifier of the item's catalog definition.</param>
/// <param name="Quantity">The quantity of items stacked in this slot.</param>
/// <param name="DurabilityPercent">The remaining durability of the item normalized between 0.0 and 1.0.</param>
public record ContainerSlotDto(Ulid InstanceId, Ulid DefinitionId, int Quantity, float DurabilityPercent);
