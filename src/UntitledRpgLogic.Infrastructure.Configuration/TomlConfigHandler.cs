using System.Text;
using Microsoft.Extensions.Logging;
using Tomlet;
using Tomlet.Models;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Data;
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
	private readonly Dictionary<ConfigType, Type> configTypeMappings = new()
	{
		{ ConfigType.Item, typeof(ItemDataConfig) },
		{ ConfigType.Material, typeof(MaterialDataConfig) },
		{ ConfigType.Skill, typeof(SkillDataConfig) },
		{ ConfigType.Stat, typeof(StatDataConfig) }
	};

	private readonly ILogger<TomlConfigHandler> logger;

	/// <summary>
	///     Initializes a new instance of the <see cref="TomlConfigHandler" /> class.
	/// </summary>
	/// <param name="logger">The logger instance used for logging operations. Cannot be <see langword="null" />.</param>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="logger" /> is <see langword="null" />.</exception>
	public TomlConfigHandler(ILogger<TomlConfigHandler> logger)
	{
		this.logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");

		// Register mappers for custom types only once
		MapperRegistration.RegisterMappers();
	}

	/// <inheritdoc />
	public TConfig LoadConfigFromFile<TConfig>(string filePath) where TConfig : ITomlConfig
	{
		if (string.IsNullOrEmpty(filePath))
		{
			throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
		}

		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException($"Configuration file not found: {filePath}");
		}

		try
		{
			var fileContent = File.ReadAllText(filePath);

			return this.ParseTomlConfigFromText<TConfig>(fileContent);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to load configuration from {filePath}", ex);
		}
	}

	/// <inheritdoc />
	public TConfig LoadConfig<TConfig>(byte[] bytes) where TConfig : ITomlConfig
	{
		if (bytes == null || bytes.Length == 0)
		{
			throw new ArgumentException("Byte array cannot be null or empty.", nameof(bytes));
		}

		try
		{
			var tomlText = Encoding.UTF8.GetString(bytes);
			return this.ParseTomlConfigFromText<TConfig>(tomlText);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Failed to load configuration from byte array.", ex);
		}
	}

	/// <inheritdoc />
	public byte[] SaveConfig<TConfig>(TConfig config) where TConfig : ITomlConfig
	{
		if (config == null)
		{
			throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
		}

		try
		{
			var tomlString = SerializeToml(config);
			return Encoding.UTF8.GetBytes(tomlString);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to serialize {nameof(TConfig)} to TOML.", ex);
		}
	}

	/// <inheritdoc />
	public void SaveConfigToFile<TConfig>(TConfig config, string filePath) where TConfig : ITomlConfig
	{
		if (string.IsNullOrEmpty(filePath))
		{
			throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
		}

		if (config == null)
		{
			throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
		}

		try
		{
			var tomlString = SerializeToml(config);
			File.WriteAllText(filePath, tomlString);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to save configuration to {filePath}", ex);
		}
	}

	private static string SerializeToml<TConfig>(TConfig config) where TConfig : ITomlConfig
	{
		if (config == null)
		{
			throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
		}

		try
		{
			return TomletMain.TomlStringFrom(config);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Failed to serialize configuration to TOML.", ex);
		}
	}

	private TConfig ParseTomlConfigFromText<TConfig>(string text) where TConfig : ITomlConfig
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentException("TOML text cannot be null or empty.", nameof(text));
		}

		try
		{
			// Parse TOML into TomlDocument
			TomlParser parser = new();
			var tomlDocument = parser.Parse(text);

			// Check if the document contains the expected field
			if (!tomlDocument.ContainsKey("ConfigType"))
			{
				this.logger.LogError("TOML document does not contain required 'ConfigType' field.");
				throw new InvalidOperationException("TOML document does not contain required 'ConfigType' field.");
			}

			// Extract the ConfigType from the document
			if (!tomlDocument.TryGetValue("ConfigType", out var configType) ||
			    configType is not TomlString configTypeString)
			{
				this.logger.LogError("'ConfigType' is not set or not a string");
				throw new InvalidOperationException("'ConfigType' is not set or not a string.");
			}

			if (!Enum.TryParse(configTypeString.Value, true, out ConfigType foundConfigType))
			{
				this.logger.LogError("Invalid 'ConfigType' value '{ConfigTypeValue}' found in TOML document.",
					configTypeString.Value);
				throw new InvalidOperationException($"Invalid 'ConfigType' value '{configTypeString.Value}'.");
			}

			if (!this.configTypeMappings.TryGetValue(foundConfigType, out var configTypeToLoad))
			{
				this.logger.LogError("No mapping found for ConfigType '{ConfigType}'", foundConfigType);
				throw new InvalidOperationException($"No mapping found for ConfigType '{foundConfigType}'.");
			}

			return (TConfig)TomletMain.To(configTypeToLoad, tomlDocument);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Failed to parse TOML text.", ex);
		}
	}
}
