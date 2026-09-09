namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Contract for animate agents capable of combat, progression, and learning.
/// </summary>
public interface IActor : IEntity, IHasStats, IHasSkills, IHasModifiers, IHasInventory
{
}
