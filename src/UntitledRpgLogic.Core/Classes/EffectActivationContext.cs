using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Provides context for an effect's activation, including the caster and potential targets.
/// </summary>
public class EffectActivationContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EffectActivationContext" /> class.
    /// </summary>
    /// <param name="caster">The entity that initiated or cast the effect.</param>
    /// <param name="targets">An optional collection of entities targeted by the effect.</param>
    public EffectActivationContext(IEntity? caster = null, IEnumerable<IEntity>? targets = null)
    {
        Caster = caster;
        if (targets is not null) Targets = targets.ToList();
    }

    /// <summary>
    ///     Gets the entity that cast or initiated the effect. Can be null for environmental effects.
    /// </summary>
    public IEntity? Caster { get; }

    /// <summary>
    ///     Gets the collection of entities that are targets of the effect.
    /// </summary>
    public IReadOnlyCollection<IEntity> Targets { get; } = [];
}
