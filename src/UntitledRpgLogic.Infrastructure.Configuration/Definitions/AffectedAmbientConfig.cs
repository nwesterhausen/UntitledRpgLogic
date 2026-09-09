using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Describes an ambient affected by an <see cref="Effect" />.
/// </summary>
public record AffectedAmbientConfig(AmbientType AmbientType, float Amount, bool IsPercentage = false);
