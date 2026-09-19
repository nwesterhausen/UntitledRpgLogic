using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Infrastructure.Configuration.Definitions;

/// <summary>
///     Describes a chance for a failure when an <see cref="AbilityDefinition" /> is used,
/// </summary>
public record FailureInfluenceConfig(RequirementType Type, Ulid EntityId, float SuccessThreshold, float InfluenceScale);
