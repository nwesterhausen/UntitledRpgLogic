using Tomlet;
using UntitledRpgLogic.Configuration;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic;

/// <summary>
///     Various utility methods that are used throughout the project.
/// </summary>
public static class Utility
{
    /// <summary>
    ///     Given a name, it will do its best to pluralize it.
    /// </summary>
    /// <param name="singular">a singular name</param>
    /// <returns>a best guess for how to pluralize the name (in english)</returns>
    public static string BestGuessPlural(string singular)
    {
        if (string.IsNullOrEmpty(singular)) return string.Empty;

        if (singular.EndsWith('y') && !singular.EndsWith("ay") && !singular.EndsWith("ey") &&
            !singular.EndsWith("oy") && !singular.EndsWith("uy"))
            return string.Concat(singular.AsSpan(0, singular.Length - 1), "ies");

        if (singular.EndsWith('s') || singular.EndsWith('x') || singular.EndsWith('z') || singular.EndsWith("ch") ||
            singular.EndsWith("sh"))
            return singular + "es";

        return singular + "s";
    }

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
