using UntitledRpgLogic.Core.Contracts.Events;

namespace UntitledRpgLogic.Core.Contracts;

/// <summary>
///     Defines the contract for a networking adapter, allowing the core game logic
///     to send and receive data without being coupled to a specific networking library.
///     This contract is the boundary between the core logic and the networking infrastructure.
/// </summary>
public interface INetworkingService
{
	/// <summary>
	///     Event raised when a new client has successfully connected and been authenticated.
	/// </summary>
	public event EventHandler<PlayerConnectionEventArgs> PlayerConnected;

	/// <summary>
	///     Event raised when a client disconnects.
	/// </summary>
	public event EventHandler<PlayerConnectionEventArgs> PlayerDisconnected;

	/// <summary>
	///     Event raised when a message is received from a client.
	/// </summary>
	public event EventHandler<MessageReceivedEventArgs> MessageReceived;

	/// <summary>
	///     Starts the networking layer in server mode.
	/// </summary>
	/// <param name="port">The network port to listen on.</param>
	public void StartServer(ushort port);

	/// <summary>
	///     Starts the networking layer in client mode.
	/// </summary>
	/// <param name="address">The IP address or hostname of the server.</param>
	/// <param name="port">The network port to connect to.</param>
	public void StartClient(string address, ushort port);

	/// <summary>
	///     Sends a message to a specific client.
	/// </summary>
	/// <param name="recipientId">The unique identifier of the recipient client.</param>
	/// <param name="messageData">The raw byte data of the message to send.</param>
	/// <param name="isReliable">
	///     True to send the message reliably (guaranteed delivery), false for unreliable (faster, for non-critical data like
	///     movement).
	/// </param>
	public void SendTo(Guid recipientId, ReadOnlySpan<byte> messageData, bool isReliable = true);

	/// <summary>
	///     Broadcasts a message to all connected clients.
	/// </summary>
	/// <param name="messageData">The raw byte data of the message to send.</param>
	/// <param name="isReliable">True to send the message reliably, false for unreliable.</param>
	public void Broadcast(ReadOnlySpan<byte> messageData, bool isReliable = true);

	/// <summary>
	///     Disconnects a specific client from the server.
	/// </summary>
	/// <param name="clientId">The ID of the client to disconnect.</param>
	public void Disconnect(Guid clientId);

	/// <summary>
	///     Polls for new network events (connections, disconnections, messages).
	///     This should be called once per game loop tick.
	/// </summary>
	public void PollEvents();
}
