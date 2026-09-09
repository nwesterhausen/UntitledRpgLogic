using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Defines an interactive entity capable of holding, carrying, or storing items.
/// </summary>
public interface IHasInventory
{
	/// <summary>
	///     Gets the entity's inventory container.
	/// </summary>
	EntityInventory? Inventory { get; set; }
}
