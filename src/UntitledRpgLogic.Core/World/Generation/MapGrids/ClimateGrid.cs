using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation.MapGrids;

/// <summary>
///     Represents the mid-scale climate simulation layers (temperature, rainfall, and derived biomes) across 2D map space.
/// </summary>
public sealed class TerrainClimate : NoiseGridDimensions
{
	private readonly TerrainBiomeMap biomeMap;
	private readonly TerrainRainfallMap rainfallMap;
	private readonly TerrainTemperatureMap temperatureMap;

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainClimate" /> class with the specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the climate grid in tiles.</param>
	/// <param name="heightTiles">The total height of the climate grid in tiles.</param>
	public TerrainClimate(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
		this.temperatureMap = new TerrainTemperatureMap(widthTiles, heightTiles);
		this.rainfallMap = new TerrainRainfallMap(widthTiles, heightTiles);
		this.biomeMap = new TerrainBiomeMap(widthTiles, heightTiles);
	}

	/// <summary>
	///     Retrieves the ambient temperature in degrees Celsius at the specified tile coordinate.
	/// </summary>
	public float GetTemperature(int tileX, int tileY) => this.temperatureMap.GetTemperature(tileX, tileY);

	/// <summary>
	///     Retrieves the normalized rainfall level (0.0 to 1.0) at the specified tile coordinate.
	/// </summary>
	public float GetRainfall(int tileX, int tileY) => this.rainfallMap.GetRainfall(tileX, tileY);

	/// <summary>
	///     Retrieves the derived biome at the specified tile coordinate.
	/// </summary>
	public BiomeType GetBiome(int tileX, int tileY) => this.biomeMap.GetBiome(tileX, tileY);

	/// <summary>
	///     Sets the temperature, rainfall, and derived biome at the specified tile coordinate.
	/// </summary>
	public void SetCell(int tileX, int tileY, float temperature, float rain, BiomeType biome)
	{
		if (!this.IsInBounds(tileX, tileY))
		{
			throw new ArgumentOutOfRangeException(nameof(tileX), "Coordinates outside map bounds.");
		}

		this.temperatureMap.SetTemperature(tileX, tileY, temperature);
		this.rainfallMap.SetRainfall(tileX, tileY, rain);
		this.biomeMap.SetBiome(tileX, tileY, biome);
	}
}
