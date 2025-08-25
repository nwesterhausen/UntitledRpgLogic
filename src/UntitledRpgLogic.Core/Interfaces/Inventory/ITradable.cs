namespace UntitledRpgLogic.Core.Interfaces.Inventory;

/// <summary>
///     Assigns value to an object, and enables it to be traded or valued in some way.
/// </summary>
public interface ITradable
{
	/// <summary>
	///     The value of a single unit of this tradable item.
	/// </summary>
	public long Value { get; }

	/// <summary>
	///     Returns the total value of this tradable item, which in cases of currency or stackable items is the value
	///     multiplied by the amount there is.
	/// </summary>
	/// <returns>total value of the object, taking into account how much the object represents</returns>
	public long GetTotalValue();
}
