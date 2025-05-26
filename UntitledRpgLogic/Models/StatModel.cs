using System.ComponentModel.DataAnnotations;
using UntitledRpgLogic.Stat;

namespace UntitledRpgLogic.Models;

public class StatModel
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public StatVariation Variation { get; set; } = StatVariation.Pseudo;
    public int MaxValue { get; set; } = StatBase.STAT_DEFAULT_MAX_VALUE;
    public int MinValue { get; set; } = StatBase.STAT_DEFAULT_MIN_VALUE;
}