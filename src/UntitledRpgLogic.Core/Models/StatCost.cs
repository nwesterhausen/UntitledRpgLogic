using System.ComponentModel.DataAnnotations;

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
	///     The (FK) id of the ability that consumes this stat cost.
	/// </summary>
	public Ulid AbilityId { get; set; }

	/// <summary>
	///     A navigation property to the ability that consumes this stat cost.
	/// </summary>
	public virtual Ability Ability { get; set; } = null!;

	/// <summary>
	///     Gets or sets the Stat definition (e.g., Mana, Stamina) being affected (Referenced by <see cref="Ulid" />).
	/// </summary>
	public Ulid AffectedStatId { get; set; }

	/// <summary>
	///     Gets or sets the amount consumed.
	/// </summary>
	public float Amount { get; set; }
}
