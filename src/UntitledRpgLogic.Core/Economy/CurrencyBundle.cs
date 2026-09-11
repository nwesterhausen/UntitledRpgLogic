using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Economy;

/// <summary>
///		A "bundle" of currency, used for transactions.
/// </summary>
public record CurrencyBundle
{
	/// <summary>
	///		Create a currency bundle record.
	/// </summary>
	/// <param name="currencyId">Id of the currency to bundle</param>
	/// <param name="amount">Amount of currency</param>
	public CurrencyBundle(Ulid currencyId, int amount)
	{
		this.CurrencyId = currencyId;
		this.Amount = amount;
	}

	/// <summary>
	///		Create a currency bundle record.
	/// </summary>
	/// <param name="currency">Currency to bundle</param>
	/// <param name="amount">Amount</param>
	public CurrencyBundle(CurrencyDefinition currency, int amount)
	{
		ArgumentNullException.ThrowIfNull(currency);

		this.CurrencyId = currency.Id;
		this.Amount = amount;
	}

	/// <summary>
	///		<see cref="CurrencyDefinition.Id"/> for the currency bundled in this record.
	/// </summary>
	public Ulid CurrencyId { get; init; }
	/// <summary>
	///		The amount of currency represented.
	/// </summary>
	public int Amount { get; init; }
}
