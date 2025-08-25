using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extensions for currency-related operations.
/// </summary>
public static class CurrencyExtensions
{
	/// <summary>
	///     Convert this currency into another currency. Any limit which cannot be wholly converted is retained in this
	///     currency.
	/// </summary>
	/// <param name="currency">this currency (converting from)</param>
	/// <param name="otherCurrency">target currency to convert into</param>
	/// <param name="limit">(optional) a limit to how many of target currency to convert into</param>
	/// <returns>true if any amount was successfully converted</returns>
	/// <remarks>
	///     this will act similar to <see cref="ICurrency.Subtract" /> in that it removes <see cref="ITradable.Value" /> from
	///     this currency. it performs <see cref="ICurrency.Add" /> on the target currency.
	/// </remarks>
	public static bool ConvertToCurrency(this ICurrency currency, ICurrency otherCurrency, int? limit = null)
	{
		ArgumentNullException.ThrowIfNull(currency, nameof(currency));
		ArgumentNullException.ThrowIfNull(otherCurrency, nameof(otherCurrency));

		// Calculate how much can be converted
		var canConvert = (int)(currency.GetTotalValue() / otherCurrency.Value);

		if (limit == null || canConvert < limit)
		{
			limit = canConvert;
		}

		if (limit <= 0)
		{
			return false;
		}

		// Calculate how much to subtract from this otherCurrency
		var toSubtract = limit.Value * otherCurrency.Value;
		// Subtract the value from this otherCurrency
		var remaining = currency.Subtract(toSubtract);

		// Find the actual amount to add to the target otherCurrency
		var toAdd = toSubtract - remaining;
		// Add the value to the target otherCurrency
		_ = otherCurrency.Add(toAdd);

		return true;
	}
}
