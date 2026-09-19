using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Core.Data;

/// <summary>
///     Maintains a client-side registry of static catalog definitions and runtime server-injected definitions.
/// </summary>
public interface IClientDefinitionRegistry
{
	/// <summary>
	///     Ingests catalog definitions loaded from an extracted <c>.urpglib</c> package payload.
	/// </summary>
	/// <param name="packContent">The extracted package content containing definitions to register.</param>
	public void LoadPackDefinitions(ExtractedPackageContent packContent);

	/// <summary>
	///     Registers a dynamic content definition pushed from the authoritative server at runtime.
	/// </summary>
	/// <typeparam name="T">The definition type implementing <see cref="IDefined" />.</typeparam>
	/// <param name="definition">The dynamic definition instance to store.</param>
	public void RegisterDynamicDefinition<T>(T definition) where T : class, IDefined;

	/// <summary>
	///     Retrieves a definition by its unique identifier.
	/// </summary>
	/// <typeparam name="T">The expected definition type.</typeparam>
	/// <param name="id">The unique identifier of the definition.</param>
	/// <returns>The matching definition instance, or <see langword="null" /> if not found.</returns>
	public T? GetDefintion<T>(Ulid id) where T : class, IDefined;
}
