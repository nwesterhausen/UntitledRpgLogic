using UntitledRpgLogic.Events;
using UntitledRpgLogic.Interfaces;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <summary>
///     Behavior for an inventory that supports storing currencies.
/// </summary>
public class CurrencyStorageBehavior : ICurrencyStorage
{
    /// <summary>
    ///     The stored currencies in the inventory, indexed by their currency identifier (GUID).
    /// </summary>
    private Dictionary<Guid, ICurrency> _currencies = new();

    /// <inheritdoc />
    public ICurrency? DepositCurrency(ICurrency currency, int? amount = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public event EventHandler<CurrencyDepositedEventArgs>? CurrencyDeposited;

    /// <inheritdoc />
    public event EventHandler<CurrencyWithdrawnEventArgs>? CurrencyWithdrawn;
}