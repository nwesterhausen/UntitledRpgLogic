namespace UntitledRpgLogic.Core.Events;

/// <summary>
/// Provides data for client connection events.
/// </summary>
public class ClientConnectionEventArgs : EventArgs
{
	/// <summary>
	/// Gets the unique identifier for the client.
	/// </summary>
	public Ulid ClientId { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ClientConnectionEventArgs"/> class.
	/// </summary>
	/// <param name="clientId">The unique identifier for the client.</param>
	public ClientConnectionEventArgs(Ulid clientId) => this.ClientId = clientId;
}
