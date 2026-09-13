using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.WorldGen.Models;

/// <summary>
///     Represents the mid-scale climate simulation layers (temperature, rainfall, and derived biomes) across 2D map space.
/// </summary>
public class TerrainClimate
{
	private readonly BiomeType[] biomes;
	private readonly float[] rainfall;
	private readonly float[] temperatures;

	/// <summary>
	///     Initializes a new instance of the <see cref="TerrainClimate" /> class with the specified grid dimensions.
	/// </summary>
	/// <param name="widthTiles">The total width of the climate grid in tiles.</param>
	/// <param name="heightTiles">The total height of the climate grid in tiles.</param>
	public TerrainClimate(int widthTiles, int heightTiles)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(widthTiles);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heightTiles);

		var totalSize = widthTiles * heightTiles;
		this.WidthTiles = widthTiles;
		this.HeightTiles = heightTiles;
		this.temperatures = new float[totalSize];
		this.rainfall = new float[totalSize];
		this.biomes = new BiomeType[totalSize];
	}

	/// <summary>
	///     Gets the total horizontal dimension of the climate grid in tiles.
	/// </summary>
	public int WidthTiles { get; }

	/// <summary>
	///     Gets the total vertical dimension of the climate grid in tiles.
	/// </summary>
	public int HeightTiles { get; }

	/// <summary>
	///     Retrieves the ambient temperature in degrees Celsius at the specified tile coordinate.
	/// </summary>
	public float GetTemperature(int tileX, int tileY)
	{
		var clampedX = Math.Clamp(tileX, 0, this.WidthTiles - 1);
		var clampedY = Math.Clamp(tileY, 0, this.HeightTiles - 1);
		return this.temperatures[(clampedY * this.WidthTiles) + clampedX];
	}

	/// <summary>
	///     Retrieves the normalized rainfall level (0.0 to 1.0) at the specified tile coordinate.
	/// </summary>
	public float GetRainfall(int tileX, int tileY)
	{
		var clampedX = Math.Clamp(tileX, 0, this.WidthTiles - 1);
		var clampedY = Math.Clamp(tileY, 0, this.HeightTiles - 1);
		return this.rainfall[(clampedY * this.WidthTiles) + clampedX];
	}

	/// <summary>
	///     Retrieves the derived biome at the specified tile coordinate.
	/// </summary>
	public BiomeType GetBiome(int tileX, int tileY)
	{
		var clampedX = Math.Clamp(tileX, 0, this.WidthTiles - 1);
		var clampedY = Math.Clamp(tileY, 0, this.HeightTiles - 1);
		return this.biomes[(clampedY * this.WidthTiles) + clampedX];
	}

	/// <summary>
	///     Sets the temperature, rainfall, and derived biome at the specified tile coordinate.
	/// </summary>
	public void SetCell(int tileX, int tileY, float temperature, float rain, BiomeType biome)
	{
		if (tileX >= 0 && tileX < this.WidthTiles && tileY >= 0 && tileY < this.HeightTiles)
		{
			var idx = (tileY * this.WidthTiles) + tileX;
			this.temperatures[idx] = temperature;
			this.rainfall[idx] = Math.Clamp(rain, 0.0f, 1.0f);
			this.biomes[idx] = biome;
		}
	}
}
