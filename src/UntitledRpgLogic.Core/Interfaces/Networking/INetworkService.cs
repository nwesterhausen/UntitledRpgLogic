using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces.Networking;

/// <summary>
///     Defines the contract for a server-side network port.
/// </summary>
public interface INetworkService : IBaseNetworkService
{
	/// <summary>
	///     Occurs when a new client connects to the server.
	/// </summary>
	public event EventHandler<ClientConnectionEventArgs> ClientConnected;

	/// <summary>
	///     Occurs when a client disconnects from the server.
	/// </summary>
	public event EventHandler<ClientConnectionEventArgs> ClientDisconnected;

	/// <summary>
	///     Occurs when a message is received from any client.
	/// </summary>
	public event EventHandler<MessageReceivedEventArgs> MessageReceived;

	/// <summary>
	///     Starts the server and begins listening for client connections.
	/// </summary>
	public void StartServer();

	/// <summary>
	///     Stops the server and disconnects all clients.
	/// </summary>
	public void StopServer();

	/// <summary>
	///     Sends a message to a specific client.
	/// </summary>
	/// <param name="clientId">The unique identifier of the client to send the message to.</param>
	/// <param name="payload">The network payload to send.</param>
	/// <returns>A task that represents the asynchronous send operation.</returns>
	public Task SendToClientAsync(string clientId, INetworkPayload payload);

	/// <summary>
	///     Sends a message to all connected clients.
	/// </summary>
	/// <param name="payload">The network payload to send.</param>
	/// <returns>A task that represents the asynchronous send operation.</returns>
	public Task SendToAllClientsAsync(INetworkPayload payload);
}
