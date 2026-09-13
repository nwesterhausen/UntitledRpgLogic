namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Client command requesting movement toward a target world position.
/// </summary>
/// <param name="EntityId">The unique identifier of the entity to move.</param>
/// <param name="TargetX">The desired destination horizontal coordinate.</param>
/// <param name="TargetY">The desired destination vertical coordinate.</param>
public record MoveEntityCommand(Ulid EntityId, float TargetX, float TargetY);
