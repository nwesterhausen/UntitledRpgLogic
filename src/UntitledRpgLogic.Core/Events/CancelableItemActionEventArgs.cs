using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Event arguments for item actions that can be canceled.
/// </summary>
/// <remarks>
///     Create a new instance of <see cref="CancelableItemActionEventArgs" /> with the specified item.
/// </remarks>
/// <param name="item">Item that the action is being performed on</param>
public class CancelableItemActionEventArgs(IStorable item) : EventArgs
{

	/// <summary>
	///     Whether the action should be canceled.
	/// </summary>
	public bool Cancel { get; set; }

	/// <summary>
	///     Item that the action is being performed on.
	/// </summary>
	public IStorable Item { get; init; } = item;
}
