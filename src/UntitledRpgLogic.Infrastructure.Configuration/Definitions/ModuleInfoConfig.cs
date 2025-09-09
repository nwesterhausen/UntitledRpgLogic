namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Represents the configuration information for a module, including its identity, name, description, and version
///     details.
/// </summary>
/// <remarks>
///     This record is used to encapsulate the essential metadata for a module, which can be serialized or
///     deserialized using TOML configuration.
/// </remarks>
public record ModuleInfoConfig
{
  /// <summary>
  ///     The name of the module.
  /// </summary>
  public required string Name { get; init; }

  /// <summary>
  ///     A description of the module, providing additional context or information about its purpose and functionality.
  /// </summary>
  public string Description { get; init; } = string.Empty;

  /// <summary>
  ///     A human-readable version of the module, typically used for display purposes.
  /// </summary>
  public string Version { get; init; } = "1.0.0";

  /// <summary>
  ///     A numeric representation of the module's version, which can be used for comparisons, sorting, or dependency management.
  /// </summary>
  public int VersionNumber { get; init; } = 1;

  /// <summary>
  ///    The unique identifier for the module. If not provided, a new one will be generated.
  /// </summary>
  public Ulid Id { get; init; } = Ulid.NewUlid();
}
