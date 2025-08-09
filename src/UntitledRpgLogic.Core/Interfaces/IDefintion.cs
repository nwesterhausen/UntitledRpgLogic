namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     A marker interface representing the definition of a game object (e.g., Stat, Skill, Item).
///     Definitions are immutable data records loaded from configuration.
/// </summary>
public interface IDefinition : ITomlConfig
{
}
