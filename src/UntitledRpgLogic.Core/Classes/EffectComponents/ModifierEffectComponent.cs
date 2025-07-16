using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes.EffectComponents;

/// <summary>
///     Represents a component that applies a stat modification effect.
///     This wraps an existing IModifierEffect to fit into the IEffectComponent pattern.
/// </summary>
public class ModifierEffectComponent : IEffectComponent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ModifierEffectComponent" /> class.
    /// </summary>
    /// <param name="modifierEffect">The underlying modifier effect logic.</param>
    /// <param name="name">The name of this modifier component.</param>
    public ModifierEffectComponent(IModifierEffect modifierEffect, string name)
    {
        ModifierEffect = modifierEffect;
        Name = new Name(name);

        // Generate a new GUID for this component instance
        Guid = Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();
    }

    /// <summary>
    ///     The actual logic for applying the stat modification.
    /// </summary>
    public IModifierEffect ModifierEffect { get; }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public EffectComponentType ComponentType => EffectComponentType.StatModification;
}
