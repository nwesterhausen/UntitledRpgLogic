namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
/// Properties related to the fantastical or magical aspects of a material.
/// </summary>
public record FantasticalPropertiesConfig
{
	/// <summary>
	/// A relative measure of how well the material channels magical energy.
	/// </summary>
	public float? AetherialConductivity { get; set; }

	/// <summary>
	/// A material's natural affinity for one or more elemental types, referenced by the Element's Guid.
	/// </summary>
	public Dictionary<Ulid, float>? ElementalAttunement { get; init; }

	/// <summary>
	/// A relative measure of how much magical energy a material can store.
	/// </summary>
	public float? ManaCapacity { get; set; }

	/// <summary>
	/// A scale representing a material's alignment.
	/// Negative values indicate corruption, positive values indicate purity.
	/// </summary>
	public float? Purity { get; set; }

	/// <summary>
	/// A relative measure of a material's natural light emission.
	/// </summary>
	public float? Luminosity { get; set; }
}
