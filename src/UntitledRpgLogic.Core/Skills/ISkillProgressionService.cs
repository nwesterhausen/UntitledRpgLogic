using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Core.Skills;

/// <summary>
///     Pure domain service for calculating skill experience requirements, level curves, and advancement thresholds.
/// </summary>
public interface ISkillProgressionService
{
	/// <summary>
	///     Calculates the total experience required to advance from the current level to the next.
	/// </summary>
	public int CalculateExperienceForNextLevel(Skill skill, LevelingDefinition levelingDefinition);

	/// <summary>
	///     Evaluates whether adding experience causes level advancements, mutating the skill instance in-memory.
	/// </summary>
	/// <param name="skill">The active skill instance to mutate.</param>
	/// <param name="levelingDefinition">The curve formula governing this skill.</param>
	/// <param name="addedXp">Amount of experience to grant.</param>
	/// <param name="levelsGained">The number of levels crossed during this progression step.</param>
	/// <returns>True if at least one level was gained; otherwise, false.</returns>
	public bool TryAddExperience(Skill skill, LevelingDefinition levelingDefinition, int addedXp, out int levelsGained);
}
