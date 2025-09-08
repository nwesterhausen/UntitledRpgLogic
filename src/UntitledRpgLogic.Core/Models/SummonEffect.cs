using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     An effect that summons a creature or object.
/// </summary>
public class SummonEffect : Effect
{
	/// <summary>
	///     An effect that summons a creature or object.
	/// </summary>
	public SummonEffect() => this.EffectType = EffectType.Summon;

	/// <summary>
	///     Gets or sets the ULID of the Mob or Object definition to summon.
	/// </summary>
	public Ulid EntityToSummonId { get; set; }

	/// <summary>
	///     Gets or sets the number of entities to summon.
	/// </summary>
	public int Quantity { get; set; } = 1;
}
