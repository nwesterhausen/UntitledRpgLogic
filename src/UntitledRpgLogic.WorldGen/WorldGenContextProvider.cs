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
	public WorldGenContext GetOrCreateContext(MapDefinition map)
	{
		ArgumentNullException.ThrowIfNull(map);

		if (map.GenerationConfig == null)
		{
			throw new InvalidOperationException(
				"Map generation config is null, but required for a generation context.");
		}

		return this.contexts.GetOrAdd(map.Id, _ =>
		{
			var newContext = new WorldGenContext(map.GenerationConfig);

			/****** The world map generation flow. ********/
			// Tier 1, these can happen in parallel with the ReadOnly context
			var heightMap = HeightMapGenerator.Generate(newContext.AsReadOnly());
			var leylineMap = LeylineMapGenerator.Generate(newContext.AsReadOnly());
			var alignmentMap = AlignmentMapGenerator.Generate(newContext.AsReadOnly());
			var savageryMap = SavageryMapGenerator.Generate(newContext.AsReadOnly());
			var volcanismMap = VolcanismMapGenerator.Generate(newContext.AsReadOnly());

			// End of Tier 1, must update context with new maps
			newContext.Terrain.OverwriteHeightMap(heightMap);
			newContext.Terrain.OverwriteVolcanismMap(volcanismMap);
			newContext.Arcana.OverwriteLeylineMap(leylineMap);
			newContext.Arcana.OverwriteAlignmentMap(alignmentMap);
			newContext.Arcana.OverwriteSavageryMap(savageryMap);
			// Tier 1.5
			var blendedHeight = BlendedHeightMapGenerator.Generate(newContext.AsReadOnly());
			var climateEnergyTurbulence = ClimateTurbulenceMapGenerator.Generate(newContext.AsReadOnly());

			newContext.Terrain.OverwriteHeightMap(blendedHeight);
			newContext.Climate.OverwriteTurbulenceMap(climateEnergyTurbulence);

			var basinMap = BasinMapGenerator.Generate(newContext.AsReadOnly());
			newContext.Hydrology.OverwriteBasinMap(basinMap);
			// Tier 2

			// Tier 2.5

			// Tier 3

			// Final Processing

			// Map post-back
			// ! set map.OreDeposits with new OreDeposit definitions

			return newContext;
		});
	}
}
