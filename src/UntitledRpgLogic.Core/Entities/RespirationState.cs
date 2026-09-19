namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Represents the state of respiration for an entity that requires breathing.
/// </summary>
public enum RespirationState
{
	/// <summary>
	///     Breathing normally
	/// </summary>
	Normal = 0,

	/// <summary>
	///     Unable to breathe because there is not enough of the required material, or too little atmostpheric pressure.
	/// </summary>
	Suffocation = 1,

	/// <summary>
	///     Unable to breathe because there is too much of the required material, or too much atmospheric pressure.
	/// </summary>
	Asphyxiation = 2,

	/// <summary>
	///     Poisoned by a toxic material being breathed
	/// </summary>
	Poisoned = 3,

	/// <summary>
	///     Lethally poisoned by a toxic material being breathed
	/// </summary>
	LethallyPoisoned = 4
}
