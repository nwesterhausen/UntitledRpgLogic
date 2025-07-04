using Tomlet;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Infrastructure.Configuration;

/// <summary>
///     Various utility methods that are used throughout the project.
/// </summary>
public static class Utility
{
    /// <summary>
    ///     Load a configuration file from a TOML file on disk.
    /// </summary>
    /// <param name="filepath">path to toml</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static T LoadConfig<T>(string filepath)
        where T : ITomlConfig
    {
        if (string.IsNullOrEmpty(filepath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filepath));

        if (!File.Exists(filepath))
            throw new FileNotFoundException($"Configuration file not found: {filepath}");

        TomletSupport.RegisterMappers();
        try
        {
            string fileContent = File.ReadAllText(filepath);
            if (string.IsNullOrEmpty(fileContent))
                throw new InvalidOperationException($"Configuration file is empty: {filepath}");

            T config = TomletMain.To<T>(fileContent);
            return config;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load configuration from {filepath}", ex);
        }
    }
}
