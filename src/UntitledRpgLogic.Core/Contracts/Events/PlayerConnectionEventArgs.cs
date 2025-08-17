namespace UntitledRpgLogic.Core.Contracts.Events;

/// <summary>
///     Provides data for the PlayerConnected and PlayerDisconnected events.
/// </summary>
public class PlayerConnectionEventArgs : EventArgs
{
	/// <summary>
	///     Initializes a new instance of the <see cref="PlayerConnectionEventArgs" /> class.
	/// </summary>
	/// <param name="playerId">The unique identifier of the player.</param>
	public PlayerConnectionEventArgs(Guid playerId) => this.PlayerId = playerId;

	/// <summary>
	///     The unique identifier for the player who connected or disconnected.
	/// </summary>
	public Guid PlayerId { get; }
}
