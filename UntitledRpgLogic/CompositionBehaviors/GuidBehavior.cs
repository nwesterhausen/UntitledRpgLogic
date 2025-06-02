using UntitledRpgLogic.BaseImplementations;

namespace UntitledRpgLogic.CompositionBehaviors;

/// <inheritdoc />
public class GuidBehavior : HasGuidBase
{
    /// <inheritdoc />
    public GuidBehavior(Guid? id = null)
    {
        if (id != null)
            SetId(id.Value);
    }
}