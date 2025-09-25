namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Properties specific to a material's state of matter.
/// </summary>
public record StateSpecificPropertiesConfig
{
	/// <summary>
	///     The color of the material in this state, represented as a hex string (e.g., "#FF0000").
	/// </summary>
	public string Color { get; init; } = "#FFFFFF";
}
