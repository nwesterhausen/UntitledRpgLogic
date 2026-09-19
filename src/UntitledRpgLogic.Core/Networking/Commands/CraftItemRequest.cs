namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Client command requesting the crafting of an item using specified material components.
/// </summary>
/// <param name="ItemDefinitionId">The unique identifier of the item to craft.</param>
/// <param name="SlotMaterials">Mapping of material component slots to selected material definition identifiers.</param>
/// <param name="CustomName">Optional custom name applied to the crafted item.</param>
public record CraftItemRequest(Ulid ItemDefinitionId, Dictionary<byte, Ulid> SlotMaterials, string CustomName);
