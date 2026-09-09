using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents an active stat instance bound to an entity, derived from a <see cref="StatDefinition" />.
/// </summary>
[Table("instanced_stats")]
public record InstancedStat : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedStat" /> record with default values.
	/// </summary>
	public InstancedStat()
	{
		this.Id = Ulid.NewUlid();
		this.StatDefinitionId = Ulid.Empty;
		this.BaseValue = 0;
		this.ApparentValue = 0;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="InstancedStat" /> record referencing a definition.
	/// </summary>
	/// <param name="statDefinitionId">The identifier of the governing stat template.</param>
	public InstancedStat(Ulid statDefinitionId) : this() => this.StatDefinitionId = statDefinitionId;

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
}
