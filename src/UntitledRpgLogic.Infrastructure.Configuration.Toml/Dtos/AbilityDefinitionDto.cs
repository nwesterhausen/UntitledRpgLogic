using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class AbilityDefinitionDto : IConfigDto<AbilityDefinitionDto, AbilityDefinition>
{
	public Name Name { get; init; }
	public AbilityType AbilityType { get; init; }
	public TargetingType TargetingType { get; init; }
	public bool AffectsCaster { get; init; }
	public bool AffectsAllies { get; init; }
	public int NumberOfTargets { get; init; }
	public float CastTime { get; init; }
	public Ulid SkillDisciplineId { get; init; } = Ulid.Empty;
	public List<StatCostDto> StatCosts { get; init; } = [];
	public Ulid Id { get; init; } = Ulid.Empty;
	public string Description { get; init; } = string.Empty;
	public List<AbilityLearningRequirementDto> LearningRequirements { get; init; } = [];
	public List<AbilityCastingRequirementDto> CastingRequirements { get; init; } = [];
	public List<AbilityFailureInfluenceDto> FailureInfluences { get; init; } = [];

	public AbilityDefinition ToModel() => new()
	{
		AbilityType = this.AbilityType,
		TargetingType = this.TargetingType,
		AffectsCaster = this.AffectsCaster,
		AffectsAllies = this.AffectsAllies,
		NumberOfTargets = this.NumberOfTargets,
		CastTime = this.CastTime,
		SkillDisciplineId = this.SkillDisciplineId,
		StatCosts = this.StatCosts.ConvertAll(x => x.ToModel()),
		LearningRequirements = this.LearningRequirements.ConvertAll(x => x.ToModel()),
		CastingRequirements = this.CastingRequirements.ConvertAll(x => x.ToModel()),
		FailureInfluences = this.FailureInfluences.ConvertAll(x => x.ToModel()),
		Name = this.Name,
		Description = this.Description,
		Id = this.Id
	};

	public static AbilityDefinitionDto FromModel(AbilityDefinition model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new AbilityDefinitionDto
		{
			Name = model.Name,
			AbilityType = model.AbilityType,
			TargetingType = model.TargetingType,
			AffectsCaster = model.AffectsCaster,
			AffectsAllies = model.AffectsAllies,
			NumberOfTargets = model.NumberOfTargets,
			CastTime = model.CastTime,
			SkillDisciplineId = model.SkillDisciplineId,
			StatCosts = model.StatCosts.Select(StatCostDto.FromModel).ToList(),
			Id = model.Id,
			Description = model.Description,
			LearningRequirements =
				model.LearningRequirements.Select(AbilityLearningRequirementDto.FromModel).ToList(),
			CastingRequirements = model.CastingRequirements.Select(AbilityCastingRequirementDto.FromModel).ToList(),
			FailureInfluences = model.FailureInfluences.Select(AbilityFailureInfluenceDto.FromModel).ToList()
		};
	}
}
