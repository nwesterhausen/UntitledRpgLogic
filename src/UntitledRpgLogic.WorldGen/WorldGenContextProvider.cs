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
			// Tier 1
			HeightMapGenerator.Generate(newContext);

			// Tier 1.5

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
