namespace UntitledRpgLogic.Core.Economy;

/// <summary>
///     Contract for a coordinator to handle trading.
/// </summary>
public interface ITradeCoordinatorService
{
	/// <summary>
	///     Executes a purchase by deducting currency from the buyer, transferring items
	///     from the seller, and committing the transaction atomically.
	/// </summary>
	public Task<bool> ExecutePurchaseAsync(
		Ulid buyerEntityId,
		Ulid sellerEntityId,
		Ulid itemInstanceId,
		int quantity,
		CancellationToken cancellationToken = default);
}
