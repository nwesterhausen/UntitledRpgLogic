using System.ComponentModel.DataAnnotations;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Describes the fantastical properties of a material.
/// </summary>
public record FantasticalProperties
{
	/// <summary>
	///		Default constructor.
	/// </summary>
	public FantasticalProperties()
	{}

	/// <summary>
	///     A relative measure of how well the material channels magical energy.
	/// </summary>
	/// <remarks>
	///     The default value is 0.0, indicating no magical conductivity.
	/// </remarks>
	public float AetherialConductivity { get; init; }

	/// <summary>
	///     A material's natural affinity for one or more elemental types, referenced by the Element's Guid.
	/// </summary>
	public Dictionary<Ulid, float> ElementalAttunement { get; init; } = new();

	/// <summary>
	///     A relative measure of how much magical energy a material can store.
	/// </summary>
	/// <remarks>
	///     The default value is 0.0, indicating no mana capacity.
	/// </remarks>
	public float ManaCapacity { get; init; }

	/// <summary>
	///     A scale representing a material's alignment.
	///     Negative values indicate corruption, positive values indicate purity.
	/// </summary>
	/// <remarks>
	///     The default value is 0.0, indicating neutrality.
	/// </remarks>
	public float Purity { get; init; }

	/// <summary>
	///     A relative measure of a material's natural light emission.
	/// </summary>
	/// <remarks>
	///     The default value is 0.0, indicating no luminosity. A negative value indicates light absorption.
	/// </remarks>
	public float Luminosity { get; init; }

	/// <summary>
	///		A unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; init; }
}
