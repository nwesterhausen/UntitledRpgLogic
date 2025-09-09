namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the categories of requirements needed for various interactions.
/// </summary>
public enum RequirementType
{
	None = 0,
	Stat = 1,
	SkillLevel = 2,
	PlayerLevel = 3,
	Race = 4,
	Class = 5,
	Profession = 6,

	/// <summary>
	///     Requirement specifying that another specific ability/effect must be active.
	/// </summary>
	OngoingSpell = 7
}
