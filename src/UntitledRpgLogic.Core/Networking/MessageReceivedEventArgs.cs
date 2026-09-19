using System.Collections.ObjectModel;

namespace UntitledRpgLogic.Core.Networking;

/// <summary>
///     Provides data for message received events.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="MessageReceivedEventArgs" /> class.
/// </remarks>
/// <param name="data">The raw message data.</param>
/// <param name="senderId">The identifier of the message sender.</param>
public class MessageReceivedEventArgs(byte[] data, string? senderId = null) : EventArgs
{
	/// <summary>
	///     Gets the raw data of the message that was received.
	/// </summary>
	/// <remarks>
	///     The data is provided as a byte array to remain serialization-agnostic.
	///     The handler will be responsible for deserializing it into a concrete type.
	/// </remarks>
	public ReadOnlyCollection<byte> Data { get; } = Array.AsReadOnly(data);

	/// <summary>
	///     Gets the unique identifier of the client who sent the message.
	///     This may be null if the message is from the server.
	/// </summary>
	public string? SenderId { get; } = senderId ?? string.Empty;
}
