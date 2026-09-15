using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
/// </summary>
public interface IHeightmapGenerator
{
	/// <summary>
	///     Populates a <see cref="Heightmap" /> with bedrock elevations, applying an ocean falloff mask if requested.
	/// </summary>
	public Heightmap Generate();
}
