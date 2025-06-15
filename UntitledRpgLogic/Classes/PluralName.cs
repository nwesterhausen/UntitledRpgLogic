namespace UntitledRpgLogic.Classes;

/// <summary>
///     Contains the name of an object. Has a singular, a plural and a form when used as an adjective.
/// </summary>
public class PluralName
{
    /// <summary>
    ///     Constructs a new PluralName object with the given singular, plural and adjective names. If not supplied, the
    ///     singular
    ///     will be used as the adjective and a best guess will be made for the plural.
    /// </summary>
    /// <param name="singular"></param>
    /// <param name="plural"></param>
    /// <param name="adjective"></param>
    public PluralName(string singular, string? plural = null, string? adjective = null)
    {
        Singular = singular;
        Plural = plural ?? BestGuessPlural(singular);
        Adjective = adjective ?? singular;
    }

    /// <summary>
    ///     The singular name of the object, e.g. "a Sword".
    /// </summary>
    public string Singular { get; }

    /// <summary>
    ///     The plural name of the object, e.g. "two Swords".
    /// </summary>
    public string Plural { get; }

    /// <summary>
    ///     The name used as an adjective, e.g. "Sword soup".
    /// </summary>
    public string Adjective { get; }

    /// <summary>
    ///     Given a name, it will do its best to pluralize it.
    /// </summary>
    /// <param name="singular"></param>
    /// <returns></returns>
    public static string BestGuessPlural(string singular)
    {
        if (string.IsNullOrEmpty(singular)) return string.Empty;

        if (singular.EndsWith('y') && !singular.EndsWith("ay") && !singular.EndsWith("ey") &&
            !singular.EndsWith("oy") && !singular.EndsWith("uy"))
            return string.Concat(singular.AsSpan(0, singular.Length - 1), "ies");

        if (singular.EndsWith('z') && !singular.EndsWith("zz"))
            return singular + "zes";

        if (singular.EndsWith('s') || singular.EndsWith('x') || singular.EndsWith('z') || singular.EndsWith("ch") ||
            singular.EndsWith("sh"))
            return singular + "es";

        return singular + "s";
    }

    /// <summary>
    ///     Get the name of the object, either singular or plural based on the count.
    /// </summary>
    /// <param name="count">number of items</param>
    /// <returns>appropriate name for the count of objects</returns>
    public string GetName(int count = 1)
    {
        return count == 1 ? Singular : Plural;
    }

    /// <summary>
    ///     Serializes the name into a string format that can be easily stored or transmitted.
    /// </summary>
    /// <returns>the names separated by semicolons, avoiding duplicates</returns>
    /// <remarks>if the plural is derived using <see cref="BestGuessPlural" /> it will be omitted in the result</remarks>
    public string Serialize()
    {
        if (Singular.Equals(Adjective))
        {
            try
            {
                string pluralEnding = Plural[(Singular.Length - 1)..];
                if (pluralEnding.Length == 0 || pluralEnding.Equals("es") || pluralEnding.Equals("s"))
                    return $"{Singular}";
            }
            catch (Exception)
            {
                // If there's an error in slicing, don't throw an error just move on.
            }

            return $"{Singular};{Plural}";
        }

        return $"{Singular};{Plural};{Adjective}";
    }

    /// <summary>
    ///     Deserializes a string into the PluralName object. Expects the string to be in the format:
    ///     `Singular;Plural;Adjective`
    /// </summary>
    /// <param name="serialized">serialized string</param>
    /// <returns>PluralName object reconstructed from serialized version</returns>
    /// <exception cref="ArgumentException">failed to deserialize the string</exception>
    public static PluralName Deserialize(string serialized)
    {
        string[] parts = serialized.Split(';');
        return parts.Length switch
        {
            0 => throw new ArgumentException("Invalid serialized name format."),
            1 => new PluralName(parts[0]),
            2 => new PluralName(parts[0], parts[1]),
            _ => new PluralName(parts[0], parts[1], parts[2])
        };
    }
}
