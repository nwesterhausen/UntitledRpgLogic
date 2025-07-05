using UntitledRpgLogic.Core.Classes;

namespace UntitledRpgLogic.Core.Interfaces;

/// <summary>
///     Represents an effect that can be explicitly activated or triggered.
/// </summary>
public interface IActiveEffect : IEffect
{
    /// <summary>
    ///     Activates the effect, triggering its logic.
    ///     The actual application logic is typically handled by a dedicated service.
    /// </summary>
    /// <param name="context">The context in which the effect is activated (e.g., caster, targets).</param>
    void Activate(EffectActivationContext context);
}
