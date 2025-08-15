using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Configuration;

/// <summary>
///     Represents the configuration information for a module, including its identity, name, description, and version
///     details.
/// </summary>
/// <remarks>
///     This record is used to encapsulate the essential metadata for a module, which can be serialized or
///     deserialized using TOML configuration. It implements the <see cref="ITomlConfig" /> interface to ensure compatibility
///     with TOML-based configuration systems.
/// </remarks>
public record ModuleInfoConfig : ITomlConfig
{
	/// <summary>
	///     The name of the module.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	///     A description of the module, providing additional context or information about its purpose and functionality.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	///     A human-readable version of the module, typically used for display purposes.
	/// </summary>
	public string Version { get; set; } = "1.0.0";

	/// <summary>
	///     A numeric representation of the module's version, which can be used for comparisons, sorting, or dependency management.
	/// </summary>
	public int VersionNumber { get; set; } = 1;

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
	public ConfigType ConfigType => ConfigType.ModuleInfo;
}
