namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
/// Service contract for reading and writing catalog definitions to and from text/data streams.
/// </summary>
public interface IDefinitionSerializationService
{
	/// <summary>
	/// Gets the standard file extension associated with this format (e.g., ".toml", ".json", ".yaml").
	/// </summary>
	string FileExtension { get; }

	/// <summary>
	/// Deserializes content into a domain model definition.
	/// </summary>
	TModel Deserialize<TModel>(string content) where TModel : class, IDefined;

	/// <summary>
	/// Deserializes a stream into a domain model definition.
	/// </summary>
	TModel Deserialize<TModel>(Stream stream) where TModel : class, IDefined;

	/// <summary>
	/// Serializes a domain definition into a formatted string.
	/// </summary>
	string Serialize<TModel>(TModel model) where TModel : class, IDefined;

	/// <summary>
	/// Serializes a domain definition directly into a destination stream.
	/// </summary>
	void Serialize<TModel>(TModel model, Stream stream) where TModel : class, IDefined;
}
