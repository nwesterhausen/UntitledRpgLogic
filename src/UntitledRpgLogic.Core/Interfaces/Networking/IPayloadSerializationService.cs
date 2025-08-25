namespace UntitledRpgLogic.Core.Interfaces.Networking;

/// <summary>
/// Defines the contract for a network payload serializer/deserializer.
/// </summary>
public interface IPayloadSerializationService
{
	/// <summary>
	/// Serializes the specified network payload into a byte array.
	/// </summary>
	/// <param name="payload">The payload to serialize.</param>
	/// <returns>A byte array representing the serialized payload.</returns>
	public byte[] Serialize(INetworkPayload payload);

	/// <summary>
	/// Deserializes a byte array into a specific network payload type.
	/// </summary>
	/// <typeparam name="T">The type of the payload to deserialize into.</typeparam>
	/// <param name="data">The byte array to deserialize.</param>
	/// <returns>The deserialized network payload.</returns>
	public T Deserialize<T>(byte[] data) where T : INetworkPayload;
}
