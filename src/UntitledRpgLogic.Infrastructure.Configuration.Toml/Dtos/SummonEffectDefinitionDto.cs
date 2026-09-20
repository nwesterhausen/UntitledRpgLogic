using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Infrastructure.Configuration.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class SummonEffectDefinitionDto : EffectDefinitionDtoBase,
	ITomlConfigDto<SummonEffectDefinitionDto, SummonEffect>
{
	[JsonPropertyName("summon_entity_template_id")]
	public Ulid SummonEntityTemplateId { get; init; }

	[JsonPropertyName("quantity")] public int Quantity { get; init; } = 1;

	public override SummonEffect ToModel() =>
		new()
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description,
			EffectType = EffectType.Summon,
			Duration = this.Duration,
			TickInterval = this.TickInterval,
			SummonEntityTemplateId = this.SummonEntityTemplateId,
			Quantity = this.Quantity
		};

	public static SummonEffectDefinitionDto FromModel(SummonEffect model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new SummonEffectDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			EffectType = model.EffectType,
			Duration = model.Duration,
			TickInterval = model.TickInterval,
			SummonEntityTemplateId = model.SummonEntityTemplateId,
			Quantity = model.Quantity
		};
	}
}
