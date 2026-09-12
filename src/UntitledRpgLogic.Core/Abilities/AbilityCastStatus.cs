namespace UntitledRpgLogic.Core.Abilities;

/// <summary>
///     The status result of casting an ability.
/// </summary>
public enum AbilityCastStatus
{
	/// <summary>
	///     The ability is cast successfully.
	/// </summary>
	Success = 0,

	/// <summary>
	///     Not enough stat resources to cast.
	/// </summary>
	InsufficientResources = 1,

	/// <summary>
	///     The requirements to cast were not met.
	/// </summary>
	CastingRequirementsUnmet = 2,

	/// <summary>
	///     Failure from the target being out of range.
	/// </summary>
	TargetOutOfRange = 3,

	/// <summary>
	///     Failure from choosing an invalid target.
	/// </summary>
	InvalidTarget = 4,

	/// <summary>
	///     Failure from being interrupted during casting.
	/// </summary>
	Interrupted = 5,

	/// <summary>
	///     Failure from an ability backfire.
	/// </summary>
	Backfired = 6
}
