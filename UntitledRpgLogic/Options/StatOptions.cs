using Microsoft.Extensions.Logging;

namespace UntitledRpgLogic.Options;

/// <summary>
///     Represents configuration options for a character stat, including its value range, name, and logging.
/// </summary>
public class StatOptions
{
    /// <summary>
    ///     Gets or sets the variation type of the stat, which may define how the stat behaves or changes.
    /// </summary>
    public StatVariation? Variation { get; set; }

    /// <summary>
    ///     Gets or sets the name of the stat (e.g., "Strength", "Agility").
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the maximum allowed value for the stat.
    /// </summary>
    public int? MaxValue { get; set; }

    /// <summary>
    ///     Gets or sets the current value of the stat.
    /// </summary>
    public int? Value { get; set; }

    /// <summary>
    ///     Gets or sets the minimum allowed value for the stat.
    /// </summary>
    public int? MinValue { get; set; }

    /// <summary>
    ///     Gets or sets the logger used for stat-related logging.
    /// </summary>
    public ILogger? Logger { get; set; }
}