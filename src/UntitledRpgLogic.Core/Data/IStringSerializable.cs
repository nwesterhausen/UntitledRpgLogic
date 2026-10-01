using System.Diagnostics.CodeAnalysis;

namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Interface that defines methods for transforming the object into a serialized string and back again.
/// </summary>
/// <typeparam name="T">the object to be serialized</typeparam>
public interface IStringSerializable<T>
	where T : IStringSerializable<T>
{
	/// <summary>
	///     Turn this object into string representation that can be used to store in a TOML file, or other format.
	/// </summary>
	/// <returns>a string that can later be deserialized back into the object</returns>
	public string Serialize();

	/// <summary>
	///     Transform from the serialized string version of this object into an instance of it.
	/// </summary>
	/// <param name="serialized">the string representation of the object</param>
	/// <returns>a new instance of the object</returns>
	public static virtual T Deserialize(string serialized)
	{
		ArgumentNullException.ThrowIfNull(serialized);

		if (!T.TryDeserialize(serialized, out var result))
		{
			throw new InvalidDataException($"Failed to deserialize '{typeof(T).Name}' from input: '{serialized}'.");
		}

		return result;
	}

	/// <summary>
	///     Tries to transform the serialized string version of this object into an instance of it.
	/// </summary>
	/// <param name="serialized">the string representation of the object</param>
	/// <param name="result">
	///     When this method returns, contains the deserialized instance if successful;
	///     otherwise, the default value.
	/// </param>
	/// <returns><see langword="true" /> if deserialization succeeded; otherwise, <see langword="false" />.</returns>
	public static abstract bool TryDeserialize(
		[NotNullWhen(true)] string? serialized,
		[MaybeNullWhen(false)] out T result);
}
