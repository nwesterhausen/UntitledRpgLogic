using UntitledRpgLogic.CompositionBehaviors;

namespace UntitledRpgLogic.Classes;

/// <summary>
///     Options for configuring the item storage behavior.
/// </summary>
public class ItemStorageOptions
{
    /// <summary>
    ///     Delegate to determine if an item can be stored in the inventory.
    /// </summary>
    public ItemStorageBehavior.AbleToStoreItem? AbleToStoreItem { get; set; }

    /// <summary>
    ///     Delegate to calculate the storage usage of the inventory.
    /// </summary>
    public ItemStorageBehavior.CalculateItemStorageUsage? CalculateItemStorageUsage { get; set; }

    /// <summary>
    ///     Whether the inventory has limited storage capacity for items.
    /// </summary>
    public bool HasLimitedStorage { get; set; } = true;
}