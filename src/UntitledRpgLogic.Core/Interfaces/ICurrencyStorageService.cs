using UntitledRpgLogic.Core.Events;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Service for a currency storage system that allows depositing and withdrawing currency.
/// </summary>
public interface ICurrencyStorageService
{
	/// <summary>
	///     Deposits a specified amount of currency into the inventory.
	/// </summary>
	/// <param name="currency">The currency to deposit.</param>
	/// <param name="amount">The amount of currency to deposit. If null, deposits all available.</param>
	/// <returns>The remaining currency that could not be deposited, or null if all was deposited.</returns>
	public ICurrency? DepositCurrency(ICurrency currency, int? amount = null);

	/// <summary>
	///     Withdraws a specified amount of currency from the inventory.
	/// </summary>
	/// <param name="currency">The currency to withdraw.</param>
	/// <param name="amount">The amount of currency to withdraw. Defaults to 1.</param>
	/// <returns>The withdrawn currency, or null if not enough currency was available.</returns>
	public ICurrency? WithdrawCurrency(ICurrency currency, int amount = 1);

	/// <summary>
	///     Occurs when currency is deposited into the inventory.
	/// </summary>
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyDeposited;

	/// <summary>
	///     Occurs when currency is withdrawn from the inventory.
	/// </summary>
	public event EventHandler<CurrencyMovedEventArgs>? CurrencyWithdrawn;
}
