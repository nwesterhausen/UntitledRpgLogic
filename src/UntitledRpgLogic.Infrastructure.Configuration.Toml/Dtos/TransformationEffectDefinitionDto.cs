using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class TransformationEffectDefinitionDto : EffectDefinitionDtoBase,
	IConfigDto<TransformationEffectDefinitionDto, TransformationEffect>
{
	public override TransformationEffect ToModel() =>
		new()
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description,
			EffectType = EffectType.Damage,
			Duration = this.Duration,
			TickInterval = this.TickInterval
		};

	public static TransformationEffectDefinitionDto FromModel(TransformationEffect model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new TransformationEffectDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			EffectType = model.EffectType,
			Duration = model.Duration,
			TickInterval = model.TickInterval
		};
	}
}
