using System.IO.Compression;
using System.Runtime.InteropServices;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Helpers;

/// <summary>
///     High-performance zero-allocation binary serialization and Brotli compression helper for chunk tile grids.
/// </summary>
public static class ChunkBlobSerializer
{
	/// <summary>
	/// 	The length of the side of a chunk (in tiles)
	/// </summary>
	public const int ChunkSize = 16;
	/// <summary>
	/// 	The area of a chunk (in total tiles)
	/// </summary>
	public const int TileCount = ChunkSize * ChunkSize; // 256 tiles
	/// <summary>
	/// 	The packed byte size used by a defined chunk
	/// </summary>
	private static readonly int RawByteSize = TileCount * Marshal.SizeOf<Tile2D>(); // 2,048 bytes

	/// <summary>
	///     Compresses a 16x16 tile array into a Brotli byte buffer for database persistence.
	/// </summary>
	public static byte[] SerializeAndCompress(ReadOnlySpan<Tile2D> tiles, CompressionLevel level = CompressionLevel.Fastest)
	{
		if (tiles.Length != TileCount)
		{
			throw new ArgumentException($"Tile array must contain exactly {TileCount} tiles.", nameof(tiles));
		}

		ReadOnlySpan<byte> rawBytes = MemoryMarshal.AsBytes(tiles);

		using var outputStream = new MemoryStream();
		using (var brotli = new BrotliStream(outputStream, level, leaveOpen: true))
		{
			brotli.Write(rawBytes);
		}

		return outputStream.ToArray();
	}

	/// <summary>
	///     Decompresses a Brotli byte blob into an existing preallocated Tile2D destination buffer.
	/// </summary>
	public static void DecompressInto(ReadOnlySpan<byte> compressedBlob, Span<Tile2D> destination)
	{
		if (destination.Length != TileCount)
		{
			throw new ArgumentException($"Destination buffer must accommodate exactly {TileCount} tiles.", nameof(destination));
		}

		Span<byte> rawDestBytes = MemoryMarshal.AsBytes(destination);

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
	}
}
