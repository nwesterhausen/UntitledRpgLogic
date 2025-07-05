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
    /// <param name="elementType">The specific elemental type of energy.</param>
    /// <param name="intensity">The intensity or power of the elemental effect (e.g., heat in Celsius).</param>
    public ElementalEffectComponent(string name, ElementType elementType, float intensity)
    {
        Name = new Name(name);
        ElementType = elementType;
        Intensity = intensity;

        // Generate a new GUID for this component instance
        Guid = Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();
    }

    /// <summary>
    ///     The type of elemental energy this component represents.
    /// </summary>
    public ElementType ElementType { get; }

    /// <summary>
    ///     The intensity or power of the elemental effect. The unit depends on the ElementType (e.g., Celsius for Fire).
    /// </summary>
    public float Intensity { get; }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public EffectComponentType ComponentType => EffectComponentType.Elemental;
}
