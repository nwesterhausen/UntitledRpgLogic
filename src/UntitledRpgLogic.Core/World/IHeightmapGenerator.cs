namespace UntitledRpgLogic.Core.World;

/// <summary>
/// </summary>
public interface IHeightmapGenerator
{
	/// <summary>
	///     Populates a <see cref="TerrainHeightmap" /> with bedrock elevations, applying an ocean falloff mask if requested.
	/// </summary>
	public TerrainHeightmap Generate();
}
