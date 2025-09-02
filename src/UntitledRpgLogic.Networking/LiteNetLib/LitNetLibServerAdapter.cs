using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using LiteNetLib;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces.Networking;

namespace UntitledRpgLogic.Networking.LiteNetLib;

/// <summary>
///     An implementation of INetworkService using LiteNetLib for networking.
/// </summary>
public class LitNetLibServerAdapter : INetworkService
{
	private readonly EventBasedNetListener listener;
	private readonly IPayloadSerializer serializer;
	private readonly NetManager server;
	private CancellationTokenSource cancellationTokenSource;
	private bool disposedValue;
	private Thread networkThread;

	/// <summary>
	///     Creates a new instance of the LitNetLibServerAdapter with a payload serializer (via DI).
	/// </summary>
	/// <param name="serializer">the serializer we're using</param>
	public LitNetLibServerAdapter(IPayloadSerializer serializer)
	{
		this.serializer = serializer;
		this.listener = new EventBasedNetListener();
		this.server = new NetManager(this.listener);

		// Map LiteNetLib events to our IServerPort events
		this.listener.ConnectionRequestEvent += this.OnConnectionRequest;
		this.listener.PeerConnectedEvent += this.OnPeerConnected;
		this.listener.PeerDisconnectedEvent += this.OnPeerDisconnected;
		this.listener.NetworkReceiveEvent += this.OnNetworkReceive;
	}

	/// <inheritdoc />
	public bool IsRunning { get; private set; }

	/// <inheritdoc />
	public event EventHandler<ClientConnectionEventArgs>? ClientConnected;

	/// <inheritdoc />
	public event EventHandler<ClientConnectionEventArgs>? ClientDisconnected;

	/// <inheritdoc />
	public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

	/// <inheritdoc />
	public void StartServer()
	{
		if (this.IsRunning)
		{
			return;
		}

		this.IsRunning = this.server.Start(4296); // Port to listen on
		if (this.IsRunning)
		{
			this.cancellationTokenSource = new CancellationTokenSource();
			this.networkThread = new Thread(() => this.NetworkLoop(this.cancellationTokenSource.Token));
			this.networkThread.Start();
		}
	}

	/// <inheritdoc />
	public void StopServer()
	{
		if (!this.IsRunning)
		{
			return;
		}

		this.cancellationTokenSource?.Cancel();
		this.networkThread?.Join(); // Wait for the thread to finish
		this.server.Stop();
		this.IsRunning = false;
	}

	/// <inheritdoc />
	public Task SendToClientAsync(string clientId, INetworkPayload payload)
	{
		if (!int.TryParse(clientId, out var id))
		{
			throw new ArgumentException("Invalid client ID format for LiteNetLib.", nameof(clientId));
		}

		var client = this.server.GetPeerById(id);
		if (client != null)
		{
			var data = this.serializer.Serialize(payload);
			// We can map our own reliability enum to LiteNetLib's DeliveryMethod
			client.Send(data, DeliveryMethod.ReliableOrdered);
		}

		return Task.CompletedTask;
	}

	/// <inheritdoc />
	public Task SendToAllClientsAsync(INetworkPayload payload)
	{
		var data = this.serializer.Serialize(payload);
		this.server.SendToAll(data, DeliveryMethod.ReliableOrdered);
		return Task.CompletedTask;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void NetworkLoop(CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			this.server.PollEvents();
			Thread.Sleep(15); // Standard poll rate for LiteNetLib
		}
	}


	// --- Event Handlers for LiteNetLib ---

	[SuppressMessage("ReSharper", "ArrangeMethodOrOperatorBody")]
	private void OnConnectionRequest(ConnectionRequest request)
	{
		// Here you could add logic to accept/reject based on a key or server capacity
		request.AcceptIfKey("UntitledRpgKey");
	}

	private void OnPeerConnected(NetPeer peer) =>
		this.ClientConnected?.Invoke(this, new ClientConnectionEventArgs(peer.Id.ToString(CultureInfo.InvariantCulture)));


	private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) =>
		this.ClientDisconnected?.Invoke(this, new ClientConnectionEventArgs(peer.Id.ToString(CultureInfo.InvariantCulture)));

	private void OnNetworkReceive(NetPeer fromPeer, NetPacketReader dataReader, byte channel, DeliveryMethod deliveryMethod)
	{
		// The dataReader contains the raw bytes of the message
		var data = dataReader.GetRemainingBytes();
		this.MessageReceived?.Invoke(this, new MessageReceivedEventArgs(data, fromPeer.Id.ToString(CultureInfo.InvariantCulture)));
		dataReader.Recycle(); // IMPORTANT: Recycle the reader to avoid GC pressure
	}

	/// <inheritdoc cref="Dispose" />
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposedValue)
		{
			if (disposing)
			{
				// Dispose managed state (managed objects).
				this.StopServer();
				this.cancellationTokenSource?.Dispose();
			}

			// Free unmanaged resources (unmanaged objects) and override finalizer
			// NOTE: This class doesn't have unmanaged resources, but this is where they would be cleaned up.
			this.disposedValue = true;
		}
	}
}
