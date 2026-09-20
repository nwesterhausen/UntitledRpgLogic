using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Infrastructure.Configuration.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class SkillDefinitionDto : ITomlConfigDto<SkillDefinitionDto, SkillDefinition>
{
	[JsonPropertyName("id")]
	public Ulid Id { get; init; } = Ulid.Empty;

	[JsonPropertyName("name")]
	public Name Name { get; init; } = Name.Empty;

	[JsonPropertyName("description")]
	public string Description { get; init; } = string.Empty;

	[JsonPropertyName("leveling_definition_id")]
	public Ulid? LevelingDefinitionId { get; init; }

	/// <inheritdoc />
	public SkillDefinition ToModel()
	{
		return new SkillDefinition
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description,
			LevelingDefinitionId = this.LevelingDefinitionId
		};
	}

	/// <inheritdoc />
	public static SkillDefinitionDto FromModel(SkillDefinition model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new SkillDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			LevelingDefinitionId = model.LevelingDefinitionId
		};
	}
}
