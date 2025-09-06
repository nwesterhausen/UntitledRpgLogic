using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Effects;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that defines elemental properties of an effect.
/// </summary>
public class ElementalEffectComponent : IEffectComponent
{
	/// <summary>
	///     Initializes a new instance of the <see cref="ElementalEffectComponent" /> class.
	/// </summary>
	/// <param name="name">The name of this elemental component (e.g., "Fire Blast").</param>
	/// <param name="magicType">The specific elemental type of energy.</param>
	/// <param name="intensity">The intensity or power of the elemental effect (e.g., heat in Celsius).</param>
	public ElementalEffectComponent(string name, Ulid magicType, float intensity)
	{
		this.Name = new Name(name);
		this.MagicType = magicType;
		this.Intensity = intensity;

		// Generate a new GUID for this component instance
		this.Id = Ulid.NewUlid();
	}

	/// <summary>
	///     The Ulid of the MagicTypeDataConfig that defines the element of this effect.
	/// </summary>
	public Ulid MagicType { get; set; }

	/// <summary>
	///     The intensity or power of the elemental effect. The unit depends on the ElementType (e.g., Celsius for Fire).
	/// </summary>
	public float Intensity { get; }

	/// <inheritdoc />
	public Ulid Id { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Elemental;
}
