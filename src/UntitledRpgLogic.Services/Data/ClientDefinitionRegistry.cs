using System.Collections.Concurrent;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Services.Data;

/// <summary>
///     Thread-safe client-side registry storing static package-loaded definitions
///     and runtime server-injected dynamic definitions.
/// </summary>
public sealed class ClientDefinitionRegistry : IClientDefinitionRegistry
{
	// Key: (Type, Ulid) -> Definition object
	private readonly ConcurrentDictionary<(Type DefinitionType, Ulid Id), IDefined> definitions = new();

	/// <inheritdoc />
	public void LoadPackDefinitions(ExtractedPackageContent packContent)
	{
		ArgumentNullException.ThrowIfNull(packContent);

		foreach (var material in packContent.Materials)
		{
			this.RegisterDynamicDefinition(material);
		}

		foreach (var stat in packContent.Stats)
		{
			this.RegisterDynamicDefinition(stat);
		}

		foreach (var skill in packContent.Skills)
		{
			this.RegisterDynamicDefinition(skill);
		}

		foreach (var item in packContent.Items)
		{
			this.RegisterDynamicDefinition(item);
		}

		foreach (var entity in packContent.Entities)
		{
			this.RegisterDynamicDefinition(entity);
		}
	}

	/// <inheritdoc />
	public void RegisterDynamicDefinition<T>(T definition) where T : class, IDefined
	{
		ArgumentNullException.ThrowIfNull(definition);

		var key = (typeof(T), definition.Id);
		this.definitions[key] = definition;
	}

	/// <inheritdoc />
	public T? GetDefintion<T>(Ulid id) where T : class, IDefined
	{
		var key = (typeof(T), id);
		return this.definitions.TryGetValue(key, out var def) ? def as T : null;
	}

	/// <summary>
	///     Retrieves a definition by its unique identifier.
	/// </summary>
	/// <typeparam name="T">The definition type.</typeparam>
	/// <param name="id">The definition identifier.</param>
	/// <returns>The definition instance, or <see langword="null" /> if not registered.</returns>
	public T? Get<T>(Ulid id) where T : class, IDefined => this.GetDefintion<T>(id);
}
