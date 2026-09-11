using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Core.Materials;

/// <summary>
///     Minimal material definition for persistence. Instances of items can reference materials by this ULID.
///     Additional physical properties are attached by owned property classes.
/// </summary>
[Table("material_definitions")]
public record MaterialDefinition : IDbEntity<Ulid>
{
	/// <summary>
	///     Initializes a new instance of the <see cref="MaterialDefinition" /> record with default values.
	/// </summary>
	public MaterialDefinition()
	{
		this.Id = Ulid.NewUlid();
		this.Name = Name.Empty;
		this.Description = string.Empty;
		this.Flags = MaterialTraits.None;
		this.DefaultState = StateOfMatter.Solid;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="MaterialDefinition" /> record with the specified name.
	/// </summary>
	/// <param name="name">The name of the material.</param>
	public MaterialDefinition(Name name) : this() => this.Name = name;

	/// <summary>
	///     The unique identifier for the material definition.
	/// </summary>
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Ulid Id { get; init; }

	/// <summary>
	///     The display name of the material.
	/// </summary>
	public required Name Name { get; init; }

	/// <summary>
	///     A descriptive overview of the material's appearance, lore, and traits.
	/// </summary>
	[MaxLength(1024)]
	public string Description { get; init; }

	/// <summary>
	///     Bitwise classification flags (e.g., NaturalOre, Combustible, Fluid).
	/// </summary>
	public MaterialTraits Flags { get; init; }

	/// <summary>
	///     The default phase of matter for this material under standard room temperature conditions.
	/// </summary>
	public StateOfMatter DefaultState { get; init; }

	/// <summary>
	///     Compositional breakdown and metal extraction yields produced when smelting this material.
	///     Empty for pure elements or non-ores.
	/// </summary>
	public ICollection<OreYield> SmeltYields { get; init; } = [];

	/// <summary>
	///     Baseline mechanical characteristics (density, hardness, elasticity).
	/// </summary>
	public MechanicalProperties? MechanicalProperties { get; init; }

	/// <summary>
	///     Baseline thermal properties (melting point, boiling point, specific heat).
	/// </summary>
	public ThermalProperties? ThermalProperties { get; init; }

	/// <summary>
	///     Baseline electrical and magnetic characteristics.
	/// </summary>
	public ElectricalProperties? ElectricalProperties { get; init; }

	/// <summary>
	///     Baseline mystical traits, mana conductivity, and planar elemental attunements.
	/// </summary>
	public FantasticalProperties? FantasticalProperties { get; init; }

	/// <summary>
	///     Phase transition deviations when the material shifts state (Solid, Liquid, Gas).
	/// </summary>
	public ICollection<StateSpecificProperties> StateProperties { get; init; } = [];
}
