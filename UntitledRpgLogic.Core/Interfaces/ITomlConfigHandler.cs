namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines methods for loading and saving configuration data in TOML format.
/// </summary>
/// <remarks>
///     This interface provides functionality to deserialize TOML configuration data into objects and
///     serialize objects back into TOML format. It supports operations on both file-based and in-memory representations of
///     TOML data. Implementations of this interface are expected to handle the mapping between TOML schemas and the
///     structure of the specified configuration object type.
/// </remarks>
public interface ITomlConfigHandler
{
    /// <summary>
    ///     Loads a TOML configuration file and deserializes its contents into an object of the specified type.
    /// </summary>
    /// <remarks>
    ///     The method expects the file at <paramref name="filePath" /> to be in a valid TOML format.
    ///     Ensure that the specified type <typeparamref name="T" /> has a structure that matches the TOML file's
    ///     schema.
    /// </remarks>
    /// <typeparam name="T">The type of the configuration object to deserialize. Must implement <see cref="ITomlConfig" />.</typeparam>
    /// <param name="filePath">
    ///     The path to the TOML configuration file to load. This parameter cannot be
    ///     <see langword="null" /> or empty.
    /// </param>
    /// <returns>An instance of type <typeparamref name="T" /> populated with the deserialized configuration data.</returns>
    /// <exception cref="InvalidOperationException">When the TOML cannot be parsed into <see cref="T" /></exception>
    T LoadConfigFromFile<T>(string filePath) where T : ITomlConfig;

    /// <summary>
    ///     Deserializes a TOML configuration from the specified byte array.
    /// </summary>
    /// <typeparam name="T">The type of the configuration object to deserialize. Must implement <see cref="ITomlConfig" />.</typeparam>
    /// <param name="bytes">A byte array containing the TOML configuration data. Cannot be <see langword="null" />.</param>
    /// <returns>An instance of type <typeparamref name="T" /> populated with the deserialized configuration data.</returns>
    /// <exception cref="InvalidOperationException">When the TOML cannot be parsed into <see cref="T" /></exception>
    T LoadConfig<T>(byte[] bytes) where T : ITomlConfig;

    /// <summary>
    ///     Serializes the specified configuration object into a TOML format and returns it as a byte array.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the configuration object, which must implement the <see cref="ITomlConfig" />
    ///     interface.
    /// </typeparam>
    /// <param name="config">The configuration object to serialize. Must not be <see langword="null" />.</param>
    /// <returns>A byte array containing the serialized TOML representation of the configuration object.</returns>
    /// <exception cref="InvalidOperationException">When <see cref="T" /> cannot be serialized into TOML</exception>
    byte[] SaveConfig<T>(T config) where T : ITomlConfig;

    /// <summary>
    ///     Saves the specified configuration object to a file in TOML format.
    /// </summary>
    /// <remarks>
    ///     This method serializes the configuration object into TOML format and writes it to the
    ///     specified file. Ensure that the file path is valid and that the application has write permissions for the target
    ///     location.
    /// </remarks>
    /// <typeparam name="T">The type of the configuration object, which must implement <see cref="ITomlConfig" />.</typeparam>
    /// <param name="config">The configuration object to save. Must not be <see langword="null" />.</param>
    /// <param name="filePath">
    ///     The path to the file where the configuration will be saved. Must not be <see langword="null" />
    ///     or empty.
    /// </param>
    /// <exception cref="InvalidOperationException">When <see cref="T" /> cannot be serialized into TOML</exception>
    void SaveConfigToFile<T>(T config, string filePath) where T : ITomlConfig;
}
