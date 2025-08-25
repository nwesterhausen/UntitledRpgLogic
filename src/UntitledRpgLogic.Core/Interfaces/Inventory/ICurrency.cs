using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Interfaces.Inventory;

/// <summary>
///     Interface for classes that represent a currency in the game.
/// </summary>
public interface ICurrency : IHasName, ITradable, IHasTooltipString
{
	/// <summary>
	///     How much of this currency is available.
	/// </summary>
	public int Amount { get; }

	/// <summary>
	///     The material that this currency is made of. Used for immersive purposes, such as displaying it or attributing
	///     weight to it.
	/// </summary>
	public IMaterial Material { get; }

	/// <summary>
	///     A unique identifier for the currency type, used for serialization and identification purposes.
	/// </summary>
	public Guid CurrencyId { get; }

	/// <summary>
	///     Add an amount of <see cref="ITradable.Value" /> to this currency. Any value which cannot be wholly added is
	///     returned.
	/// </summary>
	/// <param name="value">total amount of <see cref="ITradable.Value" /> to add</param>
	/// <returns>any remaining <see cref="ITradable.Value" /></returns>
	public long Add(long value);

	/// <summary>
	///     Subtract an amount of <see cref="ITradable.Value" /> from this currency. Any value which cannot be wholly
	///     subtracted is returned.
	/// </summary>
	/// <param name="value">total amount of <see cref="ITradable.Value" /> to subtract</param>
	/// <returns>any <see cref="ITradable.Value" /> not able to be subtracted</returns>
	public long Subtract(long value);
}
