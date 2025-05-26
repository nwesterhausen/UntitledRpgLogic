using System.Data;
using UntitledRpgLogic.Models;
using UntitledRpgLogic.Stat;

namespace UntitledRpgLogic.ModelMappers;

public static class StatMappers
{
    public static StatBase ToStat(StatModel model, int? value = null)
    {
        return model.Variation switch
        {
            StatVariation.Major => new MajorStat(model.Name, model.MaxValue, value ?? model.MinValue, model.MinValue),
            StatVariation.Minor => new MinorStat(model.Name, model.MaxValue, value ?? model.MinValue, model.MinValue),
            StatVariation.Pseudo => new PseudoStat(model.Name, model.MaxValue, value ?? model.MinValue, model.MinValue),
            StatVariation.Complex => new ComplexStat(model.Name, model.MaxValue, value ?? model.MinValue,
                model.MinValue),
            _ => throw new ArgumentOutOfRangeException(nameof(model.Variation),
                $"Unknown stat variation: {model.Variation}")
        };
    }

    public static StatModel ToStatModel(StatBase stat)
    {
        if (stat == null) throw new DataException("Unable to map stat to model: Stat is null.");

        if (stat.Value != stat.MinValue)
            throw new DataException("Must call ToInstancedStatModel to map a stat with a value.");

        return new StatModel
        {
            Name = stat.Name,
            Variation = stat.Variation,
            MaxValue = stat.MaxValue,
            MinValue = stat.MinValue
        };
    }

    public static InstancedStatModel ToInstancedStatModel(StatBase stat, Guid entityId)
    {
        if (entityId == Guid.Empty) throw new ArgumentNullException(nameof(entityId));

        // We must find or create a StatModel for the stat
        var statModel = ToStatModel(stat);

        return new InstancedStatModel
        {
            EntityId = entityId,
            StatId = statModel.Id,
            Value = stat.Value
        };
    }

    public static StatBase ToInstancedStat(InstancedStatModel model)
    {
        if (model.Stat == null)
            throw new DataException("Unable to map instanced stat model to stat: StatModel is null.");

        var value = model.Value;
        var stat = ToStat(model.Stat, value);
        return stat;
    }
}