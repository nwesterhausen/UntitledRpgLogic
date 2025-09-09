using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
/// Describes a requirement that must be met to use an <see cref="Ability"/>.
/// </summary>
public record RequirementConfig(RequirementType Type, Ulid EntityId, float Amount);
