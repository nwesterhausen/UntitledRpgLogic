using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     Configuration for a Stat in the RPG logic.
/// </summary>
public record StatDataConfig : ITomlConfig
{
	/// <summary>
	///     Items will always have a name. This is required.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     A short description of the item. This is optional and can be used to provide additional context or flavor text
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///     Optionally defines the maximum value the stat can reach. This is used to limit the stat's value and prevent it from
	///     exceeding a certain threshold.
	/// </summary>
	public int? MaxValue { get; set; }

	/// <summary>
	///     Optionally defines the minimum value the stat can have. Only useful if the stat should be starting at a value above
	///     zero that it cannot drop below.
	/// </summary>
	public int? MinValue { get; set; }

	/// <summary>
	///     What kind of stat is it?
	/// </summary>
	public StatVariation? Variation { get; set; }

	/// <summary>
	///     Optional. If provided in TOML, this specific Ulid will be used for the item; otherwise, a new one will be generated.
	/// </summary>
	/// <remarks>
	///     This allows for consistent referencing of stats across different configurations or systems.
	///     <br />
	///     If not provided, a new Ulid will be generated when the configuration is loaded. If this happens when
	///     validating a configuration pack, the Ulid will be persisted back to the source file before bundling.
	///     This ensures that every stat has a stable and unique identifier.
	/// </remarks>
	public Ulid Identifier { get; set; } = Ulid.NewUlid();

	/// <inheritdoc />
	public ConfigType ConfigType => ConfigType.Stat;
}
