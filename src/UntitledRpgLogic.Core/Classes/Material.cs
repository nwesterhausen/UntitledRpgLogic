using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the data for a material in the game. This is an immutable data container.
/// </summary>
public record Material : IMaterial
{
	/// <summary>
	///     An empty material instance.
	/// </summary>
	public static readonly IMaterial Empty = new Material(Name.Empty, Ulid.NewUlid(), MechanicalProperties.Empty, ThermalProperties.Empty,
		ElectricalProperties.Empty, FantasticalProperties.Empty, new Dictionary<StateOfMatter, StateSpecificProperties>());


	private Material(Name name, Ulid identifier, MechanicalProperties mechanical, ThermalProperties thermal, ElectricalProperties electrical,
		FantasticalProperties fantastical, Dictionary<StateOfMatter, StateSpecificProperties> states)
	{
		this.Name = name;
		this.Identifier = identifier;
		this.Mechanical = mechanical;
		this.Thermal = thermal;
		this.Electrical = electrical;
		this.Fantastical = fantastical;
		this.States = states;

		this.Id = this.Identifier.ToString();
		this.ShortId = this.Identifier.ToGuid().ToString("N")[..8].ToUpperInvariant();
	}


	/// <inheritdoc />
	public string Id { get; init; } = string.Empty;

	/// <inheritdoc />
	public string ShortId { get; init; } = string.Empty;

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public Ulid Identifier { get; }

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

		return new Material(name, config.Identifier, mechanical, thermal, electrical, fantastical, states);
	}
}
