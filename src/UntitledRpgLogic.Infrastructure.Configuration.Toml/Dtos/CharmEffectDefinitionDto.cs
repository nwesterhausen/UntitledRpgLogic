using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class CharmEffectDefinitionDto : EffectDefinitionDtoBase,
	IConfigDto<CharmEffectDefinitionDto, CharmEffect>
{
	public override CharmEffect ToModel() =>
		new()
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description,
			EffectType = EffectType.Damage,
			Duration = this.Duration,
			TickInterval = this.TickInterval
		};

	public static CharmEffectDefinitionDto FromModel(CharmEffect model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new CharmEffectDefinitionDto
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
