using System.Numerics;
using UntitledRpgLogic.Core.World.Generation.GridMaps;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
/// </summary>
public sealed class ClimateMaps : NoiseGridDimensions
{
	private readonly BiomeMap biome;
	private readonly RainfallMap rainfall;
	private readonly TemperatureMap temperature;
	private readonly ClimateTurbulenceMap turbulence;
	private readonly WindMap wind;

	/// <summary>
	/// </summary>
	/// <param name="widthTiles"></param>
	/// <param name="heightTiles"></param>
	public ClimateMaps(int widthTiles, int heightTiles) : base(widthTiles, heightTiles)
	{
		this.turbulence = new ClimateTurbulenceMap(widthTiles, heightTiles);
		this.wind = new WindMap(widthTiles, heightTiles);
		this.temperature = new TemperatureMap(widthTiles, heightTiles);
		this.rainfall = new RainfallMap(widthTiles, heightTiles);
		this.biome = new BiomeMap(widthTiles, heightTiles);
	}

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public BiomeType GetBiome(int tileX, int tileY) => this.biome.GetBiome(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetTurbulence(int tileX, int tileY) => this.turbulence.GetTurbulence(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public Vector2 GetWindVector(int tileX, int tileY) => this.wind.GetWindVector(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetWindHeading(int tileX, int tileY) => this.wind.GetWindHeading(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetWindVelocity(int tileX, int tileY) => this.wind.GetWindVelocity(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetTemperature(int tileX, int tileY) => this.temperature.GetTemperature(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <returns></returns>
	public float GetRainfall(int tileX, int tileY) => this.rainfall.GetRainfall(tileX, tileY);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetBiome(int tileX, int tileY, BiomeType value) => this.biome.SetBiome(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetTurbulence(int tileX, int tileY, float value) => this.turbulence.SetTurbulence(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="windVector"></param>
	public void SetWindVector(int tileX, int tileY, Vector2 windVector) =>
		this.wind.SetWindVector(tileX, tileY, windVector);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="windHeading"></param>
	/// <param name="windVelocity"></param>
	public void SetWind(int tileX, int tileY, float windHeading, float windVelocity) =>
		this.wind.SetWindVector(tileX, tileY, new Vector2(windHeading, windVelocity));

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetTemperature(int tileX, int tileY, float value) =>
		this.temperature.SetTemperature(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="value"></param>
	public void SetRainfall(int tileX, int tileY, float value) => this.rainfall.SetRainfall(tileX, tileY, value);

	/// <summary>
	/// </summary>
	/// <param name="tileX"></param>
	/// <param name="tileY"></param>
	/// <param name="temperatureValue"></param>
	/// <param name="rainfallValue"></param>
	/// <param name="biomeType"></param>
	public void SetClimate(int tileX, int tileY, float temperatureValue, float rainfallValue, BiomeType biomeType)
	{
		this.temperature.SetTemperature(tileX, tileY, temperatureValue);
		this.rainfall.SetRainfall(tileX, tileY, rainfallValue);
		this.biome.SetBiome(tileX, tileY, biomeType);
	}

	public void OverwriteTurbulenceMap(ClimateTurbulenceMap climateEnergyTurbulence)
	{
		ArgumentNullException.ThrowIfNull(climateEnergyTurbulence);
		this.turbulence.ReplaceCells(climateEnergyTurbulence.Cells);
	}
}
