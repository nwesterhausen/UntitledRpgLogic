namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
/// </summary>
public interface IActiveSpellEffect : IActiveAbility
{
    /// <summary>
    /// </summary>
    /// <param name="caster"></param>
    /// <param name="targets"></param>
    void Activate(IEntity? caster, IEnumerable<IEntity>? targets);
}
