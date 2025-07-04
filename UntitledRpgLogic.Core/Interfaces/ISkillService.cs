namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines the contract for a service that manages and operates on Skill objects.
/// </summary>
public interface ISkillService : IChangeableValueService<ISkill>
{
    // AddPoints and SetPoints are now inherited from IChangeableValueService<ISkill>

    /// <summary>
    ///     Calculates the total experience points required to reach a specific level.
    /// </summary>
    int GetPointsForLevel(ISkill skill, int level);

    /// <summary>
    ///     Calculates the remaining experience points needed to reach the next level.
    /// </summary>
    /// <returns>The points needed, or 0 if the skill is at max level.</returns>
    int GetPointsToNextLevel(ISkill skill);
}
