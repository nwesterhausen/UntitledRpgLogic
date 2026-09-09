namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Value object describing mystical, mana, and elemental conductivity characteristics of a material.
/// </summary>
/// <remarks>Owned by <see cref="MaterialDefinition" />.</remarks>
public record FantasticalProperties
{
	/// <summary>
	///     Initializes a new default instance of the <see cref="FantasticalProperties" /> record.
	/// </summary>
	public FantasticalProperties()
	{
	}

	/// <summary>
	///     Relative measure of how efficiently the material channels raw magical energy (0.0 = mana insulator).
	/// </summary>
	public float AetherialConductivity { get; init; }

	/// <summary>
	///     Natural resonance or affinity for elemental forces, keyed by the <see cref="Element.Id" />.
	/// </summary>
	public Dictionary<Ulid, float> ElementalAttunement { get; init; } = new();

	/// <summary>
	///     Total reservoir capacity of unspent magical energy this material can hold before releasing or burning out.
	/// </summary>
	public float ManaCapacity { get; init; }

	/// <summary>
	///     Metaphysical alignment scale (-1.0 to 1.0; negative indicates corruption/decay, positive indicates sacred/pure).
	/// </summary>
	public float Purity { get; init; }

	/// <summary>
	///     Natural phosphorescence or light emission rate (negative values represent light absorption / darkness).
	/// </summary>
	public float Luminosity { get; init; }
}
