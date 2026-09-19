using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation.GridMaps;

/// <summary>
///     Represents the the calculated moisture capacity of the world/map. Calculated/inferred from <see cref="HeightMap" />
///     ,
///     <see cref="TemperatureMap" />, <see cref="BasinMap" />, and <see cref="WindMap" />.
///     <br />
///     Moisture falls off at high elevations and cool temperatures. It recharges over warm water.
/// </summary>
public sealed class MoistureMap : NoiseGrid2D<float>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="MoistureMap" /> class with specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the MoistureMap in tiles.</param>
	/// <param name="heightTiles">The total height of the MoistureMap in tiles.</param>
	public MoistureMap(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="MoistureMap" /> class using an existing moisture-capacity array.
	/// </summary>
	/// <param name="widthTiles">The total width of the MoistureMap in tiles.</param>
	/// <param name="heightTiles">The total height of the MoistureMap in tiles.</param>
	/// <param name="moistureCapacities">The flattened moisture capacitys array of length <c>widthTiles * heightTiles</c>.</param>
	public MoistureMap(int widthTiles, int heightTiles, float[] moistureCapacities)
		: base(widthTiles, heightTiles, moistureCapacities)
	{
	}

	/// <summary>
	///     Retrieves the moisture capacity at the specified tile coordinate. Clamps coordinates to valid boundaries.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <returns>The moisture capacity value at the given coordinate.</returns>
	public float GetMoistureCapacity(int tileX, int tileY) => this.GetValueClamped(tileX, tileY);

	/// <summary>
	///     Sets the moisture capacity at the specified tile coordinate if within valid bounds.
	/// </summary>
	/// <param name="tileX">The horizontal tile index.</param>
	/// <param name="tileY">The vertical tile index.</param>
	/// <param name="capacity">The moisture capacity to assign.</param>
	public void SetMoistureCapacity(int tileX, int tileY, float capacity) => this.SetValue(tileX, tileY, capacity);
}
