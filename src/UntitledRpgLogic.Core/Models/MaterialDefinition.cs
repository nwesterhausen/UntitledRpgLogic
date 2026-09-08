using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Interfaces.Data;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Minimal material definition for persistence. Instances of items can reference materials by this ULID.
///     Additional physical properties can be modeled later as separate tables or owned types.
/// </summary>
public record MaterialDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Constructs a new <see cref="MaterialDefinition" /> with an empty name and default values. (for EF use)
	/// </summary>
	public MaterialDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.StateProperties = new List<StateSpecificProperties>();
	}

	/// <summary>
	///     Constructs a new <see cref="MaterialDefinition" /> with the specified name.
	/// </summary>
	/// <param name="name">The display name for the material.</param>
	public MaterialDefinition(Name name)
	{
		this.Id = Ulid.NewUlid();
		this.Name = name;
		this.StateProperties = new List<StateSpecificProperties>();
	}

	/// <summary>
	///     Constructs a new <see cref="MaterialDefinition" /> with the specified name.
	/// </summary>
	/// <param name="name">The display name for the material.</param>
	/// <param name="stateProperties">The properties of the material at various states of matter</param>
	public MaterialDefinition(Name name, ICollection<StateSpecificProperties> stateProperties)
	{

		this.Id = Ulid.NewUlid();
		this.Name = name;
		this.StateProperties = stateProperties;
	}

	/// <summary>
	///     Display name for the material.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     Primary key for the material definition.
	/// </summary>
	[Key]
	public Ulid Id { get; init; }

	/// <summary>
	/// 	Mechanical properties of the material in all states
	/// </summary>
	public MechanicalProperties? MechanicalProperties { get; set; }
	/// <summary>
	/// 	Thermal properties of the material in all states
	/// </summary>
	public ThermalProperties? ThermalProperties { get; set; }
	/// <summary>
	/// 	Electrical properties of the material in all states
	/// </summary>
	public ElectricalProperties? ElectricalProperties { get; set; }
	/// <summary>
	/// 	Fantastical properties of the material in all states
	/// </summary>
	public FantasticalProperties? FantasticalProperties { get; set; }
	/// <summary>
	/// 	Properties dependent on the material being in a specific state of matter
	/// </summary>
	public ICollection<StateSpecificProperties> StateProperties { get; init; }
}
