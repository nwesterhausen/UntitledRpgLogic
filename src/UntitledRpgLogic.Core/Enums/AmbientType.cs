namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Identifies a measurable environmental or atmospheric condition in the world.
/// </summary>
public enum AmbientType
{
	/// <summary>
	///     Represents an unassigned or undefined ambient type. Should not be used for valid data.
	/// </summary>
	None = 0,

	// --- Weather & Climate ---

	/// <summary>
	///     The relative moisture content of the air (typically expressed as a normalized ratio from 0.0 to 1.0).
	/// </summary>
	Humidity = 1,

	/// <summary>
	///     The active intensity of atmospheric precipitation such as rain, hail, or snowfall.
	/// </summary>
	Precipitation = 2,

	/// <summary>
	///     The ground or soil permeability determining the rate at which standing surface water absorbs into subterranean layers.
	/// </summary>
	Drainage = 3,

	/// <summary>
	///     The ambient temperature of the local environment (measured in degrees Celsius).
	/// </summary>
	Temperature = 4,

	// --- Kinematics & Forces ---

	/// <summary>
	///     The local gravitational acceleration constant affecting projectile trajectories, fall rates, and structural weight.
	/// </summary>
	Gravity = 10,

	/// <summary>
	///     The current velocity of airflow or wind currents.
	/// </summary>
	WindSpeed = 11,

	/// <summary>
	///     The compass heading or rotational bearing from which wind is actively blowing (in degrees).
	/// </summary>
	WindDirection = 12,

	// --- Optical & Sensory ---

	/// <summary>
	///     The direct unobstructed exposure to open sky and sunlight (ranging from subterranean darkness to clear midday sky).
	/// </summary>
	SunlightExposure = 20,

	/// <summary>
	///     The total aggregate ambient illumination at the location, accounting for indirect bounced lighting, artificial sources, and bioluminescence.
	/// </summary>
	AmbientLightLevel = 21,

	/// <summary>
	///     The ambient background sound amplitude, influencing stealth detection and acoustic awareness.
	/// </summary>
	AcousticNoise = 22,

	/// <summary>
	///     Physical seismic or structural vibrations propagated through solid ground or bedrock.
	/// </summary>
	Vibration = 23,

	// --- Fantastical ---

	/// <summary>
	///     The ambient concentration of raw aetherial energy available to fuel spellcasting or induce magical phenomena.
	/// </summary>
	ManaDensity = 30,

	/// <summary>
	///     The environmental resonance or alignment toward specific elemental planes (such as celestial fire, planar cold, or abyssal decay).
	/// </summary>
	ElementalAttunement = 31,
}
