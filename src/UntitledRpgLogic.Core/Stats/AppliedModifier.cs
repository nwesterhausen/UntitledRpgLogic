using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Entities;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Represents an active modifier instance bound to an entity, tracking stack counts and expiration timing.
/// </summary>
[Table("applied_modifiers")]
public record AppliedModifier : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AppliedModifier" /> record for EF Core.
	/// </summary>
	public AppliedModifier()
	{
		this.ModifierDefinitionId = Ulid.Empty;
		this.EntityId = Ulid.Empty;
		this.Stacks = 1;
		this.AppliedAt = DateTimeOffset.UtcNow;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="AppliedModifier" /> record for a specific entity.
	/// </summary>
	/// <param name="modifierDefinitionId">The identifier of the modifier definition being applied.</param>
	/// <param name="entityId">The identifier of the entity receiving the modifier.</param>
	public AppliedModifier(Ulid modifierDefinitionId, Ulid entityId) : this()
	{
		this.ModifierDefinitionId = modifierDefinitionId;
		this.EntityId = entityId;
	}

	/// <summary>
	///     Foreign key referencing the template <see cref="ModifierDefinition" />.
	/// </summary>
	public required Ulid ModifierDefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the underlying modifier definition.
	/// </summary>
	[ForeignKey(nameof(ModifierDefinitionId))]
	public ModifierDefinition? ModifierDefinition { get; init; }

	/// <summary>
	///     Foreign key referencing the target <see cref="Entity" /> bearing this modifier.
	/// </summary>
	public required Ulid EntityId { get; init; }

	/// <summary>
	///     Navigation property to the target entity.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     Current active stack count.
	/// </summary>
	[Range(1, int.MaxValue)]
	public int Stacks { get; set; }

	/// <summary>
	///     Timestamp when the modifier was initially applied.
	/// </summary>
	public DateTimeOffset AppliedAt { get; init; }

	/// <summary>
	///     Timestamp when the modifier expires (null for permanent or indefinite modifiers).
	/// </summary>
	public DateTimeOffset? ExpiresAt { get; set; }

	/// <summary>
	///     Primary key for the applied modifier instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; } = Ulid.NewUlid();
}
