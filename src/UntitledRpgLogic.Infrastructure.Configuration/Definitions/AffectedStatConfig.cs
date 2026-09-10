using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Describes a stat affected by an <see cref="Effect" />.
/// </summary>
public record AffectedStatConfig(
	Ulid StatId,
	float Amount,
	bool IsPercentage
);
