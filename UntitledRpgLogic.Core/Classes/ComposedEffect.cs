using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents a concrete game effect or spell, composed of multiple modular effect components,
///     that can be explicitly activated.
/// </summary>
public class ComposedEffect : IActiveEffect
{
    private readonly List<IEffectComponent> _components;
    private readonly IEffectApplicationService _effectApplicationService; // Dependency for activating the effect

    /// <summary>
    ///     Initializes a new instance of the <see cref="ComposedEffect" /> class.
    /// </summary>
    /// <param name="name">The name of the effect (e.g., "Fireball", "Featherfall").</param>
    /// <param name="components">The collection of micro-effect components that define this effect's behavior.</param>
    /// <param name="effectApplicationService">The service responsible for applying the effect's logic.</param>
    public ComposedEffect(
        string name,
        IEnumerable<IEffectComponent> components,
        IEffectApplicationService effectApplicationService)
    {
        Name = new Name(name);
        _components = new List<IEffectComponent>(components);
        _effectApplicationService = effectApplicationService;

        // Generate a new GUID for this effect instance
        Guid = Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();
    }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public IReadOnlyCollection<IEffectComponent> Components => _components.AsReadOnly();

    /// <inheritdoc />
    public void Activate(EffectActivationContext context)
    {
        // Delegate the actual application logic to the EffectApplicationService
        _effectApplicationService.ApplyEffect(this, context.Caster, context.Targets);
    }
}
