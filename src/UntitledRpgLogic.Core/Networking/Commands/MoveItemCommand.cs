namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Client command requesting an item transfer between containers or slot positions.
/// </summary>
/// <param name="SourceContainerId">The unique identifier of the source inventory container.</param>
/// <param name="ItemInstanceId">The unique identifier of the specific item instance being moved.</param>
/// <param name="TargetContainerId">The unique identifier of the target inventory container.</param>
/// <param name="TargetSlotIndex">The destination slot index.</param>
/// <param name="Quantity">The quantity of items to transfer.</param>
public record MoveItemCommand(
	Ulid SourceContainerId,
	Ulid ItemInstanceId,
	Ulid TargetContainerId,
	int TargetSlotIndex,
	int Quantity);
