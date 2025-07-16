using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Events;
using UntitledRpgLogic.Core.Options;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Defines the contract for a service that manages and operates on StatEntry objects.
/// </summary>
public interface IStatService : IChangeableValueService<IStat>
{
	/// <summary>
	///     Raised when a stat takes damage after all calculations.
	/// </summary>
	public event EventHandler<StatDamageEventArgs> StatDamageTaken;

	/// <summary>
	///     Raised when a stat is healed.
	/// </summary>
	public event EventHandler<StatHealEventArgs> StatHealed;

	/// <summary>
	///     Applies damage to a stat entry, calculating final damage based on mitigations.
	/// </summary>
	/// <param name="statEntry">The stat entry to apply damage to.</param>
	/// <param name="damageOptions">The options describing the incoming damage.</param>
	public void ApplyDamage(StatEntry<IStat> statEntry, DamageOptions damageOptions);

	/// <summary>
	///     Applies healing to a stat entry, reducing its current damage.
	/// </summary>
	/// <param name="statEntry">The stat entry to heal.</param>
	/// <param name="healOptions">The options describing the incoming heal.</param>
	public void Heal(StatEntry<IStat> statEntry, HealOptions healOptions);

	/// <summary>
	///     Adds a modifier to a stat entry and recalculates the stat's value.
	/// </summary>
	/// <param name="statEntry">The stat entry to modify.</param>
	/// <param name="modifier">The modifier to add.</param>
	public void AddModifier(StatEntry<IStat> statEntry, IModifier modifier);

	/// <summary>
	///     Removes a modifier and recalculates the stat's value.
	/// </summary>
	/// <param name="statEntry">The stat entry to modify.</param>
	/// <param name="modifier">The modifier to remove.</param>
	public void RemoveModifier(StatEntry<IStat> statEntry, IModifier modifier);

	/// <summary>
	///     Adds a damage mitigation to a stat entry.
	/// </summary>
	/// <param name="statEntry">The stat entry to add mitigation to.</param>
	/// <param name="mitigation">The mitigation to add.</param>
	public void AddMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation);

	/// <summary>
	///     Removes a damage mitigation from a stat entry.
	/// </summary>
	/// <param name="statEntry">The stat entry to remove mitigation from.</param>
	/// <param name="mitigation">The mitigation to remove.</param>
	public void RemoveMitigation(StatEntry<IStat> statEntry, IAppliesDamageMitigation mitigation);

	/// <summary>
	///     Links a dependent stat to a source stat, causing the dependent stat's BaseValue
	///     to change proportionally when the source stat's Value changes.
	/// </summary>
	/// <param name="sourceStat">The stat that will trigger the change.</param>
	/// <param name="dependentStat">The stat that will be affected by the change.</param>
	/// <param name="ratio">The ratio of the source's change to apply to the dependent stat.</param>
	public void LinkStats(IStat sourceStat, IStat dependentStat, float ratio);
}
