using UntitledRpgLogic.Core.Interfaces.Common;

namespace UntitledRpgLogic.Core.Interfaces.Entities;

/// <summary>
///     Interface for
/// </summary>
public interface IHasSkills
{
	/// <summary>
	///     The collection of skills that this entity has.
	/// </summary>
	public IEnumerable<ISkill> Skills { get; }

	/// <summary>
	///     Add a skill to the entity.
	/// </summary>
	/// <param name="skill"></param>
	public void AddSkill(ISkill skill);

	/// <summary>
	///     Remove a skill from the entity.
	/// </summary>
	/// <param name="skillId"></param>
	public void RemoveSkill(Guid skillId);

	/// <summary>
	///     Add skill points to a skill of the entity. If the skill does not exist, this will do nothing.
	/// </summary>
	/// <param name="skillId"></param>
	/// <param name="points"></param>
	public void AddSKillPoints(Guid skillId, int points = 1);

	/// <summary>
	///     Remove skill points from a skill of the entity. If the skill does not exist, this will do nothing.
	/// </summary>
	/// <param name="skillId"></param>
	/// <param name="points"></param>
	public void RemoveSKillPoints(Guid skillId, int points = 1);

	/// <summary>
	///     Whether the entity has a skill with the given ID.
	/// </summary>
	/// <param name="skillId"></param>
	/// <returns></returns>
	public bool HasSkill(Guid skillId);

	/// <summary>
	///     Get a skill by its ID. If the skill does not exist, this returns false and the out parameter is null.
	/// </summary>
	/// <param name="skillId"></param>
	/// <param name="skill"></param>
	/// <returns></returns>
	public bool TryGetSkill(Guid skillId, out ISkill? skill);
}
