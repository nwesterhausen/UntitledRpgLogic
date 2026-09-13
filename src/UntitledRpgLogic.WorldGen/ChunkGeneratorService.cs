using UntitledRpgLogic.Core.Environment;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Extensions.Common;
using UntitledRpgLogic.WorldGen.Models;

namespace UntitledRpgLogic.WorldGen.Services;

/// <summary>
///     Pure domain service synthesizing individual 16x16 world chunks from macro simulation context.
/// </summary>
public sealed class ChunkGeneratorService : IChunkGeneratorService
{
	private const int ChunkSize = 16;

	/// <inheritdoc />
	public WorldChunk GenerateChunk(Ulid mapId, int chunkX, int chunkY, WorldGenContext context)
	{
		ArgumentNullException.ThrowIfNull(context);

		Span<Tile2D> tiles = stackalloc Tile2D[ChunkBlobExtensions.TileCount];
		var paletteList = new List<Ulid>();
		var startTileX = chunkX * ChunkSize;
		var startTileY = chunkY * ChunkSize;

		var totalTemp = 0.0f;
		var totalRain = 0.0f;

		for (var ly = 0; ly < ChunkSize; ly++)
		{
			for (var lx = 0; lx < ChunkSize; lx++)
			{
				var wx = startTileX + lx;
				var wy = startTileY + ly;
				var tileIdx = (ly * ChunkSize) + lx;

				var elevation = context.Heightmap.GetElevation(wx, wy);
				var liquidDepth = context.Hydrology.GetLiquidDepth(wx, wy);
				var liquidMaterial = context.Hydrology.GetLiquidMaterial(wx, wy) ?? Ulid.Empty;
				var biome = context.Climate.GetBiome(wx, wy);
				var temp = context.Climate.GetTemperature(wx, wy);
				var rain = context.Climate.GetRainfall(wx, wy);

				totalTemp += temp;
				totalRain += rain;

				// 1. Resolve ground material
				var groundMaterialId = context.MaterialMapping.ResolveGroundMaterial(biome);
				var groundPaletteIdx = GetOrAddPaletteIndex(paletteList, groundMaterialId);

				// 2. Resolve liquid material
				byte liquidPaletteIdx = 0;
				if (liquidDepth > 0 && liquidMaterial != Ulid.Empty)
				{
					liquidPaletteIdx = GetOrAddPaletteIndex(paletteList, liquidMaterial);
				}

				// 3. Flags
				var flags = TileTraits.None;
				if (liquidDepth > 100 || (biome == BiomeType.Mountain && elevation > 3000))
				{
					flags |= TileTraits.Impassable;
				}

				tiles[tileIdx] = new Tile2D
				{
					Elevation = elevation,
					LiquidDepth = liquidDepth,
					GroundPaletteIndex = groundPaletteIdx,
					LiquidPaletteIndex = liquidPaletteIdx,
					TemperatureOffset = 0,
					Flags = flags
				};
			}
		}

		var avgTemp = totalTemp / ChunkBlobExtensions.TileCount;
		var avgRain = totalRain / ChunkBlobExtensions.TileCount;

		return new WorldChunk
		{
			Id = Ulid.NewUlid(),
			MapId = mapId,
			ChunkX = chunkX,
			ChunkY = chunkY,
			MaterialPalette = paletteList,
			CompressedTileBlob = tiles.CompressTiles(),
			AmbientOverrides =
			[
				new AmbientValue(AmbientType.Temperature, avgTemp),
				new AmbientValue(AmbientType.Precipitation, avgRain)
			],
			Version = 1
		};
	}

	private static byte GetOrAddPaletteIndex(List<Ulid> palette, Ulid materialId)
	{
		var existing = palette.IndexOf(materialId);
		if (existing >= 0)
		{
			return (byte)existing;
		}

		if (palette.Count >= 255)
		{
			throw new InvalidOperationException("Chunk exceeds the 255 unique material limit.");
		}

		palette.Add(materialId);
		return (byte)(palette.Count - 1);
	}
}
