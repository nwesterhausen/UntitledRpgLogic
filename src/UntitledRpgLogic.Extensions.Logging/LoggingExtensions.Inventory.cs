using Microsoft.Extensions.Logging;
using ZLogger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace UntitledRpgLogic.Extensions.Logging;

public static partial class InventoryLoggingExtensions
{
	[ZLoggerMessage(
		EventId = EventIdValues.PlayerEquipItem,
		Level = LogLevel.Information,
		Message = "Player {PlayerId} equipped item {ItemId} to slot {Slot}.")]
	public static partial void ItemEquipped(this ILogger logger, Ulid playerId, Ulid itemId, string slot);

	[ZLoggerMessage(
		EventId = EventIdValues.PlayerInventoryFull,
		Level = LogLevel.Warning,
		Message = "Player {PlayerId} failed to add item {ItemId}. Inventory is full.")]
	public static partial void InventoryIsFull(this ILogger logger, Ulid playerId, Ulid itemId);
}
