using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Progression;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extension methods for calculating entity level and evaluating prerequisites.
/// </summary>
public static class EntityProgressionExtensions
{
	/// <summary>
	///     Evaluates whether the specified prerequisite requirement is satisfied by the entity.
	/// </summary>
	public static bool IsSatisfiedBy(this RequirementBase req, Entity caster)
	{
		ArgumentNullException.ThrowIfNull(req);
		ArgumentNullException.ThrowIfNull(caster);

		return req.RequirementType switch
		{
			RequirementType.Stat =>
				caster.GetStatApparentValue(req.RequiredEntityId) >= req.AmountNeeded,

			RequirementType.SkillLevel =>
				caster.GetSkillLevel(req.RequiredEntityId) >= (int)MathF.Round(req.AmountNeeded),

			RequirementType.None => true,

			_ => true
		};
	}

	/// <summary>
	///     Calculates the failure chance percentage for an ability influence threshold.
	/// </summary>
	public static float CalculateFailureChance(this FailureInfluence influence, Entity caster)
	{
		ArgumentNullException.ThrowIfNull(influence);
		ArgumentNullException.ThrowIfNull(caster);

		var currentValue = influence.RequirementType switch
		{
			RequirementType.Stat => caster.GetStatApparentValue(influence.RequiredEntityId),
			RequirementType.SkillLevel => caster.GetSkillLevel(influence.RequiredEntityId),
			_ => influence.AmountAlwaysSucceed
		};

		if (currentValue >= influence.AmountAlwaysSucceed)
		{
			return 0.0f;
		}

		var deficit = influence.AmountAlwaysSucceed - currentValue;
		return Math.Clamp(deficit * influence.InfluenceScale, 0.0f, 1.0f);
	}
}
