using System.Runtime.InteropServices;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.UnitTests.Core;

[TestClass]
public class ChunkBlobSerializerTests
{
	[TestMethod]
	public void StructSizeAndByteConstants_MatchEngineExpectations()
	{
		var structSize = Marshal.SizeOf<Tile2D>();

		Assert.AreEqual(8, structSize, "Tile2D must be packed to exactly 8 bytes.");
		Assert.AreEqual(2048, ChunkBlobSerializer.RawByteSize, "16x16 chunk raw byte footprint must equal 2,048 bytes.");
	}

	[TestMethod]
	public void SerializeAndDecompress_UniformChunk_RestoresIdenticalData()
	{
		var source = new Tile2D[ChunkBlobSerializer.TileCount];
		for (var i = 0; i < source.Length; i++)
		{
			source[i] = new Tile2D
			{
				Elevation = 100,
				LiquidDepth = 0,
				GroundPaletteIndex = 3,
				LiquidPaletteIndex = 0,
				TemperatureOffset = 15,
				Flags = TileTraits.None
			};
		}

		var compressed = ChunkBlobSerializer.SerializeAndCompress(source);

		Assert.IsLessThan(ChunkBlobSerializer.RawByteSize / 4, compressed.Length, "Brotli should compress uniform tiles to a fraction of raw size.");

		var destination = new Tile2D[ChunkBlobSerializer.TileCount];
		ChunkBlobSerializer.DecompressInto(compressed, destination);

		for (var i = 0; i < source.Length; i++)
		{
			Assert.AreEqual(source[i].Elevation, destination[i].Elevation);
			Assert.AreEqual(source[i].LiquidDepth, destination[i].LiquidDepth);
			Assert.AreEqual(source[i].GroundPaletteIndex, destination[i].GroundPaletteIndex);
			Assert.AreEqual(source[i].LiquidPaletteIndex, destination[i].LiquidPaletteIndex);
			Assert.AreEqual(source[i].TemperatureOffset, destination[i].TemperatureOffset);
			Assert.AreEqual(source[i].Flags, destination[i].Flags);
		}
	}

	[TestMethod]
	public void SerializeAndDecompress_HighEntropyHeterogeneousGrid_PreservesAllTileFields()
	{
		var source = new Tile2D[ChunkBlobSerializer.TileCount];
		for (var i = 0; i < source.Length; i++)
		{
			source[i] = new Tile2D
			{
				Elevation = (short)(short.MinValue + (i * 250)),
				LiquidDepth = (ushort)(i * 128),
				GroundPaletteIndex = (byte)(i % 256),
				LiquidPaletteIndex = (byte)((i * 3) % 256),
				TemperatureOffset = (sbyte)((i % 255) - 128),
				Flags = i % 2 == 0 ? (TileTraits.Impassable | TileTraits.IsConstructed) : TileTraits.IsBurning
			};
		}

		var compressed = ChunkBlobSerializer.SerializeAndCompress(source);
		var destination = new Tile2D[ChunkBlobSerializer.TileCount];

		ChunkBlobSerializer.DecompressInto(compressed, destination);

		CollectionAssert.AreEqual(source, destination);
	}

	[TestMethod]
	public void Serialize_MismatchedSpanLength_ThrowsArgumentException()
	{
		var invalidSpan = new Tile2D[128];

		Assert.Throws<ArgumentException>(() =>
			ChunkBlobSerializer.SerializeAndCompress(invalidSpan));
	}

	[TestMethod]
	public void Decompress_MismatchedDestinationLength_ThrowsArgumentException()
	{
		var validTiles = new Tile2D[ChunkBlobSerializer.TileCount];
		var compressed = ChunkBlobSerializer.SerializeAndCompress(validTiles);

		var invalidDestination = new Tile2D[100];

		Assert.Throws<ArgumentException>(() =>
			ChunkBlobSerializer.DecompressInto(compressed, invalidDestination));
	}

	[TestMethod]
	public void Decompress_TruncatedStream_ThrowsInvalidDataException()
	{
		var validTiles = new Tile2D[ChunkBlobSerializer.TileCount];
		var compressed = ChunkBlobSerializer.SerializeAndCompress(validTiles);

		var truncated = compressed.AsSpan(0, compressed.Length / 2).ToArray();
		var destination = new Tile2D[ChunkBlobSerializer.TileCount];

		Assert.Throws<InvalidDataException>(() =>
			ChunkBlobSerializer.DecompressInto(truncated, destination));
	}
}
