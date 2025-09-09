namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

public record ElementConfig
{
  /// <summary>
  ///     The name of the element (e.g., "Fire", "Time", "Summoning"). This is required.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  ///     A brief description of the element and its characteristics.
  /// </summary>
  public string Description { get; init; } = string.Empty;

  /// <summary>
  /// The unique identifier for the element. If not provided, a new one will be generated.
  /// </summary>
  public Ulid Id { get; init; } = Ulid.NewUlid();
}
