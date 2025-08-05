namespace UntitledRpgLogic.LibraryFile;

/// <summary>
///		Constants used throughout the URPG library file format library.
/// </summary>
public static class UrpgConstants
{
	/// <summary>
	/// The unique 8-byte signature for .urpglib files.
	/// Hex: BA 4E 57 7E 52 50 47 1A
	/// ASCII: ºNW~RPG.
	/// </summary>
	public static readonly byte[] MagicBytes = [0xBA, 0x4E, 0x57, 0x7E, 0x52, 0x50, 0x47, 0x1A];

	/// <summary>
	/// The current version of the header structure this library can write.
	/// </summary>
	public const byte CurrentHeaderSchemaVersion = 1;

	/// <summary>
	/// The current version of the manifest structure this library can write.
	/// </summary>
	public const ushort CurrentManifestSchemaVersion = 1;

	/// <summary>
	/// The current version of the payload data structure this library can write.
	/// </summary>
	public const ushort CurrentPayloadSchemaVersion = 1;
}
