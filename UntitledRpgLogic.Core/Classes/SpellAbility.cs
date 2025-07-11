using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents an active ability that triggers a specific game effect.
/// </summary>
public class SpellAbility : IActiveAbility
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SpellAbility" /> class.
    /// </summary>
    /// <param name="name">The name of the spell ability.</param>
    /// <param name="activeEffect">The active effect that this ability triggers.</param>
    public SpellAbility(string name, IActiveEffect activeEffect)
    {
        Name = new Name(name);
        ActiveEffect = activeEffect;

        // The GUID, Id, ShortGuid for the ability can be derived or separate.
        // For simplicity, we'll generate new ones for the ability itself.
        Guid = Guid.NewGuid();
        Id = Convert.ToBase64String(Guid.ToByteArray());
        ShortGuid = Guid.ToString("N")[..8].ToUpperInvariant();
    }

    /// <summary>
    ///     Gets the active effect associated with this spell ability.
    /// </summary>
    public IActiveEffect ActiveEffect { get; }

    /// <inheritdoc />
    public Guid Guid { get; }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public string ShortGuid { get; }

    /// <inheritdoc />
    public Name Name { get; }

    /// <inheritdoc />
    public void Activate()
    {
        // When the ability is activated, trigger the associated active effect.
        // In a real scenario, 'caster' and 'targets' would come from game state.
        // For example, this could be from the player entity activating the ability
        // and its current target.
        // For this example, we'll use a placeholder context.
        ActiveEffect.Activate(new EffectActivationContext());
    }

    /// <inheritdoc />
    public IReadOnlyCollection<IEffectComponent> Components => ActiveEffect.Components;

    /// <inheritdoc />
    public void Activate(EffectActivationContext context)
    {
        ActiveEffect.Activate(context);
    }

    /// <summary>
    ///     Activates the spell ability with specific context.
    /// </summary>
    /// <param name="caster">The entity casting the spell.</param>
    /// <param name="targets">The targets of the spell.</param>
    public void Activate(IEntity? caster, IEnumerable<IEntity>? targets)
    {
        ActiveEffect.Activate(new EffectActivationContext(caster, targets));
    }
}
