using System.Text.Json.Serialization;
using Tomlyn;
using UntitledRpgLogic.Core.Abilities.Effects;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

/// <summary>
///     Lightweight probe DTO used only to read the discriminator.
/// </summary>
internal sealed class EffectDiscriminatorDto
{
	[JsonPropertyName("effect_type")] public EffectType EffectType { get; init; }
}

public static class EffectConfigDtoMapper
{
	public static Effect Deserialize(string content, TomlSerializerOptions options)
	{
		var probe = TomlSerializer.Deserialize<EffectDiscriminatorDto>(content, options);

		return probe?.EffectType switch
		{
			// Leverages ITomlConfigDto.ToModel() on each concrete DTO
			EffectType.Damage => TomlSerializer.Deserialize<DamageEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Summon => TomlSerializer.Deserialize<SummonEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Enchant => TomlSerializer.Deserialize<EnchantEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Charm => TomlSerializer.Deserialize<CharmEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Heal => TomlSerializer.Deserialize<HealEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Buff => TomlSerializer.Deserialize<BuffEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Debuff => TomlSerializer.Deserialize<DebuffEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Elemental => TomlSerializer.Deserialize<ElementalEffectDefinitionDto>(content, options)!
				.ToModel(),
			EffectType.Movement => TomlSerializer.Deserialize<MovementEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.Transformation =>
				TomlSerializer.Deserialize<TransformationEffectDefinitionDto>(content, options)!.ToModel(),
			EffectType.None
				=> throw new NotSupportedException($"Unknown EffectType '{probe?.EffectType}'.")
		};
	}

	public static string Serialize(Effect model, TomlSerializerOptions options) =>
		model switch
		{
			// Leverages ITomlConfigDto.FromModel(TModel) on each concrete DTO
			DamageEffect damage => TomlSerializer.Serialize(DamageEffectDefinitionDto.FromModel(damage), options),
			SummonEffect summon => TomlSerializer.Serialize(SummonEffectDefinitionDto.FromModel(summon), options),
			_ => throw new NotSupportedException($"Unsupported Effect subtype '{model.GetType().Name}'.")
		};
}
