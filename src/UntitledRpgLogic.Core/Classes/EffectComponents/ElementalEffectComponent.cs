using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

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
	public ElementalEffectComponent(string name, Guid magicType, float intensity)
	{
		this.Name = new Name(name);
		this.MagicType = magicType;
		this.Intensity = intensity;

		// Generate a new GUID for this component instance
		this.Identifier = Guid.NewGuid();
		this.Id = Convert.ToBase64String(this.Identifier.ToByteArray());
		this.ShortId = this.Identifier.ToString("N")[..8].ToUpperInvariant();
	}

	/// <summary>
	/// The Guid of the MagicTypeDataConfig that defines the element of this effect.
	/// </summary>
	public Guid MagicType { get; set; }

	/// <summary>
	///     The intensity or power of the elemental effect. The unit depends on the ElementType (e.g., Celsius for Fire).
	/// </summary>
	public float Intensity { get; }

	/// <inheritdoc />
	public Guid Identifier { get; }

	/// <inheritdoc />
	public string Id { get; }

	/// <inheritdoc />
	public string ShortId { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public EffectComponentType ComponentType => EffectComponentType.Elemental;
}
