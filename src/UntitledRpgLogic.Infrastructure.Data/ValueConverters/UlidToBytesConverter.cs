using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UntitledRpgLogic.Infrastructure.Data.ValueConverters;

/// <summary>
///     A value converter that tells Entity Framework Core how to store <see cref="Ulid" /> properties in a database.
///     It converts <see cref="Ulid" /> to a byte array for storage and back again when reading.
/// </summary>
public class UlidToBytesConverter : ValueConverter<Ulid, byte[]>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="UlidToBytesConverter" /> class.
	/// </summary>
	public UlidToBytesConverter() : base(
		ulid => ulid.ToByteArray(),
		bytes => new Ulid(bytes))
	{
	}
}
