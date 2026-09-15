using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.World;

/// <summary>
/// </summary>
public record NoiseSettings
{
	/// <summary>
	///     A default scale for reasonable nice linear gradient noise generation.
	/// </summary>
	public const float DefaultScale = 48f;

	/// <summary>
	///     A default frequency that results in a no-op when doing complex noise generation.
	/// </summary>
	public const float DefaultFrequency = 1f;

	/// <summary>
	///     A default seed for when one isn't specified.
	/// </summary>
	public const long DefaultSeed = 1234567890123456L;

	/// <summary>
	///     The seed to use for noise generation.
	/// </summary>
	public long Seed { get; init; } = DefaultSeed;

	/// <summary>
	///     The scale affects how drastic the changes between points are.
	/// </summary>
	[Range(0f, float.MaxValue)]
	public float Scale { get; init; } = DefaultScale;

	/// <summary>
	///     Octaves are how many times to generate noise and layer it.
	/// </summary>
	public int Octaves { get; init; } = 1;

	/// <summary>
	///     Amplitude affects how much each consecutive octave affects the generation.
	/// </summary>
	public float Amplitude { get; init; } = 1f;

	/// <summary>
	///     Affects the change of scale for each consecutive octave.
	/// </summary>
	public float Frequency { get; init; } = DefaultFrequency;

	/// <summary>
	///     How rapidly the octave influence decreases (used with <see cref="Amplitude" />)
	/// </summary>
	public float Persistence { get; init; } = 1f;

	/// <summary>
	///     How rapidly the octaves increase in <see cref="Frequency" />
	/// </summary>
	public float Lacunarity { get; init; } = 1f;

	/// <summary>
	///     The lower bound for final map normalization. Defaults to 0.0f.
	/// </summary>
	public float TargetMin { get; init; }

	/// <summary>
	///     The upper bound for final map normalization. Defaults to 1.0f.
	/// </summary>
	public float TargetMax { get; init; } = 1.0f;
}
