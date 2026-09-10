namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the classification of damage.
/// </summary>
public enum DamageType
{
	/// <summary>
	///     Represents an unassigned or undefined damage type. Should not be used for valid data.
	/// </summary>
	None = 0,

	/// <summary>
	///     Immediately applied damage.
	/// </summary>
	Immediate = 1,

	/// <summary>
	///     Damage applied over a span of time. This means it is applied over the parent effect's duration.
	/// </summary>
	OverTime = 2,

	/// <summary>
	///     Damage the applies after a set delay.
	/// </summary>
	Delayed = 3
}
