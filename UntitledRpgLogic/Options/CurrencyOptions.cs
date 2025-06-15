using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.Options;

/// <summary>
///     Options for creating a currency.
/// </summary>
public class CurrencyOptions
{
    /// <summary>
    ///     The singular name of the currency, e.g. "Gold", "Silver", "Copper".
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     The plural name of the currency. Specify this if you prefer the plural to match the singalar or
    ///     if the plural is not simply derived by <see cref="Classes.PluralName.BestGuessPlural" />
    /// </summary>
    public string? PluralName { get; set; }

    /// <summary>
    ///     How much this currency is worth in the base unit of currency.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    ///     What material this currency is made of.
    /// </summary>
    public required IMaterial Material { get; set; }

    /// <summary>
    ///     Specify a unique identifier for the currency type, used for serialization and identification purposes.
    /// </summary>
    public Guid? Guid { get; set; }
}
