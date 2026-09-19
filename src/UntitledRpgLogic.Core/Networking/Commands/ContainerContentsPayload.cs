using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Server payload detailing the items currently residing within an inventory container.
/// </summary>
/// <param name="ContainerId">The unique identifier of the container or inventory.</param>
/// <param name="Slots">The collection of slot data descriptors.</param>
public record ContainerContentsPayload(Ulid ContainerId, IReadOnlyCollection<ContainerSlotData> Slots);
