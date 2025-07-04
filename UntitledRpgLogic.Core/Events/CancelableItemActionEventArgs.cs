using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Event arguments for item actions that can be canceled.
/// </summary>
public class CancelableItemActionEventArgs : EventArgs
{
    /// <summary>
    ///     Create a new instance of <see cref="CancelableItemActionEventArgs" /> with the specified item.
    /// </summary>
    /// <param name="item"></param>
    public CancelableItemActionEventArgs(IStorable item)
    {
        Item = item;
    }

    /// <summary>
    ///     Whether the action should be canceled.
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    ///     Item that the action is being performed on.
    /// </summary>
    public IStorable Item { get; init; }
}
