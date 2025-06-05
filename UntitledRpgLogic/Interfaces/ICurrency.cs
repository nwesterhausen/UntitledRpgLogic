namespace UntitledRpgLogic.Interfaces;

/// <summary>
///     Interface for classes that represent a currency in the game.
/// </summary>
public interface ICurrency : IHasName, ITradable, IHasTooltipString
{
    /// <summary>
    ///     How much of this currency is available.
    /// </summary>
    int Amount { get; }

    /// <summary>
    ///     The material that this currency is made of. Used for immersive purposes, such as displaying it or attributing
    ///     weight to it.
    /// </summary>
    IMaterial Material { get; }

    /// <summary>
    ///     Add an amount of <see cref="ITradable.Value" /> to this currency. Any value which cannot be wholly added is
    ///     returned.
    /// </summary>
    /// <param name="value">total amount of <see cref="ITradable.Value" /> to add</param>
    /// <returns>any remaining <see cref="ITradable.Value" /></returns>
    int Add(int value);

    /// <summary>
    ///     Subtract an amount of <see cref="ITradable.Value" /> from this currency. Any value which cannot be wholly
    ///     subtracted is returned.
    /// </summary>
    /// <param name="value">total amount of <see cref="ITradable.Value" /> to subtract</param>
    /// <returns>any <see cref="ITradable.Value" /> not able to be subtracted</returns>
    int Subtract(int value);

    /// <summary>
    ///     Convert this currency into another currency. Any limit which cannot be wholly converted is retained in this
    ///     currency.
    /// </summary>
    /// <param name="currency">target currency to convert into</param>
    /// <param name="limit">(optional) a limit to how many of target currency to convert into</param>
    /// <returns>if any amount was successfully converted</returns>
    /// <remarks>
    ///     this will act similar to <see cref="Subtract" /> in that it removes <see cref="ITradable.Value" /> from this
    ///     currency. it should <see cref="Add" /> to the target currency.
    /// </remarks>
    bool ConvertToCurrency(ICurrency currency, int? limit = null);
}