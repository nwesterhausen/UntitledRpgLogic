using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Configuration;
/// <summary>
/// Represents the configuration information for a module, including its identity, name, description, and version
/// details.
/// </summary>
/// <remarks>This record is used to encapsulate the essential metadata for a module, which can be serialized or
/// deserialized using TOML configuration. It implements the <see cref="ITomlConfig"/> interface to ensure compatibility
/// with TOML-based configuration systems.</remarks>
public record ModuleInfoConfig : ITomlConfig
{
    /// <summary>
    ///     The unique identifier for the module.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
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

    /// <inheritdoc />
    public ConfigType ConfigType => ConfigType.ModuleInfo;
}
