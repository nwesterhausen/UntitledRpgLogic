namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the specific classification of how an ability is delivered or targeted.
/// </summary>
public enum TargetType
{
	/// <summary>
	///     Represents an unassigned or undefined target type.
	/// </summary>
	None = 0,

	/// <summary>
	///     Affects an area (Area of Effect).
	/// </summary>
	Aoe = 1,

	/// <summary>
	///     Travels toward a target.
	/// </summary>
	Projectile = 2,

	/// <summary>
	///     Requires direct contact.
	/// </summary>
	Touch = 3,

	/// <summary>
	///     The ability applies only to the caster.
	/// </summary>
	Self = 4
}
