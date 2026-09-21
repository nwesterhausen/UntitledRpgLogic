using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class AbilityLearningRequirementDto : RequirementBaseDto
{
	public AbilityLearningRequirement ToModel() => new()
	{
		RequirementType = this.RequirementType,
		RequiredEntityId = this.RequirementId,
		AmountNeeded = this.AmountNeeded
	};

	public static AbilityLearningRequirementDto FromModel(AbilityLearningRequirement model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new AbilityLearningRequirementDto
		{
			RequirementType = model.RequirementType,
			RequirementId = model.RequiredEntityId,
			AmountNeeded = model.AmountNeeded
		};
	}
}
