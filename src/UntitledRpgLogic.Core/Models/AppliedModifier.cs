using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an applied modifier instance on a specific entity, including stack count and timing.
/// </summary>
public record AppliedModifier : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AppliedModifier" /> class.
	///     This parameterless constructor is required by Entity Framework Core for materialization.
	/// </summary>
	public AppliedModifier()
	{
		this.Id = Ulid.NewUlid();
		this.ModifierDefinitionId = Ulid.Empty;
		this.EntityId = Ulid.Empty;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="AppliedModifier" /> class with the specified modifier definition ID
	///     and entity ID.
	/// </summary>
	/// <param name="modifierDefinitionId">The unique identifier of the modifier definition being applied.</param>
	/// <param name="entityId">The unique identifier of the entity receiving the modifier.</param>
	public AppliedModifier(Ulid modifierDefinitionId, Ulid entityId)
	{
		this.Id = Ulid.NewUlid();
		this.ModifierDefinitionId = modifierDefinitionId;
		this.EntityId = entityId;
	}

	/// <summary>
	///     FK to the modifier definition being applied.
	/// </summary>
	public required Ulid ModifierDefinitionId { get; init; }

	/// <summary>
	///     FK to the target entity receiving this modifier.
	/// </summary>
	public required Ulid EntityId { get; init; }

	/// <summary>
	///     Current stack count of the modifier.
	/// </summary>
	public int Stacks { get; init; } = 1;

	/// <summary>
	///     When the modifier was applied.
	/// </summary>
	public DateTimeOffset AppliedAt { get; init; } = DateTimeOffset.UtcNow;

	/// <summary>
	///     When the modifier expires, if applicable.
	/// </summary>
	public DateTimeOffset? ExpiresAt { get; init; }

	/// <summary>
	///     The modifier definition being applied by this instance.
	/// </summary>
	[ForeignKey(nameof(ModifierDefinitionId))]
	public ModifierDefinition? ModifierDefinition { get; init; }

	/// <summary>
	///     The entity that is the target/owner of this applied modifier.
	/// </summary>
	[ForeignKey(nameof(EntityId))]
	public Entity? Entity { get; init; }

	/// <summary>
	///     Primary key (ULID) for the applied modifier instance.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }
}
