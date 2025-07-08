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
    /// Loads a TOML configuration from the specified file.
    /// </summary>
    /// <param name="filePath">The path to the TOML file to be loaded. Must not be null or empty.</param>
    /// <returns>An <see cref="ITomlConfig"/> instance representing the configuration data from the file.</returns>
    /// <exception cref="InvalidOperationException">When the TOML cannot be parsed into <see cref="ITomlConfig" /></exception>
    ITomlConfig LoadConfigFromFile(string filePath);

    /// <summary>
    /// Loads a TOML configuration from the specified byte array.
    /// </summary>
    /// <param name="bytes">The byte array containing the TOML configuration data. Cannot be null or empty.</param>
    /// <returns>An <see cref="ITomlConfig"/> instance representing the loaded configuration.</returns>
    /// <exception cref="InvalidOperationException">When the TOML cannot be parsed into <see cref="ITomlConfig" /></exception>
    ITomlConfig LoadConfig(byte[] bytes);

    /// <summary>
    ///     Serializes the specified configuration object into a TOML format and returns it as a byte array.
    /// </summary>
    /// <typeparam name="TConfig">
    ///     The type of the configuration object, which must implement the <see cref="ITomlConfig" />
    ///     interface.
    /// </typeparam>
    /// <param name="config">The configuration object to serialize. Must not be <see langword="null" />.</param>
    /// <returns>A byte array containing the serialized TOML representation of the configuration object.</returns>
    /// <exception cref="InvalidOperationException">When <see typecref="TConfig" /> cannot be serialized into TOML</exception>
    byte[] SaveConfig<TConfig>(TConfig config) where TConfig : ITomlConfig;

    /// <summary>
    ///     Saves the specified configuration object to a file in TOML format.
    /// </summary>
    /// <remarks>
    ///     This method serializes the configuration object into TOML format and writes it to the
    ///     specified file. Ensure that the file path is valid and that the application has write permissions for the target
    ///     location.
    /// </remarks>
    /// <typeparam name="TConfig">The type of the configuration object, which must implement <see cref="ITomlConfig" />.</typeparam>
    /// <param name="config">The configuration object to save. Must not be <see langword="null" />.</param>
    /// <param name="filePath">
    ///     The path to the file where the configuration will be saved. Must not be <see langword="null" />
    ///     or empty.
    /// </param>
    /// <exception cref="InvalidOperationException">When <see typecref="TConfig" /> cannot be serialized into TOML</exception>
    void SaveConfigToFile<TConfig>(TConfig config, string filePath) where TConfig : ITomlConfig;
}
