using System.IO.Compression;
using System.Runtime.InteropServices;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     High-performance zero-allocation binary serialization and Brotli compression
///     extensions for tile spans and chunk blobs.
/// </summary>
public static class ChunkBlobExtensions
{
	/// <summary>
	///     Amount of tiles on one side of a chunk.
	/// </summary>
	public const int ChunkSize = 16;

	/// <summary>
	///     Total count of tiles inside one chunk.
	/// </summary>
	public const int TileCount = ChunkSize * ChunkSize; // 256 tiles

	/// <summary>
	///     The raw byte size used by a chunk of <see cref="Tile2D" />.
	/// </summary>
	public static readonly int RawByteSize = TileCount * Marshal.SizeOf<Tile2D>(); // 2,048 bytes

	/// <summary>
	///     Compresses a 16x16 tile span into a Brotli-compressed byte array for persistence.
	/// </summary>
	public static byte[] CompressTiles(
		this ReadOnlySpan<Tile2D> tiles,
		CompressionLevel level = CompressionLevel.Fastest)
	{
		if (tiles.Length != TileCount)
		{
			throw new ArgumentException(
				$"Tile span must contain exactly {TileCount} tiles (Length was {tiles.Length}).",
				nameof(tiles));
		}

		var rawBytes = MemoryMarshal.AsBytes(tiles);

		using var outputStream = new MemoryStream();
		using (var brotli = new BrotliStream(outputStream, level, true))
		{
			brotli.Write(rawBytes);
		}

		return outputStream.ToArray();
	}

	/// <summary>
	///     Decompresses a Brotli byte buffer directly into a destination Tile2D span.
	/// </summary>
	public static void DecompressTilesInto(
		this ReadOnlySpan<byte> compressedBlob,
		Span<Tile2D> destination)
	{
		if (destination.Length != TileCount)
		{
			throw new ArgumentException(
				$"Destination buffer must accommodate exactly {TileCount} tiles.",
				nameof(destination));
		}

		var rawDestBytes = MemoryMarshal.AsBytes(destination);

		using var inputStream = new MemoryStream(compressedBlob.ToArray());
		using var brotli = new BrotliStream(inputStream, CompressionMode.Decompress);

		var totalRead = 0;
		while (totalRead < RawByteSize)
		{
			var bytesRead = brotli.Read(rawDestBytes[totalRead..]);
			if (bytesRead == 0)
			{
				break;
			}

			totalRead += bytesRead;
		}

		if (totalRead != RawByteSize)
		{
			throw new InvalidDataException(
				$"Corrupt or truncated tile blob: expected {RawByteSize} uncompressed bytes, but decompressed {totalRead} bytes.");
		}
	}

	/// <summary>
	///     Convenience overload allowing decompressing directly from byte arrays on entities.
	/// </summary>
	public static void DecompressTilesInto(
		this byte[] compressedBlob,
		Span<Tile2D> destination) =>
		((ReadOnlySpan<byte>)compressedBlob).DecompressTilesInto(destination);
}
