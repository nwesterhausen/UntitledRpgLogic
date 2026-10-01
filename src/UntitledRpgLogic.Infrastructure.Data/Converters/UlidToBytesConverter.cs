using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UntitledRpgLogic.Infrastructure.Data.Converters;

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

/// <summary>
/// </summary>
public class UlidCollectionToBytesConverter : ValueConverter<ICollection<Ulid>, byte[]>
{
	/// <inheritdoc />
	public UlidCollectionToBytesConverter() : base(
		ulids => UlidBinaryPacker.Pack(ulids, ulids.Count),
		bytes => UlidBinaryPacker.Unpack(bytes))
	{
	}
}

/// <summary>
/// </summary>
public class UlidReadOnlyCollectionToBytesConverter : ValueConverter<IReadOnlyCollection<Ulid>, byte[]>
{
	/// <inheritdoc />
	public UlidReadOnlyCollectionToBytesConverter() : base(
		ulids => UlidBinaryPacker.Pack(ulids, ulids.Count),
		bytes => UlidBinaryPacker.Unpack(bytes))
	{
	}
}

internal static class UlidBinaryPacker
{
	public static byte[] Pack(IEnumerable<Ulid> ulids, int count)
	{
		if (count == 0)
		{
			return [];
		}

		var buffer = new byte[count * 16];
		var span = buffer.AsSpan();
		var offset = 0;

		foreach (var ulid in ulids)
		{
			ulid.TryWriteBytes(span.Slice(offset, 16));
			offset += 16;
		}

		return buffer;
	}

	public static List<Ulid> Unpack(byte[] bytes)
	{
		if (bytes.Length == 0)
		{
			return [];
		}

		var count = bytes.Length / 16;
		var result = new List<Ulid>(count);
		ReadOnlySpan<byte> span = bytes;

		for (var i = 0; i < count; i++)
		{
			result.Add(new Ulid(span.Slice(i * 16, 16)));
		}

		return result;
	}
}
