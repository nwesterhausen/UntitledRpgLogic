using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
/// Describes a chance for a failure when an <see cref="Ability"/> is used,
/// </summary>
public record FailureInfluenceConfig(RequirementType Type, Ulid EntityId, float SuccessThreshold, float InfluenceScale);
