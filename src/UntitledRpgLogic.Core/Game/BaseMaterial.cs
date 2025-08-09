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
	private BaseMaterial(Name name, Guid guid, MechanicalProperties mechanical, ThermalProperties thermal, ElectricalProperties electrical,
		FantasticalProperties fantastical, Dictionary<StateOfMatter, StateSpecificProperties> states)
	{
		this.Name = name;
		this.Identifier = guid;
		this.Mechanical = mechanical;
		this.Thermal = thermal;
		this.Electrical = electrical;
		this.Fantastical = fantastical;
		this.States = states;
	}

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public Guid Identifier { get; init; }

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

	/// <summary>
	///     Creates a Material instance from the provided configuration.
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

		var explicitId = config.Id == Guid.Empty
			? Guid.NewGuid()
			: config.Id;

		return new BaseMaterial(name, explicitId, mechanical, thermal, electrical, fantastical, states);
	}
}
