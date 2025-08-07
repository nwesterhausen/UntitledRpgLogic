namespace UntitledRpgLogic.Core.Events;

/// <summary>
///     Provides data for events related to damage calculation and mitigation for a stat.
/// </summary>
public class StatDamageEventArgs : EventArgs
{
	/// <summary>
	///     Gets the unique identifier of the source that caused the damage, if available.
	/// </summary>
	public Guid? SourceId { get; init; }

	/// <summary>
	/// The Guid of the MagicTypeDataConfig that defines the type of damage.
	/// Can be null for physical damage.
	/// </summary>
	public Guid? MagicType { get; }

	/// <summary>
	///     Gets the amount of damage before any mitigation is applied.
	/// </summary>
	public int IncomingDamage { get; init; }

	/// <summary>
	///     Gets the amount of damage after all mitigation is applied.
	/// </summary>
	public int FinalDamage { get; init; }

	/// <summary>
	///     Gets the percentage representation of the incoming damage.
	/// </summary>
	public float IncomingDamagePercentage { get; init; }

	/// <summary>
	///     Gets the percentage representation of the final damage after mitigation.
	/// </summary>
	public float FinalDamagePercentage { get; init; }

	/// <summary>
	///     Gets the name of the stat affected by the damage.
	/// </summary>
	public string StatName { get; init; } = string.Empty;

	/// <summary>
	///     Gets the total amount of damage mitigated.
	/// </summary>
	public int AmountMitigated => this.IncomingDamage - this.FinalDamage;

	/// <summary>
	///     Gets the percentage of damage mitigated.
	/// </summary>
	public float AmountMitigatedPercentage => this.IncomingDamagePercentage - this.FinalDamagePercentage;
}
