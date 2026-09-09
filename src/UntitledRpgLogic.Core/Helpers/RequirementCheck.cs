using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Core.Helpers;

/// <summary>
///     Helper class for evaluating whether prerequisite requirements and conditions are satisfied.
/// </summary>
public static class RequirementCheck
{
    /// <summary>
    ///     Checks if the <paramref name="req" /> condition is met by the <paramref name="caster" />.
    /// </summary>
    /// <param name="req">The prerequisite requirement.</param>
    /// <param name="caster">The entity attempting to satisfy the condition.</param>
    /// <returns><c>true</c> if the requirement is satisfied; otherwise, <c>false</c>.</returns>
    public static bool IsMet(RequirementBase req, Entity caster)
    {
        ArgumentNullException.ThrowIfNull(req);
        ArgumentNullException.ThrowIfNull(caster);

        return req.RequirementType switch
        {
            RequirementType.Stat =>
                GetStatValue(caster, req.RequiredEntityId) >= req.AmountNeeded,

            RequirementType.SkillLevel =>
                GetSkillLevel(caster, req.RequiredEntityId) >= req.AmountNeeded,

            RequirementType.PlayerLevel =>
                GetPlayerLevel(caster) >= req.AmountNeeded,

            RequirementType.None => true,

            _ => true
        };
    }

    /// <summary>
    ///     Calculates the failure probability (0.0 to 1.0) caused by an unmet <see cref="FailureInfluence" />.
    /// </summary>
    public static float CalculateFailureChance(FailureInfluence influence, Entity caster)
    {
        ArgumentNullException.ThrowIfNull(influence);
        ArgumentNullException.ThrowIfNull(caster);

        var currentValue = influence.RequirementType switch
        {
            RequirementType.Stat => GetStatValue(caster, influence.RequiredEntityId),
            RequirementType.SkillLevel => GetSkillLevel(caster, influence.RequiredEntityId),
            RequirementType.PlayerLevel => GetPlayerLevel(caster),
            _ => influence.AmountAlwaysSucceed
        };

        // If the caster meets or exceeds the guaranteed success threshold, no failure penalty
        if (currentValue >= influence.AmountAlwaysSucceed)
        {
            return 0.0f;
        }

        // Penalty scales proportionally to the deficit
        var deficit = influence.AmountAlwaysSucceed - currentValue;
        var failureChance = deficit * influence.InfluenceScale;

        return Math.Clamp(failureChance, 0.0f, 1.0f);
    }

    private static float GetStatValue(Entity caster, Ulid statDefinitionId)
    {
        if (caster.Stats is null or { Count: 0 }) return 0f;

        return caster.Stats
            .FirstOrDefault(s => s.InstancedStat?.StatDefinitionId == statDefinitionId)
            ?.InstancedStat?.ApparentValue ?? 0f;
    }

    private static float GetSkillLevel(Entity caster, Ulid skillDefinitionId)
    {
        if (caster.Skills is null or { Count: 0 }) return 0f;

        return caster.Skills
            .FirstOrDefault(s => s.InstancedSkill?.SkillDefinitionId == skillDefinitionId)
            ?.InstancedSkill?.Level ?? 0f;
    }

    private static float GetPlayerLevel(Entity caster)
    {
        if (caster.Stats is null or { Count: 0 }) return 0f;

        return caster.Stats
            .FirstOrDefault(s => s.InstancedStat?.StatDefinition?.Name.Singular == "Level")
            ?.InstancedStat?.ApparentValue ?? 0f;
    }
}
