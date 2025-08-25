using System.Collections.ObjectModel;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Effects;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     A data container for a stat and its related modifiers, mitigations, and damage state.
///     The logic for operating on this data is handled by an IStatService.
/// </summary>
/// <typeparam name="T">The stat contained within this entry</typeparam>
public class StatEntry<T> : IDamageable where T : IStat
{
	/// <summary>
	/// </summary>
	/// <param name="stat"></param>
	/// <param name="isDamageable"></param>
	/// <param name="isHealable"></param>
	public StatEntry(T stat, bool isDamageable = false, bool isHealable = false)
	{
		if (isHealable && !isDamageable)
		{
			throw new ArgumentException("A stat cannot be healable if it is not damageable.", nameof(isHealable));
		}

		this.Stat = stat;
		this.IsDamageable = isDamageable;
		this.IsHealable = isHealable;
	}

	/// <summary>The stat that this entry represents.</summary>
	public T Stat { get; }

	/// <summary>Whether the stat is damageable or not.</summary>
	public bool IsDamageable { get; }

	/// <summary>Whether the stat can be healed or not.</summary>
	public bool IsHealable { get; }

	/// <summary>A list of mitigations that apply to this stat entry.</summary>
	public Collection<IAppliesDamageMitigation> Mitigations { get; } = [];

	/// <summary>A list of modifiers that apply to this stat entry.</summary>
	public Collection<IModifier> Modifiers { get; } = [];

	/// <summary>
	///     A helper to calculate what percentage of the stat's maximum value a given point value represents.
	/// </summary>
	public float PointsAsPercentageOfMax(int pointValue)
	{
		if (this.Stat.MaxValue <= 0)
		{
			return 0f;
		}

		return pointValue / (float)this.Stat.MaxValue * 100f;
	}

	#region IDamageable Implementation

	// Explicitly implement the interface property to avoid confusion.
	IStat IDamageable.Stat => this.Stat;

	/// <inheritdoc />
	public int CurrentDamage { get; set; }

	/// <inheritdoc />
	public float CurrentPercentageDamage { get; set; }

	#endregion
}
