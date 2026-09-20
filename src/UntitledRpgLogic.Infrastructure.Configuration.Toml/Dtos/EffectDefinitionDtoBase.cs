using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Common;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

/// <summary>
///     Base DTO for Effects
/// </summary>
public abstract class EffectDefinitionDtoBase
{
	[JsonPropertyName("id")] public Ulid Id { get; init; }

	[JsonPropertyName("name")] public Name Name { get; init; } = default!;

	[JsonPropertyName("description")] public string Description { get; init; } = string.Empty;

	[JsonPropertyName("effect_type")] public EffectType EffectType { get; init; }

	[JsonPropertyName("duration")] public float Duration { get; init; }

	[JsonPropertyName("tick_interval")] public float TickInterval { get; init; }

	public abstract Effect ToModel();
}
