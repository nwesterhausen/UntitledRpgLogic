namespace UntitledRpgLogic.Core.Options;

/// <summary>
///     Provides parameters for a healing action.
/// </summary>
/// <param name="BaseAmount">The base number of points to heal.</param>
/// <param name="SourceId">The unique identifier of the entity or effect causing the heal.</param>
public record HealOptions(int BaseAmount, Ulid SourceId);
