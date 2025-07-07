using System.Text;
using Tomlet;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

namespace UntitledRpgLogic.Infrastructure.Configuration;

/// <summary>
///     Provides functionality for loading and saving configuration data in TOML format.
/// </summary>
/// <remarks>
///     This class implements the <see cref="ITomlConfigHandler" /> interface, offering methods to load
///     configuration objects from files or byte arrays and to save configuration objects to files or byte arrays. The
///     configuration objects must implement the <see cref="ITomlConfig" /> interface.
/// </remarks>
public class TomlConfigHandler : ITomlConfigHandler
{
    /// <inheritdoc />
    public T LoadConfigFromFile<T>(string filePath) where T : ITomlConfig
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Configuration file not found: {filePath}");
        try
        {
            string fileContent = File.ReadAllText(filePath);

            return ParseTomlFromText<T>(fileContent);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load configuration from {filePath}", ex);
        }
    }

    /// <inheritdoc />
    public T LoadConfig<T>(byte[] bytes) where T : ITomlConfig
    {
        if (bytes == null || bytes.Length == 0)
            throw new ArgumentException("Byte array cannot be null or empty.", nameof(bytes));

        try
        {
            string tomlText = Encoding.UTF8.GetString(bytes);
            return ParseTomlFromText<T>(tomlText);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to load configuration from byte array.", ex);
        }
    }

    /// <inheritdoc />
    public byte[] SaveConfig<T>(T config) where T : ITomlConfig
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
        try
        {
            string tomlString = SerializeToml(config);
            return Encoding.UTF8.GetBytes(tomlString);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to serialize {nameof(T)} to TOML.", ex);
        }
    }

    /// <inheritdoc />
    public void SaveConfigToFile<T>(T config, string filePath) where T : ITomlConfig
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        if (config == null)
            throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
        try
        {
            string tomlString = SerializeToml(config);
            File.WriteAllText(filePath, tomlString);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to save configuration to {filePath}", ex);
        }
    }

    private static string SerializeToml<T>(T config) where T : ITomlConfig
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
        MapperRegistration.RegisterMappers();
        try
        {
            return TomletMain.TomlStringFrom(config);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to serialize configuration to TOML.", ex);
        }
    }

    private static T ParseTomlFromText<T>(string text) where T : ITomlConfig
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("TOML text cannot be null or empty.", nameof(text));

        MapperRegistration.RegisterMappers();

        try
        {
            return TomletMain.To<T>(text);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to parse TOML text.", ex);
        }
    }
}
