using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Core.World.Generation;
using UntitledRpgLogic.Core.World.Generation.NoiseMaps;

namespace UntitledRpgLogic.WorldGen.Noise;

/// <summary>
///     An API for generating layered noise.
/// </summary>
public class NoiseMaker
{
	public static float GenerateSimpleNoise(float x, float y, long seed) =>
		OpenSimplex2.Noise2(seed, x, y);

	/// <summary>
	///     Generates an octave-summed noise value for the given settings.
	/// </summary>
	/// <param name="settings">The sampling configuration parameters.</param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns>The accumulated noise value.</returns>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="settings" /> is null.</exception>
	public static float GenerateNoise(NoiseSettings settings, float x = 0f, float y = 0f)
	{
		ArgumentNullException.ThrowIfNull(settings);

		// Guarding against a divide by zero in the noise generation.
		if (settings.Scale <= 0f)
		{
			settings = settings with { Scale = NoiseSettings.DefaultScale };
		}

		var amplitude = settings.Amplitude;
		var frequency = settings.Frequency;
		var noiseHeight = 0f;

		for (var i = 0; i < settings.Octaves; i++)
		{
			var sampleX = x / settings.Scale * frequency;
			var sampleY = y / settings.Scale * frequency;

			var noiseValue = (OpenSimplex2.Noise2(settings.Seed, sampleX, sampleY) * 2) - 1;
			noiseHeight += noiseValue * amplitude;

			amplitude *= settings.Persistence;
			frequency *= settings.Lacunarity;
		}

		return noiseHeight;
	}

	/// <summary>
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="seed"></param>
	/// <param name="scale"></param>
	/// <param name="octaves"></param>
	/// <param name="amplitude"></param>
	/// <param name="frequency"></param>
	/// <param name="persistence"></param>
	/// <param name="lacunarity"></param>
	/// <returns></returns>
	public static float GenerateNoise(float x, float y, long seed, float scale, int octaves, float amplitude,
		float frequency,
		float persistence, float lacunarity)
	{
		// Guarding against a divide by zero in the noise generation.
		if (scale <= 0f)
		{
			scale = NoiseSettings.DefaultScale;
		}

		var noiseHeight = 0f;

		for (var i = 0; i < octaves; i++)
		{
			var sampleX = x / scale * frequency;
			var sampleY = y / scale * frequency;

			var noiseValue = (OpenSimplex2.Noise2(seed, sampleX, sampleY) * 2) - 1;
			noiseHeight += noiseValue * amplitude;

			amplitude *= persistence;
			frequency *= lacunarity;
		}

		return noiseHeight;
	}

	/// <summary>
	///     Generates a 2D <see cref="NoiseMap" /> with all values normalized to the range [0.0, 1.0].
	/// </summary>
	/// <param name="settings">The base noise configuration.</param>
	/// <param name="width">The horizontal dimension of the map in tiles.</param>
	/// <param name="height">The vertical dimension of the map in tiles.</param>
	/// <returns>A populated, normalized <see cref="NoiseMap" />.</returns>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="settings" /> is null.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if width or height is less than or equal to zero.</exception>
	public static NoiseMap GenerateNoiseMap(NoiseSettings settings, int width, int height) =>
		new(width, height, GenerateNoiseArray(settings, width, height));

	/// <summary>
	///     Generates a flattened 1D array of noise normalized to the settings' TargetMin and TargetMax.
	/// </summary>
	public static float[] GenerateNoiseArray(NoiseSettings settings, int width, int height)
	{
		ArgumentNullException.ThrowIfNull(settings);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

		var totalSize = width * height;
		var noiseArray = new float[totalSize];

		var maxNoiseHeight = float.MinValue;
		var minNoiseHeight = float.MaxValue;

		var scale = settings.Scale <= 0f ? NoiseSettings.DefaultScale : settings.Scale;

		for (var y = 0; y < height; y++)
		{
			var rowOffset = y * width;
			for (var x = 0; x < width; x++)
			{
				// Avoid copying NoiseSettings each iteration because that's a lot of unnecessary objects for just changing x,y
				var noise = GenerateNoise(x, y, settings.Seed,
					scale, settings.Octaves, settings.Amplitude,
					settings.Frequency, settings.Persistence, settings.Lacunarity);

				if (noise > maxNoiseHeight)
				{
					maxNoiseHeight = noise;
				}
				else if (noise < minNoiseHeight)
				{
					minNoiseHeight = noise;
				}

				noiseArray[rowOffset + x] = noise;
			}
		}

		var range = maxNoiseHeight - minNoiseHeight;
		var targetRange = settings.TargetMax - settings.TargetMin;

		for (var i = 0; i < totalSize; i++)
		{
			var normalizedNoise = range > 0.00001f
				? (noiseArray[i] - minNoiseHeight) / range
				: 0.5f;
			noiseArray[i] = settings.TargetMin + (normalizedNoise * targetRange);
		}

		return noiseArray;
	}
}
