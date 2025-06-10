namespace UntitledRpgLogic;

/// <summary>
///     A collection of reserved GUIDs used throughout the game system. These GUIDs are reserved for specific purposes
///     like indicating an item is system-generated, etc.
/// </summary>
public static class ReservedGuids
{
    /// <summary>
    ///     A reserved GUID for the GameSystem. This is used to identify the game system itself and should not be used for any
    ///     other purpose.
    /// </summary>
    public static readonly Guid GameSystem = new("00000000-0000-0000-0000-000000000001");

    /// <summary>
    ///     A reserved GUID for a "copper" material. This is a pre-defined basic materials in the game.
    /// </summary>
    public static readonly Guid MaterialCopper = new("00000000-0000-0000-0000-000000000002");
}