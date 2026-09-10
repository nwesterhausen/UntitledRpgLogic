using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database catalog model defining an ongoing modifier (buff or debuff) that can be applied to entities.
/// </summary>
[Table("modifier_definitions")]
public record ModifierDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ModifierDefinition" /> record with default values for EF Core.
	/// </summary>
	public ModifierDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.MaxStacks = 1;
		this.LoseAllStacksOnExpiration = true;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ModifierDefinition" /> record with a designated name.
	/// </summary>
	/// <param name="name">The display name of the modifier.</param>
	public ModifierDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the modifier definition.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The display name of the modifier.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Indicates whether the modifier persists indefinitely until explicitly dispelled.
	/// </summary>
	public bool IsPermanent { get; init; }

	/// <summary>
	///     Indicates whether the modifier is beneficial (true) or detrimental (false).
	/// </summary>
	public bool IsPositive { get; init; }

	/// <summary>
	///     Indicates whether the modifier calculates via flat addition.
	/// </summary>
	public bool IsAdditive { get; init; }

	/// <summary>
	///     Indicates whether the modifier scales multiplicatively.
	/// </summary>
	public bool IsMultiplicative { get; init; }

	/// <summary>
	///     Indicates whether scaling operates against the stat's base value (true) or current apparent value (false).
	/// </summary>
	public bool ScalesOnBaseValue { get; init; }

	/// <summary>
	///     The maximum number of concurrent stacks an entity can hold.
	/// </summary>
	[Range(1, int.MaxValue)]
	public int MaxStacks { get; init; }

	/// <summary>
	///     The active duration in seconds (ignored if <see cref="IsPermanent" /> is true).
	/// </summary>
	public float Duration { get; init; }

	/// <summary>
	///     Indicates whether all stacks clear simultaneously upon expiration or decrement individually.
	/// </summary>
	public bool LoseAllStacksOnExpiration { get; init; }

	/// <summary>
	///     Priority order used when evaluating multiple stacked modifiers on the same stat.
	/// </summary>
	public int Priority { get; init; }

	/// <summary>
	///     Foreign key to the baseline <see cref="ModificationEffect" /> active at 0 or base stacks.
	/// </summary>
	public Ulid? ModifierEffectId { get; init; }

	/// <summary>
	///     Navigation property to the baseline modification effect.
	/// </summary>
	[ForeignKey(nameof(ModifierEffectId))]
	public ModificationEffect? ModifierEffect { get; init; }

	/// <summary>
	///     Foreign key to the supplemental <see cref="ModificationEffect" /> applied per additional stack.
	/// </summary>
	public Ulid? StackEffectId { get; init; }

	/// <summary>
	///     Navigation property to the per-stack modification effect.
	/// </summary>
	[ForeignKey(nameof(StackEffectId))]
	public ModificationEffect? StackEffect { get; init; }
}
