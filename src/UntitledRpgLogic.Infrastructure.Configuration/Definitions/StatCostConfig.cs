using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
/// Describes a cost in terms of a stat points to use an <see cref="Ability"/>
/// (e.g., 2 points of "Mana" to cast a spell).
/// </summary>
public record StatCostConfig(Ulid StatId, float Amount);
