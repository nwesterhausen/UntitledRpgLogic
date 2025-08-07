using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Game;

/// <summary>
///     A base class for materials in the game. This class implements the <see cref="IMaterial" /> interface and provides
///     boilerplate for a lot of the common functionality that materials will need.
/// </summary>
public class BaseMaterial : IMaterial
{
	/// <inheritdoc />
	public Name Name { get; }
	/// <inheritdoc />
	public Guid Identifier { get; }
	/// <inheritdoc />
	public MechanicalProperties Mechanical { get; }
	/// <inheritdoc />
	public ThermalProperties Thermal { get; }
	/// <inheritdoc />
	public ElectricalProperties Electrical { get; }
	/// <inheritdoc />
	public FantasticalProperties Fantastical { get; }
	/// <inheritdoc />
	public IReadOnlyDictionary<StateOfMatter, StateSpecificProperties> States { get; }


	/// <inheritdoc />
	public string Id { get; init; } = string.Empty;

	/// <inheritdoc />
	public string ShortId { get; init; } = string.Empty;


	private BaseMaterial(Name name, Guid guid, MechanicalProperties mechanical, ThermalProperties thermal, ElectricalProperties electrical, FantasticalProperties fantastical, Dictionary<StateOfMatter, StateSpecificProperties> states)
	{
		this.Name = name;
		this.Identifier = guid;
		this.Mechanical = mechanical;
		this.Thermal = thermal;
		this.Electrical = electrical;
		this.Fantastical = fantastical;
		this.States = states;

		this.Id = Convert.ToBase64String(this.Identifier.ToByteArray());
		this.ShortId = this.Identifier.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	/// 	Creates a Material instance from the provided configuration.
	/// </summary>
	/// <param name="config">material data config to use</param>
	/// <returns>a material instance</returns>
	public static BaseMaterial FromConfig(MaterialDataConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		var name = new Name(config.Name, config.PluralName, config.NameAsAdjective);
		var mechanical = MechanicalProperties.FromConfig(config.Mechanical);
		var thermal = ThermalProperties.FromConfig(config.Thermal);
		var electrical = ElectricalProperties.FromConfig(config.Electrical);
		var fantastical = FantasticalProperties.FromConfig(config.Fantastical);

		var states = new Dictionary<StateOfMatter, StateSpecificProperties>();
		foreach (var (state, stateConfig) in config.States)
		{
			states[state] = StateSpecificProperties.FromConfig(stateConfig);
		}

		return new BaseMaterial(name, config.ExplicitId ?? Guid.NewGuid(), mechanical, thermal, electrical, fantastical, states);
	}
}
