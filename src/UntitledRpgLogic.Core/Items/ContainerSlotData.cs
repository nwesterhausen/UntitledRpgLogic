namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Represents the serialized state of a single slot within an inventory container.
/// </summary>
/// <param name="SlotIndex">The 0-based index of the slot inside the container.</param>
/// <param name="ItemDefinitionId">The unique identifier of the item catalog template.</param>
/// <param name="Quantity">The current stack count of items occupying the slot.</param>
public record ContainerSlotData(int SlotIndex, Ulid ItemDefinitionId, int Quantity);
