using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.DevTool.Services.Contracts;

/// <summary>
/// Represents a store for configuration settings, allowing retrieval, update, and management of configurations
/// identified by unique keys.
/// </summary>
/// <remarks>The <see cref="IConfigStore"/> interface provides methods to interact with configuration data,
/// including retrieving configurations by key, updating existing configurations, and accessing all stored keys. It also
/// includes properties for managing author and module information.</remarks>
public interface IConfigStore
{
    /// <summary>
    /// Retrieves the configuration associated with the specified key.
    /// </summary>
    /// <param name="key">The unique identifier for the configuration to retrieve.</param>
    /// <returns>An <see cref="ITomlConfig"/> instance representing the configuration if found; otherwise, <see
    /// langword="null"/>.</returns>
    ITomlConfig? GetConfig(Guid key);
    /// <summary>
    ///     Update the configuration for a given key.
    /// </summary>
    /// <param name="key">unique identifier for the config to update</param>
    /// <param name="config">a config to replace the config stored at <see cparamref="key"/></param>
    void UpdateConfig(Guid key, ITomlConfig config);
    /// <summary>
    ///     Get all keys in the configuration store.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Guid> GetAllKeys();
    /// <summary>
    /// Gets or sets the author information.
    /// </summary>
    AuthorConfig Author { get; set; }
    /// <summary>
    /// Gets or sets the module information.
    /// </summary>
    ModuleInfoConfig ModuleInfo { get; set; }
}
