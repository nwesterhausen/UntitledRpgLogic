using System.Globalization;

namespace UntitledRpgLogic.Core;

/// <summary>
///     A collection of reserved GUIDs used throughout the game system. These GUIDs are reserved for specific purposes
///     like indicating an item is system-generated, etc.
/// </summary>
public static class WellKnownIdentifiers
{
	/// <summary>
	///     A reserved GUID for the GameSystem. This is used to identify the game system itself and should not be used for any
	///     other purpose.
	/// </summary>
	public static readonly Ulid GameSystem = Ulid.Parse("01K2MHERM20K401ENRX9JR472K", CultureInfo.InvariantCulture);

	/// <summary>
	///     A reserved GUID (as ULID) for a universal stat representing character / player level. This enables some canonical checks
	/// 	for meeting player-level requirements.
	/// </summary>
	public static readonly Ulid PlayerLevel = Ulid.Parse("01M23K96GWXPA2R3ZKN5QM9DX9", CultureInfo.InvariantCulture);
}
