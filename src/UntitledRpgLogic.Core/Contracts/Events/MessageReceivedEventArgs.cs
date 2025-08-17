using System;

namespace UntitledRpgLogic.Core.Contracts.Events;

/// <summary>
/// Provides data for the MessageReceived event.
/// </summary>
public class MessageReceivedEventArgs : EventArgs
{
	/// <summary>
	/// The unique identifier of the player who sent the message.
	/// </summary>
	public Guid SenderId { get; }

	/// <summary>
	/// The payload of the received message.
	/// </summary>
	/// <remarks>
	/// We use ReadOnlyMemory here because it can be stored on the heap, unlike
	/// ReadOnlySpan, making it suitable for use in an EventArgs class that
	/// may persist beyond the immediate scope of the event invocation.
	/// </remarks>
	public ReadOnlyMemory<byte> Data { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="MessageReceivedEventArgs"/> class.
	/// </summary>
	/// <param name="senderId">The unique identifier of the message sender.</param>
	/// <param name="data">The message payload.</param>
	public MessageReceivedEventArgs(Guid senderId, ReadOnlyMemory<byte> data)
	{
		this.SenderId = senderId;
		this.Data = data;
	}
}
