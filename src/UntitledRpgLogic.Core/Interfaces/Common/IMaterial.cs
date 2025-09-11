using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Interfaces.Common;

/// <summary>
///     Represents a material with its properties and states of matter. Can be used to determine a weight or roughly how
///     a material might behave in different conditions.
/// </summary>
public interface IMaterial : IHasName, IHasIdentifier
{
	/// <summary>
	///     Mechanical properties of the material.
	/// </summary>
	public MechanicalProperties Mechanical { get; }

	/// <summary>
	///     Thermal properties of the material.
	/// </summary>
	public ThermalProperties Thermal { get; }

	/// <summary>
	///     Electrical properties of the material.
	/// </summary>
	public ElectricalProperties Electrical { get; }

	/// <summary>
	///     Fantastical properties of the material.
	/// </summary>
	public FantasticalProperties Fantastical { get; }

	/// <summary>
	///     Properties specific to the material's state of matter.
	/// </summary>
	public IReadOnlyDictionary<StateOfMatter, StateSpecificProperties> States { get; }
}
