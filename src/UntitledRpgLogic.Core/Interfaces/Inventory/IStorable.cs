using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Interfaces.Inventory;

/// <summary>
///     Something that can be stored in an inventory.
/// </summary>
public interface IStorable : IHasIdentifier, IHasName, IHasDimensions
{
}
