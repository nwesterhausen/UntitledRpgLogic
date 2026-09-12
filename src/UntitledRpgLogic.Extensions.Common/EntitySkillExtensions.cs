using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Skills;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extension methods for managing entity skill attachments and values.
/// </summary>
public static class EntitySkillExtensions
{
	/// <summary>
	///     Attaches or updates a skill on an entity, initializing the underlying <see cref="Skill" /> and
	///     <see cref="EntitySkills" /> join record.
	///     New skill defaults to level 0 and 0 experience points.
	/// </summary>
	/// <param name="entity">The target entity.</param>
	/// <param name="definition">The skill template definition.</param>
	/// <param name="initialLevel">The starting level of the skill. Default: 0</param>
	/// <param name="initialExperience">The starting experience points for the skill. Default: 0</param>
	/// <returns>The resulting <see cref="EntitySkills" /> join record.</returns>
	public static EntitySkills LearnSkill(this Entity entity, SkillDefinition definition, int initialLevel = 0,
		int initialExperience = 0)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(definition);

		var existing = entity.Skills.FirstOrDefault(s => s.InstancedSkill?.DefinitionId == definition.Id);
		if (existing is not null)
		{
			if (existing.InstancedSkill is not null)
			{
				existing.InstancedSkill.Level = initialLevel;
				existing.InstancedSkill.ExperiencePoints = initialExperience;
			}

			return existing;
		}

		var instancedStat = new Skill
		{
			Level = initialLevel, ExperiencePoints = initialExperience, DefinitionId = definition.Id
		};

		var join = new EntitySkills(entity.Id, instancedStat.Id) { Entity = entity, InstancedSkill = instancedStat };

		entity.Skills.Add(join);
		return join;
	}

	/// <summary>
	///     Get the level of a particular skill on this entity.
	/// </summary>
	/// <param name="entity">target entity</param>
	/// <param name="skillDefinitionId">The ID of the skill to find</param>
	/// <returns>The skill level or `0` if the entity doesn't know the skill</returns>
	/// <exception cref="ArgumentNullException">Throws if <paramref name="entity" /> is `null`</exception>
	public static int GetSkillLevel(this Entity entity, Ulid skillDefinitionId)
	{
		ArgumentNullException.ThrowIfNull(entity);

		var skill = entity.Skills?
			.FirstOrDefault(s => s.InstancedSkillId == skillDefinitionId)?
			.InstancedSkill;

		return skill?.Level ?? 0;
	}
}
