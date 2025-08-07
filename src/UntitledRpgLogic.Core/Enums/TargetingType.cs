namespace UntitledRpgLogic.Core.Enums;

/// <summary>
///     Defines how an effect targets entities or areas.
/// </summary>
public enum TargetingType
{
	/// <summary>
	///     No specific targeting type defined.
	/// </summary>
	None = 0,

	/// <summary>
	///     Targets a single entity.
	/// </summary>
	SingleTarget = 1,

	/// <summary>
	///		Targets multiple specific entities, which may include allies or enemies.
	/// </summary>
	MultipleTarget = 2,

	/// <summary>
	///     Targets an area, affecting all entities within that area.
	/// </summary>
	AreaOfEffect = 3,

	/// <summary>
	///     Targets the caster or user of the effect.
	/// </summary>
	/// <remarks>This implies a single target.</remarks>
	Self = 4,

	/// <summary>
	///     Requires physical touch to apply the effect.
	/// </summary>
	/// <remarks>This implies a single target.</remarks>
	Touch = 5,
}
