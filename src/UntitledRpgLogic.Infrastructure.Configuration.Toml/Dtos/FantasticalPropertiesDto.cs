using UntitledRpgLogic.Core.Elements;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class FantasticalPropertiesDto
{
	/// <summary>
	///     Relative measure of how efficiently the material channels raw magical energy (0.0 = mana insulator).
	/// </summary>
	public float AetherialConductivity { get; init; }

	/// <summary>
	///     Natural resonance or affinity for elemental forces, keyed by the <see cref="ElementDefinition.Id" />.
	/// </summary>
	public Dictionary<Ulid, float> ElementalAttunement { get; init; } = [];

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

	public FantasticalProperties ToModel() => new()
	{
		AetherialConductivity = this.AetherialConductivity,
		ElementalAttunement = new Dictionary<Ulid, float>(this.ElementalAttunement),
		ManaCapacity = this.ManaCapacity,
		Purity = this.Purity,
		Luminosity = this.Luminosity
	};

	public static FantasticalPropertiesDto FromModel(FantasticalProperties model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new FantasticalPropertiesDto
		{
			AetherialConductivity = model.AetherialConductivity,
			ElementalAttunement = new Dictionary<Ulid, float>(model.ElementalAttunement),
			ManaCapacity = model.ManaCapacity,
			Purity = model.Purity,
			Luminosity = model.Luminosity
		};
	}
}
