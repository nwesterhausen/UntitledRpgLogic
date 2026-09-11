using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Environment;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Describes an ambient affected by an <see cref="Effect" />.
/// </summary>
public record AffectedAmbientConfig(AmbientType AmbientType, float Amount, bool IsPercentage = false);
