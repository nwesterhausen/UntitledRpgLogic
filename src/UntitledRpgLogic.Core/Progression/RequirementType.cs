namespace UntitledRpgLogic.Core.Progression;

/// <summary>
///     Defines the categories of requirements needed for various interactions.
/// </summary>
public enum RequirementType
{
	/// <summary>
	///     No requirement, default value.
	/// </summary>
	None = 0,

	/// <summary>
	///     Requirement specifying that a specific stat must be at or above a certain value.
	/// </summary>
	Stat = 1,

	/// <summary>
	///     Requirement specifying that a specific skill must be at or above a certain level.
	/// </summary>
	SkillLevel = 2,

	/// <summary>
	///     Requirement specifying that the player must be at or above a certain level.
	/// </summary>
	PlayerLevel = 3,

	/// <summary>
	///     Requirement specifying that the player must be of a certain race level.
	/// </summary>
	RaceLevel = 4,

	/// <summary>
	///     Requirement specifying that the player must be of a certain class level.
	/// </summary>
	ClassLevel = 5,

	/// <summary>
	///     Requirement specifying that the player must be of a certain profession and at least a specific level in that profession.
	/// </summary>
	ProfessionLevel = 6,

	/// <summary>
	///     Requirement specifying that another specific ability/effect must be active.
	/// </summary>
	OngoingSpell = 7,

	/// <summary>
	///     Requirement specifying that the player must be of a certain race (implicit RaceLevel >= 1).
	/// </summary>
	Race = RaceLevel,

	/// <summary>
	///     Requirement specifying that the player must be of a certain class (implicit ClassLevel >= 1).
	/// </summary>
	Class = ClassLevel,

	/// <summary>
	///     Requirement specifying that the player must be of a certain profession (implicit Professionlevel >= 1)
	/// </summary>
	Profession = ProfessionLevel,
}
