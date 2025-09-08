namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines the core nature of what an Effect payload does.
///     This will be used by EFCore as the "Discriminator" for TPH inheritance.
/// </summary>
public enum EffectType
{
	/// <summary>
	///     An unassigned or undefined effect type.
	/// </summary>
	None = 0,

	/// <summary>
	///     Summons an entity, such as a monster or a player character.
	/// </summary>
	Summon = 1,

	/// <summary>
	///     Imbues one or more effects into an item.
	/// </summary>
	Enchant = 2,

	/// <summary>
	///     Applies a special status to an entity, adjusting the difficulty of certain interactions (could be more or less difficult).
	/// </summary>
	Charm = 3,

	/// <summary>
	///     Heals a stat (typically health), increasing its current value within its min-max bounds.
	/// </summary>
	Heal = 4,

	/// <summary>
	///     Does damage to a stat (typically health), reducing its current value within its min-max bounds.
	/// </summary>
	Damage = 5,

	/// <summary>
	///     Increases the maximum of a specific stat or skill level, bringing up the current value proportionally.
	/// </summary>
	Buff = 6,

	/// <summary>
	///     Lower the maximum of a specific stat or skill level, bringing down the current value proportionally.
	/// </summary>
	Debuff = 7,

	/// <summary>
	///     A specialized effect that modifies an Ambient (e.g., weather, temperature).
	/// </summary>
	Elemental = 8,

	/// <summary>
	///     Moves an entity. Could be as simple as a flash-step to the side or a teleport across the world.
	/// </summary>
	Movement = 9,

	/// <summary>
	///     Transforms an entity, possibly giving extra abilities based on what is changed.
	/// </summary>
	Transformation = 10
}
