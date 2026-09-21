using System.Text.Json.Serialization;
using Tomlyn.Serialization;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Stats;
using UntitledRpgLogic.Infrastructure.Configuration.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class StatDefinitionDto : ITomlConfigDto<StatDefinitionDto, StatDefinition>
{
	[JsonPropertyName("id")] public Ulid Id { get; init; }

	[JsonPropertyName("name")] public Name Name { get; init; } = default!;

	[JsonPropertyName("description")] public string Description { get; init; } = string.Empty;

	[JsonPropertyName("variation")] public StatVariation Variation { get; init; } = StatVariation.Major;

	[JsonPropertyName("has_changeable_value")]
	public bool HasChangeableValue { get; init; } = true;

	[JsonPropertyName("linked_stats")]
	[TomlSingleOrArray]
	public List<LinkedStatDto> LinkedStats { get; init; } = [];

	/// <inheritdoc />
	public StatDefinition ToModel() =>
		new()
		{
			Id = this.Id == default ? Ulid.Empty : this.Id,
			Name = this.Name,
			Description = this.Description,
			Variation = this.Variation,
			HasChangeableValue = this.HasChangeableValue,
			LinkedStats = this.LinkedStats.ConvertAll(s => s.ToModel(this.Id))
		};

	/// <inheritdoc />
	public static StatDefinitionDto FromModel(StatDefinition model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new StatDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			Variation = model.Variation,
			HasChangeableValue = model.HasChangeableValue,
			LinkedStats = model.LinkedStats.Select(LinkedStatDto.FromModel).ToList()
		};
	}
}
