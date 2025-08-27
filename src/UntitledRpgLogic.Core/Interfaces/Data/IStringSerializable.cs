namespace UntitledRpgLogic.Core.Interfaces.Data;

/// <summary>
///     Interface that defines methods for transforming the object into a serialized string and back again.
/// </summary>
/// <typeparam name="T">the object to be serialized</typeparam>
public interface IStringSerializable<out T>
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
	/// <returns>a new instance of the object</returns>
	public static abstract T Deserialize(string serialized);
}
