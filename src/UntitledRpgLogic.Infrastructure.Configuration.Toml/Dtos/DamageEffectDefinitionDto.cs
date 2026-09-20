using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class DamageEffectDefinitionDto : EffectDefinitionDtoBase,
	IConfigDto<DamageEffectDefinitionDto, DamageEffect>
{
	[JsonPropertyName("damage_type")] public DamageType DamageType { get; init; } = DamageType.None;

	[JsonPropertyName("ignores_armor")] public bool IgnoresArmor { get; init; }

	[JsonPropertyName("delay")] public TimeSpan? Delay { get; init; }

	[JsonPropertyName("affected_stat_id")] public Ulid AffectedStatId { get; init; } = Ulid.Empty;

	[JsonPropertyName("stat_change_options")]
	public ChangeOptions Options { get; init; }

	public override DamageEffect ToModel() =>
		new(this.Name, this.AffectedStatId, this.Options, this.IgnoresArmor)
		{
			Id = this.Id,
			Description = this.Description,
			EffectType = EffectType.Damage,
			Duration = this.Duration,
			TickInterval = this.TickInterval,
			DamageType = this.DamageType,
			Delay = this.Delay
		};

	public static DamageEffectDefinitionDto FromModel(DamageEffect model)
	{
		ArgumentNullException.ThrowIfNull(model);

		if (model.AffectedStat == null)
		{
			throw new ArgumentNullException(nameof(model), "Damage effects must have an affected stat");
		}

		return new DamageEffectDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			EffectType = model.EffectType,
			Duration = model.Duration,
			TickInterval = model.TickInterval,
			DamageType = model.DamageType,
			IgnoresArmor = model.IgnoresArmor,
			Delay = model.Delay,
			AffectedStatId = model.AffectedStat.StatId,
			Options = model.AffectedStat
		};
	}
}
