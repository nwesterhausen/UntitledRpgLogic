namespace UntitledRpgLogic.Core.Interfaces.Networking;

/// <summary>
/// Represents a network payload with a message type and unique identifier.
/// Defines the base contract for any object that can be sent over the network.
/// </summary>
public interface INetworkPayload
{
	/// <summary>
	/// The type of the message being sent.
	/// </summary>
	public Enums.MessageType MessageType { get; }

	/// <summary>
	/// The processing priority level of the message.
	/// </summary>
	public Enums.MessagePriority MessagePriority { get; }

	/// <summary>
	/// The unique identifier for the message.
	/// </summary>
	public Ulid MessageId { get; }
}
