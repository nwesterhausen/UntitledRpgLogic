using System.Collections.Concurrent;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.WorldGen.Generators;

namespace UntitledRpgLogic.WorldGen;

/// <summary>
///     In-memory cache and builder for macro simulation contexts across active maps.
/// </summary>
public sealed class WorldGenContextProvider : IWorldGenContextProvider
{
	private readonly ConcurrentDictionary<Ulid, WorldGenContext> contexts = new();

	/// <inheritdoc />
	public WorldGenContext GetOrCreateContext(MapDefinition map, WorldMapConfiguration worldConfig)
	{
		ArgumentNullException.ThrowIfNull(map);

		return this.contexts.GetOrAdd(map.Id, _ =>
		{
			var mapping = new BiomeMaterialMapping();

			// 1. Generate macro heightmap
			var heightmap = HeightMapGenerator.Generate(worldConfig);

			// 2. Simulate ocean filling and river descent
			var hydrology = MacroHydrologyGenerator.Generate(heightmap, map.Seed, mapping.WaterMaterialId, worldConfig);

			// 3. Derive temperature, rainfall, and Whittaker biomes
			var climate = MacroClimateGenerator.Generate(heightmap, hydrology, map.Seed, worldConfig);

			return new WorldGenContext(heightmap, hydrology, climate, mapping, map.Seed);
		});
	}
}
