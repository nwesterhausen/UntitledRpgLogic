namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Provides data for client connection events.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="ClientConnectionEventArgs" /> class.
/// </remarks>
/// <param name="clientId">The unique identifier for the client.</param>
public class ClientConnectionEventArgs(string clientId) : EventArgs
{

	/// <summary>
	///     Gets the unique identifier for the client.
	/// </summary>
	public string ClientId { get; } = clientId;
}
