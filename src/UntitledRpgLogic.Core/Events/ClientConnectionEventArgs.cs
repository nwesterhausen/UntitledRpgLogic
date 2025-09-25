namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Provides data for client connection events.
/// </summary>
public class ClientConnectionEventArgs : EventArgs
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ClientConnectionEventArgs" /> class.
	/// </summary>
	/// <param name="clientId">The unique identifier for the client.</param>
	public ClientConnectionEventArgs(string clientId) => this.ClientId = clientId;

	/// <summary>
	///     Gets the unique identifier for the client.
	/// </summary>
	public string ClientId { get; }
}
