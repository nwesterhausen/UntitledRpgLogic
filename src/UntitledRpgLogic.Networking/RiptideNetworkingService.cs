using Microsoft.Extensions.Logging;
using Riptide;
using Riptide.Utils;
using UntitledRpgLogic.Core.Contracts;
using UntitledRpgLogic.Core.Contracts.Events;
using UntitledRpgLogic.Extensions.Logging;
using MessageReceivedEventArgs = UntitledRpgLogic.Core.Contracts.Events.MessageReceivedEventArgs;

namespace UntitledRpgLogic.Networking;

/// <summary>
/// A concrete implementation of the INetworkingService contract using the Riptide v2.0.0 networking library.
/// This class acts as an "Adapter" in the Hexagonal Architecture, translating Riptide's
/// networking logic into domain-centric events and actions.
/// </summary>
internal partial class RiptideNetworkingAdapter : INetworkingService
{
	private Server? server;
	private Client? client;
	private readonly ILogger<RiptideNetworkingAdapter> logger;

	private readonly Dictionary<ushort, Guid> riptideToPlayerIdMap = new();
	private readonly Dictionary<Guid, ushort> playerToRiptideIdMap = new();


	/// <summary>
	/// Initializes a new instance of the RiptideNetworkingAdapter.
	/// </summary>
	/// <param name="logger">The logger provided by dependency injection.</param>
	public RiptideNetworkingAdapter(ILogger<RiptideNetworkingAdapter> logger)
	{
		RiptideLogger.Initialize(this.RiptideDebug,
			this.RiptideInfo,
			this.RiptideWarning,
			this.RiptideError,
			false
		);
		this.logger = logger;

		this.logger.LogNetworkServiceStarted();
	}


	/// <summary>
	/// Occurs when a player successfully connects to the server.
	/// </summary>
	public event EventHandler<PlayerConnectionEventArgs>? PlayerConnected;

	/// <summary>
	/// Occurs when a player disconnects from the server.
	/// </summary>
	public event EventHandler<PlayerConnectionEventArgs>? PlayerDisconnected;

	/// <summary>
	/// Occurs when a network message is received from a client or the server.
	/// </summary>
	public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

	/// <summary>
	/// Starts the Riptide server and begins listening for incoming connections.
	/// </summary>
	/// <param name="port">The UDP port to listen on.</param>
	public void StartServer(ushort port)
	{
		this.server = new Server();
		this.server.Start(port, 256);

		this.server.ClientConnected += this.OnRiptideClientConnected;
		this.server.ClientDisconnected += this.OnRiptideClientDisconnected;
		this.server.MessageReceived += this.OnRiptideMessageReceived;
	}

	/// <summary>
	/// Starts a Riptide client and connects to a server.
	/// </summary>
	/// <param name="address">The hostname or IP address of the server.</param>
	/// <param name="port">The UDP port of the server.</param>
	public void StartClient(string address, ushort port)
	{
		this.client = new Client();
		this.client.Connect($"{address}:{port}", 5000);

		this.client.MessageReceived += this.OnRiptideMessageReceived;
	}

	/// <summary>
	/// Processes pending networking events for both client and server.
	/// Should be called regularly (e.g., once per frame or on a timer).
	/// </summary>
	public void PollEvents()
	{
		// Renamed from Tick() to Update()
		this.server?.Update();
		this.client?.Update();
	}

	/// <summary>
	/// Sends a message directly to a specific connected player.
	/// </summary>
	/// <param name="recipientId">The target player's Guid.</param>
	/// <param name="messageData">The message payload to send.</param>
	/// <param name="isReliable">True to send reliably; otherwise false for unreliable.</param>
	public void SendTo(Guid recipientId, ReadOnlySpan<byte> messageData, bool isReliable = true)
	{
		if (!this.playerToRiptideIdMap.TryGetValue(recipientId, out var riptideId))
		{
			return;
		}

		var message = CreateMessage(messageData, isReliable);
		this.server?.Send(message, riptideId);
	}

	/// <summary>
	/// Broadcasts a message to all connected players.
	/// </summary>
	/// <param name="messageData">The message payload to send.</param>
	/// <param name="isReliable">True to send reliably; otherwise false for unreliable.</param>
	public void Broadcast(ReadOnlySpan<byte> messageData, bool isReliable = true)
	{
		var message = CreateMessage(messageData, isReliable);
		this.server?.SendToAll(message);
	}

	/// <summary>
	/// Disconnects a specific client from the server.
	/// </summary>
	/// <param name="clientId">The Guid of the client to disconnect.</param>
	public void Disconnect(Guid clientId)
	{
		if (this.playerToRiptideIdMap.TryGetValue(clientId, out var riptideId))
		{
			this.server?.DisconnectClient(riptideId);
		}
	}

	private static Message CreateMessage(ReadOnlySpan<byte> data, bool isReliable)
	{
		// Enum casing updated from .reliable to .Reliable
		var sendMode = isReliable ? MessageSendMode.Reliable : MessageSendMode.Unreliable;
		var message = Message.Create(sendMode, 0);
		message.AddBytes(data.ToArray());
		return message;
	}

	// Event arg type renamed from ServerClientConnectedEventArgs to ServerConnectedEventArgs
	private void OnRiptideClientConnected(object? sender, ServerConnectedEventArgs e)
	{
		var playerId = Guid.NewGuid();
		this.riptideToPlayerIdMap[e.Client.Id] = playerId;
		this.playerToRiptideIdMap[playerId] = e.Client.Id;

		this.PlayerConnected?.Invoke(this, new PlayerConnectionEventArgs(playerId));
	}

	// Event arg type renamed from ServerClientDisconnectedEventArgs to ServerDisconnectedEventArgs
	private void OnRiptideClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
	{
		if (this.riptideToPlayerIdMap.TryGetValue(e.Client.Id, out var playerId))
		{
			this.riptideToPlayerIdMap.Remove(e.Client.Id);
			this.playerToRiptideIdMap.Remove(playerId);

			this.PlayerDisconnected?.Invoke(this, new PlayerConnectionEventArgs(playerId));
		}
	}

	/// <summary>
	///		Handles incoming messages from the Riptide networking library, and raises the MessageReceived event from our INetworkingService contract.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnRiptideMessageReceived(object? sender, Riptide.MessageReceivedEventArgs e)
	{
		// The new MessageReceivedEventArgs directly contains FromConnection.
		// On the server, this is the client that sent the message.
		// On the client, this is the server.
		// This check correctly handles the server-side case to find the player Guid.
		if (this.riptideToPlayerIdMap.TryGetValue(e.FromConnection.Id, out var senderId))
		{
			var messageData = e.Message.GetBytes();
			this.MessageReceived?.Invoke(this, new MessageReceivedEventArgs(senderId, messageData));
		}
		// If the lookup fails, it's a message from the server to our client.
		// The senderId will remain Guid.Empty, which correctly represents the server.
		else if (this.client != null)
		{
			var messageData = e.Message.GetBytes();
			this.MessageReceived?.Invoke(this, new MessageReceivedEventArgs(Guid.Empty, messageData));
		}
	}

	/// <summary>
	///		Provides a debug logging method for Riptide messages.
	/// </summary>
	/// <param name="message">the message to debug</param>
	[LoggerMessage(Level = LogLevel.Debug, EventId = 121, Message = "{Message}", EventName = "RiptideDebug")]
	private partial void RiptideDebug(string message);

	/// <summary>
	///		Provides an info logging method for Riptide messages.
	/// </summary>
	/// <param name="message">the message to log</param>
	[LoggerMessage(Level = LogLevel.Information, EventId = 122, Message = "{Message}", EventName = "RiptideInfo")]
	private partial void RiptideInfo(string message);

	/// <summary>
	///		Provides a warning logging method for Riptide messages.
	/// </summary>
	/// <param name="message">the message to log</param>
	[LoggerMessage(Level = LogLevel.Warning, EventId = 123, Message = "{Message}", EventName = "RiptideWarning")]
	private partial void RiptideWarning(string message);

	/// <summary>
	///		Provides an error logging method for Riptide messages.
	/// </summary>
	/// <param name="message">the message to log</param>
	[LoggerMessage(Level = LogLevel.Error, EventId = 124, Message = "{Message}", EventName = "RiptideError")]
	private partial void RiptideError(string message);
}
