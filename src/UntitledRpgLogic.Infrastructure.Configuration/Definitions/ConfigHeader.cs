namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     A simple DTO used for the first pass of deserialization to determine
///     the type of the configuration file.
/// </summary>
public record ConfigHeader
{
	/// <summary>
	///     Gets the type of the configuration object defined in the file.
	///     This property MUST exist in every TOML config file.
	/// </summary>
	public ConfigType Type { get; init; }
}
