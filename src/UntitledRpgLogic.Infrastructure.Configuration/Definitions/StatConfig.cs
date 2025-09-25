using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Configuration for a stat.
/// </summary>
public record StatConfig
{
    /// <summary>
    ///     Items will always have a name. This is required.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    ///     A short description of the item. This is optional and can be used to provide additional context or flavor text
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    ///     Optionally defines the maximum value the stat can reach. This is used to limit the stat's value and prevent it from
    ///     exceeding a certain threshold.
    /// </summary>
    public int MaxValue { get; init; } = int.MaxValue;

    /// <summary>
    ///     Optionally defines the minimum value the stat can have. Only useful if the stat should be starting at a value above
    ///     zero that it cannot drop below.
    /// </summary>
    public int MinValue { get; init; }

    /// <summary>
    ///     What kind of stat is it?
    /// </summary>
    public StatVariation? Variation { get; init; } = StatVariation.None;

    /// <summary>
    ///     The unique identifier for the stat. If not provided, a new one will be generated.
    /// </summary>
    public Ulid Id { get; init; } = Ulid.NewUlid();
}
