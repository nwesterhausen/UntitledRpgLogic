using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class AbilityFailureInfluenceDto : RequirementBaseDto
{
	public float AmountAlwaysSucceed { get; init; }
	public float InfluenceScale { get; init; }

	public AbilityFailureInfluence ToModel() => new()
	{
		AmountAlwaysSucceed = this.AmountAlwaysSucceed,
		InfluenceScale = this.InfluenceScale,
		RequirementType = this.RequirementType,
		RequiredEntityId = this.RequirementId,
		AmountNeeded = this.AmountNeeded
	};

	public static AbilityFailureInfluenceDto FromModel(AbilityFailureInfluence model)
	{
		ArgumentNullException.ThrowIfNull(model);
		return new AbilityFailureInfluenceDto
		{
			AmountAlwaysSucceed = model.AmountAlwaysSucceed,
			InfluenceScale = model.InfluenceScale,
			RequirementType = model.RequirementType,
			RequirementId = model.RequiredEntityId,
			AmountNeeded = model.AmountNeeded
		};
	}
}
