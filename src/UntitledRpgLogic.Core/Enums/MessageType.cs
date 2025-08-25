namespace UntitledRpgLogic.Core.Enums;

/// <summary>
/// Defines the different types of network messages that can be sent.
/// </summary>
public enum MessageType
{
	/// <summary>
	/// No specific message type. A no-operation message.
	/// <remarks>This should never be sent over the network.</remarks>
	/// </summary>
	None = 0,

	/// <summary>
	/// A generic acknowledgement message.
	/// </summary>
	Ack,

	/// <summary>
	/// A message indicating a player has connected.
	/// </summary>
	PlayerConnect,

	/// <summary>
	/// A message indicating a player has disconnected.
	/// </summary>
	PlayerDisconnect,

	/// <summary>
	/// A message containing player chat information.
	/// </summary>
	PlayerChat,

	/// <summary>
	/// A message containing player movement information.
	/// </summary>
	PlayerMovement,

	// Add other message types here as needed
}
