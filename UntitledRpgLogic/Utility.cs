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
}