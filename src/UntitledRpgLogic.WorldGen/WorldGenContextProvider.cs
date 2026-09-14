using System.Collections.Concurrent;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Generators;
using UntitledRpgLogic.WorldGen.Models;

namespace UntitledRpgLogic.WorldGen.Services;

/// <summary>
///     In-memory cache and builder for macro simulation contexts across active maps.
/// </summary>
public sealed class WorldGenContextProvider : IWorldGenContextProvider
{
	private readonly ConcurrentDictionary<Ulid, WorldGenContext> contexts = new();

	/// <inheritdoc />
	public WorldGenContext GetOrCreateContext(MapDefinition map, uint seed, int widthTiles = 512, int heightTiles = 512)
	{
		ArgumentNullException.ThrowIfNull(map);

		var mapConfig = new WorldMapConfiguration { WidthTiles = widthTiles, HeightTiles = heightTiles };
		var noiseConfig = new HeightmapSettings { MinElevation = -200, MaxElevation = 2000 };
		var hydroConfig = new HydrologySettings();

		return this.contexts.GetOrAdd(map.Id, _ =>
		{
			var mapping = new BiomeMaterialMapping();

			// 1. Generate macro heightmap
			var heightmap = MacroHeightmapGenerator.Generate(seed, mapConfig, noiseConfig);

			// 2. Simulate ocean filling and river descent
			var hydrology = MacroHydrologyGenerator.Generate(heightmap, mapping.WaterMaterialId, seed, hydroConfig);

			// 3. Derive temperature, rainfall, and Whittaker biomes
			var climate = MacroClimateGenerator.Generate(heightmap, hydrology, seed);

			return new WorldGenContext(heightmap, hydrology, climate, mapping, seed);
		});
	}
}
