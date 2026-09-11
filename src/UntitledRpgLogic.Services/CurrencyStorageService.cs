using UntitledRpgLogic.Core.Economy;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Service for an inventory that supports storing currencies.
/// </summary>
public class CurrencyStorageService : ICurrencyStorageService
{

	/// <inheritdoc />
	public IReadOnlyCollection<CurrencyBundle> DepositCurrency(CurrencyBundle currencyBundle) => throw new NotImplementedException();

	/// <inheritdoc />
	public CurrencyBundle? WithdrawCurrency(CurrencyBundle currencyBundle) => throw new NotImplementedException();

	/// <inheritdoc />
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

	/// <inheritdoc />
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;
}
