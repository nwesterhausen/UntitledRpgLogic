using System.Collections.Concurrent;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Generators;

namespace UntitledRpgLogic.WorldGen;

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

		var noiseConfig = new HeightmapSettings();
		var hydroConfig = new HydrologySettings();

		var worldConfig = new WorldMapConfiguration
		{
			MaxElevation = 2000,
			MinElevation = -200,
			WidthTiles = widthTiles,
			HeightTiles = heightTiles,
			Heightmap = noiseConfig,
			Hydrology = hydroConfig
		};

		return this.contexts.GetOrAdd(map.Id, _ =>
		{
			var mapping = new BiomeMaterialMapping();

			// 1. Generate macro heightmap
			var heightmap = MacroHeightmapGenerator.Generate(map.Seed, worldConfig);

			// 2. Simulate ocean filling and river descent
			var hydrology = MacroHydrologyGenerator.Generate(heightmap, map.Seed, worldConfig, mapping.WaterMaterialId);

			// 3. Derive temperature, rainfall, and Whittaker biomes
			var climate = MacroClimateGenerator.Generate(heightmap, hydrology, map.Seed);

			return new WorldGenContext(heightmap, hydrology, climate, mapping, map.Seed);
		});
	}
}
