using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UntitledRpgLogic.Infrastructure.Data.ValueConverters;

/// <summary>
///     Converts a collection of <see cref="Ulid" /> values to a contiguous packed byte array (16 bytes per item)
///     and back, using <see cref="UlidToBytesConverter" />.
/// </summary>
public class UlidCollectionToBytesConverter : ValueConverter<ICollection<Ulid>, byte[]>
{
	private static readonly UlidToBytesConverter SingleConverter = new();

	/// <summary>
	///     Converts a collection of <see cref="Ulid" /> values to a contiguous packed byte array (16 bytes per item)
	///     and back, using <see cref="UlidToBytesConverter" />.
	/// </summary>
	public UlidCollectionToBytesConverter() : base(
		ulids => PackUlids(ulids),
		bytes => UnpackUlids(bytes))
	{
	}

	private static byte[] PackUlids(ICollection<Ulid> ulids)
	{
		if (ulids == null || ulids.Count == 0)
		{
			return [];
		}

		var buffer = new byte[ulids.Count * 16];
		var index = 0;

		foreach (var ulid in ulids)
		{
			// Call the existing UlidToBytesConverter logic directly
			var bytes = (byte[])SingleConverter.ConvertToProvider(ulid)!;
			Buffer.BlockCopy(bytes, 0, buffer, index * 16, 16);
			index++;
		}

		return buffer;
	}

	private static List<Ulid> UnpackUlids(byte[] bytes)
	{
    if (bytes == null || bytes.Length == 0)
    {
        return new List<Ulid>();
    }

    var count = bytes.Length / 16;
    var result = new List<Ulid>(count);
    var slice = new byte[16];

    for (var i = 0; i < count; i++)
    {
        Buffer.BlockCopy(bytes, i * 16, slice, 0, 16);
        result.Add((Ulid)SingleConverter.ConvertFromProvider(slice)!);
    }

    return result;
	}
}
