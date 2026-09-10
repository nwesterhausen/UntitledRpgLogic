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
	/// 	The length of the side of a chunk in tiles (16).
	/// </summary>
	public const int ChunkSize = 16;

	/// <summary>
	/// 	The total number of tiles contained in a single 2D chunk (256 tiles).
	/// </summary>
	public const int TileCount = ChunkSize * ChunkSize;

	/// <summary>
	/// 	The packed uncompressed byte size used by a standard chunk: 256 tiles * 8 bytes = 2,048 bytes.
	/// </summary>
	public static readonly int RawByteSize = TileCount * Marshal.SizeOf<Tile2D>();

	/// <summary>
	///     Compresses a 16x16 tile array into a Brotli byte buffer for database persistence.
	/// </summary>
	/// <param name="tiles">Span containing exactly 256 <see cref="Tile2D"/> elements.</param>
	/// <param name="level">Brotli compression level. Defaults to <see cref="CompressionLevel.Fastest"/>.</param>
	/// <returns>A compressed byte array containing the Brotli payload.</returns>
	/// <exception cref="ArgumentException">Thrown when <paramref name="tiles"/> length is not equal to <see cref="TileCount"/>.</exception>
	public static byte[] SerializeAndCompress(ReadOnlySpan<Tile2D> tiles, CompressionLevel level = CompressionLevel.Fastest)
	{
		if (tiles.Length != TileCount)
		{
			throw new ArgumentException($"Tile array must contain exactly {TileCount} tiles (Length was {tiles.Length}).", nameof(tiles));
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
	/// <param name="compressedBlob">The compressed Brotli byte payload.</param>
	/// <param name="destination">Destination span that must accommodate exactly 256 <see cref="Tile2D"/> elements.</param>
	/// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> length is not equal to <see cref="TileCount"/>.</exception>
	/// <exception cref="InvalidDataException">Thrown if the decompressed payload is truncated or corrupted.</exception>
	public static void DecompressInto(ReadOnlySpan<byte> compressedBlob, Span<Tile2D> destination)
	{
		if (destination.Length != TileCount)
		{
			throw new ArgumentException($"Destination buffer must accommodate exactly {TileCount} tiles (Length was {destination.Length}).", nameof(destination));
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

		if (totalRead != RawByteSize)
		{
			throw new InvalidDataException($"Corrupt or truncated tile blob: expected {RawByteSize} uncompressed bytes, but decompressed {totalRead} bytes.");
		}
	}
}
