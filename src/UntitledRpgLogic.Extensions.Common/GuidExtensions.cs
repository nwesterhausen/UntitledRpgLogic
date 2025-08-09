namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     A set of extension methods for the <see cref="Guid" /> type.
///     This provides additional functionality for working with Guids, such as generating short and hex identifiers
/// </summary>
public static class GuidExtensions
{
	/// <summary>
	///     A base-64 encoded string representation of the Guid.
	///     This value is derived from the Guid property.
	/// </summary>
	public static string HexId(this Guid id) => Convert.ToBase64String(id.ToByteArray());

	/// <summary>
	///     The first 8 characters of the guid, used as a short identifier.
	///     This value is derived from the Guid property.
	/// </summary>
	public static string ShortId(this Guid id) => id.ToString("N")[..8].ToUpperInvariant();
}
