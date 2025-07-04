using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for an inventory that supports storing currencies.
/// </summary>
public class CurrencyStorageService : ICurrencyStorageService
{
    private readonly Dictionary<Guid, ICurrency> _currencies = new();

    /// <inheritdoc />
    public ICurrency? DepositCurrency(ICurrency currency, int? amount = null)
    {
        // Logic from CurrencyStorageBehavior will go here
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1)
    {
        // Logic from CurrencyStorageBehavior will go here
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

    /// <inheritdoc />
    public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;
}
