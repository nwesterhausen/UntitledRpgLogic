using UntitledRpgLogic.Core.Configuration;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents fantastical or magical properties of a material, such as aetherial conductivity, elemental attunement, and mana capacity.
/// </summary>
public record FantasticalProperties
{
	/// <summary>
	///     An empty instance of <see cref="FantasticalProperties" />.
	/// </summary>
	public static readonly FantasticalProperties Empty = new(null, null, null, null, null);

	private FantasticalProperties(float? aetherialConductivity, Dictionary<Ulid, float>? elementalAttunement, float? manaCapacity,
		float? purity, float? luminosity)
	{
		this.AetherialConductivity = aetherialConductivity;
		this.ElementalAttunement = elementalAttunement;
		this.ManaCapacity = manaCapacity;
		this.Purity = purity;
		this.Luminosity = luminosity;
	}

	/// <summary>
	///     Gets the aetherial conductivity of the material.
	/// </summary>
	public float? AetherialConductivity { get; }

	/// <summary>
	///     Gets the elemental attunement values for the material.
	/// </summary>
	public IReadOnlyDictionary<Ulid, float>? ElementalAttunement { get; }

	/// <summary>
	///     Gets the mana capacity of the material.
	/// </summary>
	public float? ManaCapacity { get; }

	/// <summary>
	///     Gets the purity of the material.
	/// </summary>
	public float? Purity { get; }

	/// <summary>
	///     Gets the luminosity of the material.
	/// </summary>
	public float? Luminosity { get; }

	/// <summary>
	///     Creates a <see cref="FantasticalProperties" /> instance from a configuration object.
	/// </summary>
	/// <param name="config">The configuration object containing fantastical property values.</param>
	/// <returns>A new <see cref="FantasticalProperties" /> instance.</returns>
	public static FantasticalProperties FromConfig(FantasticalPropertiesConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		return new FantasticalProperties(config.AetherialConductivity, config.ElementalAttunement, config.ManaCapacity, config.Purity,
			config.Luminosity);
	}
}
