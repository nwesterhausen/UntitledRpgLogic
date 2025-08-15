using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an applied modifier instance on a specific entity, including stack count and timing.
/// </summary>
public record AppliedModifier
{
	/// <summary>
	///     Primary key (ULID) for the applied modifier instance.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

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
}
