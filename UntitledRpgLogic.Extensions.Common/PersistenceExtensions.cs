using UntitledRpgLogic.Interfaces;
using UntitledRpgLogic.Models;

namespace UntitledRpgLogic.Extensions;

/// <summary>
///     Extensions for persistence-related operations like storing and retrieving data.
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    ///     Converts an <see cref="ISkill" /> to a <see cref="SkillDefinition" /> for database storage.
    ///     This method is used to convert a skill object into a database model that can be persisted.
    /// </summary>
    /// <param name="skill"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static SkillDefinition ToSkillDefinition<T>(this T skill) where T : ISkill
    {
        return new SkillDefinition
        {
            Name = skill.Name,
            Id = skill.Guid,
            MaxLevel = skill.MaxLevel,
            ScalingCurve = skill.ScalingCurve,
            ScalingFactorA = skill.ScalingFactorA,
            ScalingFactorB = skill.ScalingFactorB,
            ScalingFactorC = skill.ScalingFactorC,
            PointsForFirstLevel = skill.PointsForFirstLevel
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="stat"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static StatDefinition ToStatDefinition<T>(this T stat) where T : IStat
    {
        return new StatDefinition
        {
            Name = stat.Name,
            Id = stat.Guid,
            MaxValue = stat.MaxValue,
            Variation = stat.Variation,
            MinValue = stat.MinValue,
            HasChangeableValue = true,
            LinkedStats = stat.LinkedStats.Select(s => new LinkedStats
            {
                DependentStatId = stat.Guid,
                LinkedStatId = s.Key,
                Ratio = s.Value
            }).ToList()
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="skill"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static InstancedSkill ToInstancedSkill<T>(this T skill) where T : ISkill
    {
        if (skill.InstanceId == Guid.Empty) throw new ArgumentException("InstanceId must be set for instanced skills.");

        return new InstancedSkill
        {
            SkillDefinitionId = skill.Guid,
            Level = skill.Level,
            Id = skill.InstanceId
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="stat"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static InstancedStat ToInstancedStat<T>(this T stat) where T : IStat
    {
        if (stat.InstanceId == Guid.Empty) throw new ArgumentException("InstanceId must be set for instanced stats.");

        return new InstancedStat
        {
            StatDefinitionId = stat.Guid,
            ApparentValue = stat.Value,
            BaseValue = stat.BaseValue,
            Id = stat.InstanceId
        };
    }
}
