namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Server payload synchronizing an entity's 2D world coordinates to subscribed clients.
/// </summary>
/// <param name="EntityId">The unique identifier of the moving entity.</param>
/// <param name="X">The new horizontal world position.</param>
/// <param name="Y">The new vertical world position.</param>
public record EntityMovedPayload(Ulid EntityId, float X, float Y);
