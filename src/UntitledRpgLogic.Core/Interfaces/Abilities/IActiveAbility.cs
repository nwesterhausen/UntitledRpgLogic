namespace UntitledRpgLogic.Core.Interfaces.Abilities;

/// <summary>
///     And ability that can be activated by the player, performing an action such as attacking, casting a spell, or using
///     a skill.
/// </summary>
public interface IActiveAbility : IAbility
{
	/// <summary>
	///     Execute the ability, performing its action.
	/// </summary>
	public void Execute();
}
