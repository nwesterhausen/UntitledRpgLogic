using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the data for a material in the game. This is an immutable data container.
/// </summary>
public record Material : IMaterial
{
	/// <summary>
	/// 	An empty material instance.
	/// </summary>
	public static readonly IMaterial Empty = new Material(Name.Empty, Guid.Empty, MechanicalProperties.Empty, ThermalProperties.Empty, ElectricalProperties.Empty, FantasticalProperties.Empty, new Dictionary<StateOfMatter, StateSpecificProperties>());

	/// <inheritdoc />
	public Name Name { get; }
	/// <inheritdoc />
	public Guid Guid { get; }
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
	public string ShortGuid { get; init; } = string.Empty;


	private Material(Name name, Guid guid, MechanicalProperties mechanical, ThermalProperties thermal, ElectricalProperties electrical, FantasticalProperties fantastical, Dictionary<StateOfMatter, StateSpecificProperties> states)
	{
		this.Name = name;
		this.Guid = guid;
		this.Mechanical = mechanical;
		this.Thermal = thermal;
		this.Electrical = electrical;
		this.Fantastical = fantastical;
		this.States = states;

		this.Id = Convert.ToBase64String(this.Guid.ToByteArray());
		this.ShortGuid = this.Guid.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	/// 	Creates a Material instance from the provided configuration.
	/// </summary>
	/// <param name="config">material data config to use</param>
	/// <returns>a material instance</returns>
	public static Material FromConfig(MaterialDataConfig config)
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

		return new Material(name, config.ExplicitId ?? Guid.NewGuid(), mechanical, thermal, electrical, fantastical, states);
	}
}
