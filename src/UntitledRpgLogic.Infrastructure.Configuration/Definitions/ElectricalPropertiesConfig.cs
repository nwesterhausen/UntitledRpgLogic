namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
/// Describes the electrical properties of a material.
/// </summary>
public record ElectricalPropertiesConfig
{
  /// <summary>
  ///     A relative measure of how well the material conducts electricity.
  ///     0 indicates a perfect insulator.
  /// </summary>
  /// <remarks>
  /// The default value is 0.25, indicating some conductivity.
  /// </remarks>
  public float Conductivity { get; init; } = 0.25f;
}
