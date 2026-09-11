using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Represents an active stat instance bound to an entity, derived from a <see cref="StatDefinition" />.
/// </summary>
[Table("instanced_stats")]
public record Stat : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="Stat" /> record with default values.
	/// </summary>
	public Stat()
	{
		this.Id = Ulid.NewUlid();
		this.StatDefinitionId = Ulid.Empty;
		this.BaseValue = 0;
		this.ApparentValue = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="Stat" /> record referencing a definition.
	/// </summary>
	/// <param name="statDefinitionId">The identifier of the governing stat template.</param>
	public Stat(Ulid statDefinitionId) : this() => this.StatDefinitionId = statDefinitionId;

	/// <summary>
	///     The unique identifier for this active stat instance.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     Foreign key referencing the template <see cref="StatDefinition" />.
	/// </summary>
	public Ulid StatDefinitionId { get; init; }

	/// <summary>
	///     Navigation property to the underlying stat definition.
	/// </summary>
	[ForeignKey(nameof(StatDefinitionId))]
	public StatDefinition? StatDefinition { get; init; }

	/// <summary>
	///     The raw base value before temporary status modifiers or equipment multipliers.
	/// </summary>
	public int BaseValue { get; set; }

	/// <summary>
	///     The active apparent value including all current buffs, debuffs, and item modifiers.
	/// </summary>
	public int ApparentValue { get; set; }

	/// <summary>
	///		The "effective" value, i.e. the value transposed above its minium.
	/// </summary>
	public int EffectiveValue => this.ApparentValue - this.StatDefinition?.MinValue ?? 0;
}
