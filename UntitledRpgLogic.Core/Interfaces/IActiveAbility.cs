namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     And ability that can be activated by the player, performing an action such as attacking, casting a spell, or using
///     a skill.
/// </summary>
public interface IActiveAbility : IActiveEffect
{
    /// <summary>
    ///     Activates the spell ability with specific context.
    /// </summary>
    new void Activate();
}
