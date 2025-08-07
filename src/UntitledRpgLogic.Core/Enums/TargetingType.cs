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
#pragma warning disable CA1720
	Single = 1,
#pragma warning restore CA1720

	/// <summary>
	///     Targets an area, affecting all entities within that area.
	/// </summary>
	AreaOfEffect = 2,

	/// <summary>
	///     Requires physical touch to apply the effect.
	/// </summary>
	Touch = 3,

	/// <summary>
	///     Targets the caster or user of the effect.
	/// </summary>
	Self = 4,

	/// <summary>
	///		Targets multiple specific entities, which may include allies or enemies.
	/// </summary>
	Multiple = 5
}
