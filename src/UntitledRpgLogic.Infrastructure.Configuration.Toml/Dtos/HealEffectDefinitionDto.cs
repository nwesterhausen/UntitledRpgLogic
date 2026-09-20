using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class HealEffectDefinitionDto : EffectDefinitionDtoBase,
	IConfigDto<HealEffectDefinitionDto, HealEffect>
{
	public Ulid AffectedStatId { get; init; } = Ulid.Empty;
	public ChangeOptions Options { get; init; }
	public bool CanOverheal { get; init; }

	public override HealEffect ToModel() =>
		new(this.Name, this.AffectedStatId, this.Options, this.CanOverheal)
		{
			Id = this.Id,
			Description = this.Description,
			EffectType = EffectType.Heal,
			Duration = this.Duration,
			TickInterval = this.TickInterval
		};

	public static HealEffectDefinitionDto FromModel(HealEffect model)
	{
		ArgumentNullException.ThrowIfNull(model);

		if (model.AffectedStat == null)
		{
			throw new ArgumentNullException(nameof(model), "Heal effects require a stat to modify");
		}

		return new HealEffectDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			EffectType = model.EffectType,
			Duration = model.Duration,
			TickInterval = model.TickInterval,
			CanOverheal = model.CanOverheal,
			Options = model.AffectedStat,
			AffectedStatId = model.AffectedStat.StatId
		};
	}
}
