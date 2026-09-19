using UntitledRpgLogic.Core.World.Generation.GridMaps;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Represents the mid-scale climate simulation layers (temperature, rainfall, and derived biomes) across 2D map space.
/// </summary>
public sealed class ClimateGrid : NoiseGridDimensions
{
	private readonly BiomeMap biomeMap;
	private readonly RainfallMap rainfallMap;
	private readonly TemperatureMap temperatureMap;

	/// <summary>
	///     Initializes a new instance of the <see cref="ClimateGrid" /> class with the specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the climate grid in tiles.</param>
	/// <param name="heightTiles">The total height of the climate grid in tiles.</param>
	public ClimateGrid(int widthTiles, int heightTiles)
		: base(widthTiles, heightTiles)
	{
		this.temperatureMap = new TemperatureMap(widthTiles, heightTiles);
		this.rainfallMap = new RainfallMap(widthTiles, heightTiles);
		this.biomeMap = new BiomeMap(widthTiles, heightTiles);
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
