namespace UntitledRpgLogic.Core.Economy;

/// <summary>
///     Service for a currency storage system that allows depositing and withdrawing currency.
/// </summary>
public interface ICurrencyStorageService
{
	/// <summary>
	///     Deposits a specified amount of currency into the inventory.
	/// </summary>
	/// <param name="currencyBundle">The currency to deposit.</param>
	/// <returns>The remaining currency that could not be deposited, possibly in alternate denominations.</returns>
	public IReadOnlyCollection<CurrencyBundle> DepositCurrency(CurrencyBundle currencyBundle);

	/// <summary>
	///     Withdraws a specified amount of currency from the inventory.
	/// </summary>
	/// <param name="currencyBundle">The currency to withdraw.</param>
	/// <returns>The withdrawn currency, or null if not enough currency was available.</returns>
	public CurrencyBundle? WithdrawCurrency(CurrencyBundle currencyBundle);

	/// <summary>
	///     Occurs when currency is deposited into the inventory.
	/// </summary>
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

	/// <summary>
	///     Occurs when currency is withdrawn from the inventory.
	/// </summary>
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;
}
