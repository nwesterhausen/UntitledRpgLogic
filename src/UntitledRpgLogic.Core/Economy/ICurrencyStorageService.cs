using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Core.Economy;

/// <summary>
///     Domain service managing currency balances, coin-stacking, valuation, and withdrawals inside inventories.
/// </summary>
public interface ICurrencyStorageService
{
	/// <summary>
	///     Calculates the total aggregate currency value of all currency items stored in the inventory.
	/// </summary>
	long GetTotalValue(Inventory inventory);

	/// <summary>
	///     Gets the total count of a specific currency denomination held in the inventory.
	/// </summary>
	int GetQuantity(Inventory inventory, Ulid currencyItemDefinitionId);

	/// <summary>
	///     Deposits a designated quantity of currency into the inventory using an item definition template.
	/// </summary>
	bool TryDeposit(Inventory inventory, ItemDefinition currencyDef, int quantity);

	/// <summary>
	///     Withdraws a specified quantity of a currency denomination, returning the detached item stack.
	/// </summary>
	bool TryWithdraw(Inventory inventory, Ulid currencyItemDefinitionId, int quantity, out Item? withdrawnItem);
}
