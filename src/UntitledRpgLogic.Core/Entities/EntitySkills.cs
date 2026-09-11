using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Skills;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Join record linking an <see cref="Entity" /> to its learned <see cref="InstancedSkill" /> instances.
/// </summary>
[Table("entity_skills")]
public record EntitySkills
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntitySkills" /> record for EF Core.
	/// </summary>
	public EntitySkills()
	{
		this.EntityId = Ulid.Empty;
		this.InstancedSkillId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntitySkills" /> record with explicit keys.
	/// </summary>
	/// <param name="entityId">The identifier of the entity owning the skill.</param>
	/// <param name="instancedSkillId">The identifier of the instanced skill.</param>
	public EntitySkills(Ulid entityId, Ulid instancedSkillId)
	{
		this.EntityId = entityId;
		this.InstancedSkillId = instancedSkillId;
	}

	/// <summary>
	///     Foreign key of the owning entity (Composite PK Part 1).
	/// </summary>
	public Ulid EntityId { get; init; }

	/// <summary>
	///     Navigation property to the owning entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     Foreign key of the associated instanced skill (Composite PK Part 2).
	/// </summary>
	public Ulid InstancedSkillId { get; init; }

	/// <summary>
	///     Navigation property to the instanced skill.
	/// </summary>
	[ForeignKey(nameof(InstancedSkillId))]
	public Skill? InstancedSkill { get; init; }
}
