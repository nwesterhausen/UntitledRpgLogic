namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Client command requesting activation of an ability targeting one or more entities.
/// </summary>
/// <param name="AbilityId">The unique identifier of the ability to cast.</param>
/// <param name="TargetEntityIds">The collection of target entity identifiers.</param>
public record CastAbilityCommand(Ulid AbilityId, IReadOnlyCollection<Ulid> TargetEntityIds);
