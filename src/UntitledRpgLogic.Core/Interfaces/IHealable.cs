namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Interface for a stat that can be healed.
/// </summary>
public interface IHealable : IDamageable
{
	/// <summary>
	///     Heals the stat by the specified amount by decreasing the <see cref="IDamageable.CurrentDamage" />.
	/// </summary>
	/// <param name="amount"></param>
	public void Heal(int amount);

	/// <summary>
	///     Event raised when haling is applied to the stat. The event should notify of the total amount of healing.
	/// </summary>
	public event EventHandler<int> Healed;
}
