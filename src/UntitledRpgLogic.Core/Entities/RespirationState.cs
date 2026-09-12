namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Represents the state of respiration for an entity that requires breathing.
/// </summary>
public enum RespirationState
{
	/// <summary>
	///     Breathing normally
	/// </summary>
	Optimal = 0,

	/// <summary>
	///     Unable to breathe and in danger
	/// </summary>
	Suffocating = 1,

	/// <summary>
	///     Poisioned by some material being breathed
	/// </summary>
	Poisoned = 2,

	/// <summary>
	///     Lethally poisoned by some material being breathed
	/// </summary>
	LethallyPoisoned = 3,

	/// <summary>
	///     Poisoned by being in an enivronment with too much preferred breathing material (e.g. hyperoxia)
	/// </summary>
	OverpressureToxicity = 4
}
