using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     Defines a generic type, field, or school of magic (e.g., Fire, Time, Summoning).
/// </summary>
public class MagicTypeConfig : ITomlConfig
{
	/// <summary>
	///     The name of the magic type (e.g., "Fire", "Time", "Summoning"). This is required.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	///     A brief description of the magic type and its characteristics.
	/// </summary>
	public string Description { get; set; } = string.Empty;

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
	public ConfigType ConfigType => ConfigType.MagicType;
}
