using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Models;

public class InstancedStatModel
{
    [Key] public int Id { get; set; }

    public int StatId { get; set; }

    [ForeignKey(nameof(StatId))] public virtual StatModel? Stat { get; set; }

    public int Value { get; set; }
}