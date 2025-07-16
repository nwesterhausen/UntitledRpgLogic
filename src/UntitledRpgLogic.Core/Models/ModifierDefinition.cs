using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Defines a modifier that can be applied to stats, such as buffs or debuffs.
/// </summary>
public class ModifierDefinition
{
    /// <summary>
    ///     The unique identifier for the modifier. This is used to reference the modifier in the game.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    ///     The name of the modifier. This is used to identify the modifier in the game and is used in the UI.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Whether the modifier is permanent or not. Permanent modifiers do not expire and remain active until removed.
    /// </summary>
    public bool IsPermanent { get; set; }

    /// <summary>
    ///     Whether the modifier is positive or negative. Positive modifiers increase stats, while negative modifiers decrease
    ///     them.
    /// </summary>
    public bool IsPositive { get; set; }

    /// <summary>
    ///     Whether the modifier is additive or multiplicative. Additive modifiers add to the stat value, while multiplicative
    ///     modifiers multiply the stat value.
    /// </summary>
    public bool IsAdditive { get; set; }

    /// <summary>
    ///     Whether the modifier is a percentage or a flat value. Percentage modifiers apply a percentage change to the stat,
    ///     while flat value modifiers apply a fixed amount.
    /// </summary>
    public bool IsMultiplicative { get; set; }

    /// <summary>
    ///     Whether the modifier scales with the base value of the stat or the current value. If true, the modifier scales with
    ///     the base value; if false, it scales with the current value.
    /// </summary>
    public bool ScalesOnBaseValue { get; set; }

    /// <summary>
    ///     The maximum number of stacks this modifier can have. Stacking allows the same modifier to be applied multiple
    ///     times, increasing its effect.
    /// </summary>
    public int MaxStacks { get; set; }

    /// <summary>
    ///     The duration in seconds for which the modifier is active. If the modifier is permanent, this value is ignored.
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    ///     Whether the modifier loses all stacks when it expires. If true, all stacks are removed when the duration ends; if
    ///     false, remaining stacks persist.
    /// </summary>
    public bool LoseAllStacksOnExpiration { get; set; }

    /// <summary>
    ///     The priority of the modifier. This determines the order in which modifiers are applied when multiple modifiers
    ///     affect the same stat.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///     The GUID for the effect that this modifier applies to the stat when it is active, for 0 or no stacks.
    /// </summary>
    public Guid? ModifierEffectId { get; set; }

    /// <summary>
    ///     The GUID for the effect that each stack of this modifier has on the stat. This is used when the modifier can stack,
    ///     allowing it to apply additional effects per stack.
    /// </summary>
    public Guid? StackEffectId { get; set; }

    /// <summary>
    ///     The effects this modifier applies to the stat when it is active, for 0 or no stacks.
    /// </summary>
    [ForeignKey(nameof(ModifierEffectId))]
    public ModificationEffect? ModifierEffect { get; set; }

    /// <summary>
    ///     The effect(s) that each stack of this modifier has on the stat.
    /// </summary>
    [ForeignKey(nameof(StackEffectId))]
    public ModificationEffect? StackEffect { get; set; }
}
