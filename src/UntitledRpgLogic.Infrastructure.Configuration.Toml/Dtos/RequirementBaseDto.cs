using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public abstract class RequirementBaseDto
{
	[JsonPropertyName("type")] public RequirementType RequirementType { get; init; } = RequirementType.None;

	[JsonPropertyName("requirement_id")] public Ulid RequirementId { get; init; } = Ulid.Empty;

	[JsonPropertyName("amount")] public float AmountNeeded { get; init; }
}
