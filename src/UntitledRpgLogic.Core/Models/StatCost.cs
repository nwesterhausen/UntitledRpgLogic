using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a specific cost applied to a caster stat when the ability is used.
/// </summary>
/// <remarks>(Owned by <see cref="Ability" />).</remarks>
public class StatCost
{
	/// <summary>
	///     The unique identifier for this record.
	/// </summary>
	[Key]
	public int Id { get; set; }

	/// <summary>
	///     Gets or sets the Stat definition (e.g., Mana, Stamina) being affected (Referenced by <see cref="Ulid" />).
	/// </summary>
	public Ulid AffectedStatId { get; set; }

	/// <summary>
	/// 	The affected stat link
	/// </summary>
	[ForeignKey(nameof(AffectedStatId))]
	public StatDefinition? AffectedStat { get; init; }

	/// <summary>
	///     Gets or sets the amount consumed.
	/// </summary>
	public float Amount { get; set; }
}
