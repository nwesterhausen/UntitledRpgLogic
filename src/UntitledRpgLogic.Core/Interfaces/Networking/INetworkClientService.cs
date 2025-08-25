using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces.Networking;

/// <summary>
/// Defines the contract for a client-side network port.
/// </summary>
public interface INetworkClientService : IBaseNetworkService
{
	/// <summary>
	/// Occurs when the client successfully connects to the server.
	/// </summary>
	public event EventHandler Connected;

	/// <summary>
	/// Occurs when the client disconnects from the server.
	/// </summary>
	public event EventHandler Disconnected;

	/// <summary>
	/// Occurs when a message is received from the server.
	/// </summary>
	public event EventHandler<MessageReceivedEventArgs> MessageReceived;

	/// <summary>
	/// Connects the client to the server.
	/// </summary>
	/// <param name="connectionString">The connection string or address of the server.</param>
	public void Connect(string connectionString);

	/// <summary>
	/// Disconnects the client from the server.
	/// </summary>
	public void Disconnect();

	/// <summary>
	/// Sends a message to the server.
	/// </summary>
	/// <param name="payload">The network payload to send.</param>
	/// <returns>A task that represents the asynchronous send operation.</returns>
	public Task SendToServerAsync(INetworkPayload payload);
}
