namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
/// Service contract for reading and writing catalog definitions to and from text/data streams.
/// </summary>
public interface IDefinitionSerializationService
{
	/// <summary>
	/// The primary file extension used when writing files (e.g., ".toml", ".yaml").
	/// </summary>
	public string DefaultFileExtension { get; }

	/// <summary>
	/// All extensions supported by this deserializer (e.g., [".yaml", ".yml"]).
	/// </summary>
	public IReadOnlyCollection<string> SupportedFileExtensions { get; }

	/// <summary>
	/// Deserializes content into a domain model definition.
	/// </summary>
	public TModel Deserialize<TModel>(string content) where TModel : class, IDefined;

	/// <summary>
	/// Deserializes a stream into a domain model definition.
	/// </summary>
	public TModel Deserialize<TModel>(Stream stream) where TModel : class, IDefined;

	/// <summary>
	/// Serializes a domain definition into a formatted string.
	/// </summary>
	public string Serialize<TModel>(TModel model) where TModel : class, IDefined;

	/// <summary>
	/// Serializes a domain definition directly into a destination stream.
	/// </summary>
	public void Serialize<TModel>(TModel model, Stream stream) where TModel : class, IDefined;
}

/// <summary>
/// Resolves the appropriate serialization service based on file extensions or paths.
/// </summary>
public interface IDefinitionSerializerRouter
{
	IDefinitionSerializationService GetServiceForExtension(string extension);
	IDefinitionSerializationService GetServiceForFile(string filePath);
	bool SupportsExtension(string extension);
	bool SupportsFile(string filePath);
}
