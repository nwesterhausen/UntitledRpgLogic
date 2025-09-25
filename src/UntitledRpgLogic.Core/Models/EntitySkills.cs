using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Used to track the skills of an entity, such as a player or NPC.
/// </summary>
public class EntitySkills
{
	/// <summary>
	///     Initializes a new instance of the <see cref="EntitySkills" /> class.
	///     This parameterless constructor is required by Entity Framework Core for materialization.
	/// </summary>
	public EntitySkills()
	{
		this.EntityId = Ulid.Empty;
		this.InstancedSkillId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="EntitySkills" /> class with the specified entity ID
	///     and item instance ID.
	/// </summary>
	/// <param name="entityId">The unique identifier of the entity owning the item instance.</param>
	/// <param name="instancedSkillId">The unique identifier of the skill belonging by the entity.</param>
	public EntitySkills(Ulid entityId, Ulid instancedSkillId)
	{
		this.EntityId = entityId;
		this.InstancedSkillId = instancedSkillId;
	}

	/// <summary>
	///     An entity's unique identifier. This is used to reference the entity in the game and in the database.
	/// </summary>
	public required Ulid EntityId { get; init; }

	/// <summary>
	///     A unique identifier for a skill that has been instanced for an entity.
	/// </summary>
	public required Ulid InstancedSkillId { get; init; }

	/// <summary>
	///     The entity that this skill belongs to. This is used to link the skill to the entity it belongs to, such as a player
	///     or NPC.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     The instanced skill that this entity has.
	/// </summary>
	[ForeignKey(nameof(InstancedSkillId))]
	public InstancedSkill? InstancedSkill { get; init; }
}
