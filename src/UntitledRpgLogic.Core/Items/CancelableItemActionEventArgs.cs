namespace UntitledRpgLogic.Core.Items;

/// <summary>
///     Event arguments for item actions that can be canceled.
/// </summary>
/// <param name="itemId">Identifier of the item being acted upon</param>
public class CancelableItemActionEventArgs(Ulid itemId) : EventArgs
{
	/// <summary>
	///     Whether the action should be canceled.
	/// </summary>
	public bool Cancel { get; set; }

	/// <summary>
	///     Item that the action is being performed on.
	/// </summary>
	public Ulid ItemId { get; init; } = itemId;
}
