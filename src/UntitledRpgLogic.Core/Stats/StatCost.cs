using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Core.Stats;

/// <summary>
///     Defines a resource cost (e.g., Mana, Stamina, Health) deducted when invoking an ability.
/// </summary>
/// <remarks>Owned by <see cref="AbilityDefinition" />.</remarks>
[Table("ability_stat_costs")]
public record StatCost
{
	/// <summary>
	///     The unique database row identifier for the cost entry.
	/// </summary>
	[Key]
	public int Id { get; init; }

	/// <summary>
	///     Foreign key referencing the target <see cref="StatDefinition" /> consumed by the cost.
	/// </summary>
	public Ulid StatId { get; init; }

	/// <summary>
	///     Navigation property to the consumed stat definition.
	/// </summary>
	[ForeignKey(nameof(StatId))]
	public StatDefinition? Stat { get; init; }

	/// <summary>
	///     The resource quantity required and consumed upon activation.
	/// </summary>
	public float Amount { get; set; }
}
