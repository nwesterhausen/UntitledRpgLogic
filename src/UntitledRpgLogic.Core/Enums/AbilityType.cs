namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the high-level classification of the ability.
/// </summary>
public enum AbilityType
{
	/// <summary>
	///     Represents an unassigned or undefined ability type. Should not be used for valid data.
	/// </summary>
	None = 0,

	/// <summary>
	///     A magical ability (e.g., Fireball).
	/// </summary>
	Spell = 1,

	/// <summary>
	///     A physical or non-magical ability that requires activation (e.g., Shield Bash).
	/// </summary>
	ActiveAbility = 2,

	/// <summary>
	///     An effect that is always active under specific conditions (e.g., Toughness).
	/// </summary>
	PassiveAbility = 3
}
