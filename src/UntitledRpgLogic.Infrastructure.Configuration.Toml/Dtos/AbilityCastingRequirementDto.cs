using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class AbilityCastingRequirementDto : RequirementBaseDto
{
	public AbilityCastingRequirement ToModel() => new()
	{
		RequirementType = this.RequirementType,
		RequiredEntityId = this.RequirementId,
		AmountNeeded = this.AmountNeeded
	};

	public static AbilityCastingRequirementDto FromModel(AbilityCastingRequirement model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new AbilityCastingRequirementDto
		{
			RequirementType = model.RequirementType,
			RequirementId = model.RequiredEntityId,
			AmountNeeded = model.AmountNeeded
		};
	}
}
