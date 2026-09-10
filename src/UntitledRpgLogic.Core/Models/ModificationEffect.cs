using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Interfaces.Data;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Database catalog model defining numerical stat adjustments (buffs or debuffs)
///     applied by a modifier template.
/// </summary>
[Table("modification_effects")]
public record ModificationEffect : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ModificationEffect" /> record for EF Core.
	/// </summary>
	public ModificationEffect()
	{
		this.Id = Ulid.NewUlid();
		this.Positive = true;
	}

	/// <summary>
	///     Creates a new modification effect record populated from specified options.
	/// </summary>
	/// <param name="options">Configuration options providing modification values.</param>
	public ModificationEffect(ModifierEffectOptions options) : this()
	{
		ArgumentNullException.ThrowIfNull(options);

		this.FlatAmount = options.FlatAmount ?? 0;
		this.Percentage = options.Percentage ?? 0f;
		this.PercentageOfMax = options.PercentageOfMax ?? 0f;
		this.Positive = options.IsPositive ?? true;
	}

	/// <summary>
	///     The unique identifier for this modification effect definition.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     A flat numerical value added to or subtracted from the target stat.
	/// </summary>
	public int FlatAmount { get; init; }

	/// <summary>
	///     A fractional percentage multiplier (0.0 to 1.0) applied to the stat value.
	/// </summary>
	public float Percentage { get; init; }

	/// <summary>
	///     A fractional percentage multiplier (0.0 to 1.0) calculated from the stat's maximum ceiling.
	/// </summary>
	public float PercentageOfMax { get; init; }

	/// <summary>
	///     Indicates whether this effect is beneficial (buff) or detrimental (debuff).
	/// </summary>
	public bool Positive { get; init; }
}
