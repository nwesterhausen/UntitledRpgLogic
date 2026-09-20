using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

/// <summary>
/// </summary>
public sealed class LinkedStatDto
{
	[JsonPropertyName("stat_id")] public Ulid? StatId { get; init; }

	[JsonPropertyName("dependent_stat_id")]
	public Ulid DependentStatId { get; init; }

	[JsonPropertyName("ratio")] public float Ratio { get; init; } = 1.0f;

	public LinkedStats ToModel(Ulid parentStatId) => new()
	{
		StatId = parentStatId, DependsOnId = this.DependentStatId, Ratio = this.Ratio
	};

	public LinkedStats ToModel() => new()
	{
		StatId = this.StatId ?? Ulid.Empty, DependsOnId = this.DependentStatId, Ratio = this.Ratio
	};

	public static LinkedStatDto FromModel(LinkedStats model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new LinkedStatDto { DependentStatId = model.DependsOnId, Ratio = model.Ratio };
	}
}
