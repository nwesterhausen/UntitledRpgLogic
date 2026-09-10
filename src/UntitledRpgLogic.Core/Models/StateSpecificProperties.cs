using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines state-specific property overrides for a material when shifting between phases (Solid, Liquid, Gas).
/// </summary>
/// <remarks>Owned by <see cref="MaterialDefinition" />.</remarks>
public record StateSpecificProperties
{
	/// <summary>
	///     Initializes a new instance of the <see cref="StateSpecificProperties" /> record with default values.
	/// </summary>
	public StateSpecificProperties()
	{
		this.Color = "#FFFFFF";
		this.State = StateOfMatter.None;
	}

	/// <summary>
	///     Hexadecimal color representation of the material in this physical state.
	/// </summary>
	public string Color { get; init; }

	/// <summary>
	///     The specific phase of matter these overrides apply to.
	/// </summary>
	public StateOfMatter State { get; init; }

	/// <summary>
	///     Mechanical property overrides for this state (null if using default material baseline).
	/// </summary>
	public MechanicalProperties? MechanicalProperties { get; init; }

	/// <summary>
	///     Thermal property overrides for this state (null if using default material baseline).
	/// </summary>
	public ThermalProperties? ThermalProperties { get; init; }

	/// <summary>
	///     Electrical property overrides for this state (null if using default material baseline).
	/// </summary>
	public ElectricalProperties? ElectricalProperties { get; init; }

	/// <summary>
	///     Mystical property overrides for this state (null if using default material baseline).
	/// </summary>
	public FantasticalProperties? FantasticalProperties { get; init; }
}
