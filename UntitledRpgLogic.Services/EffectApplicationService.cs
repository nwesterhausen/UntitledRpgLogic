using Microsoft.Extensions.Logging;
using UntitledRpgLogic.Core.Classes.EffectComponents;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.Extensions.Common;

namespace UntitledRpgLogic.Services;

/// <summary>
///     A service responsible for applying the logic of game effects based on their constituent components.
/// </summary>
public class EffectApplicationService : IEffectApplicationService
{
    private readonly ILogger<EffectApplicationService> _logger;
    private readonly IStatService _statService;

    /// <summary>
    ///     Creation of the service with dependency injection.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="statService"></param>
    public EffectApplicationService(ILogger<EffectApplicationService> logger, IStatService statService)
    {
        _logger = logger;
        _statService = statService;
    }

    /// <inheritdoc />
    public void ApplyEffect(IEffect effect, IEntity? caster = null, IEnumerable<IEntity>? targets = null)
    {
        _logger.LogInformation("Applying effect: {EffectName} (ID: {EffectShortGuid})", effect.Name.Singular,
            effect.ShortGuid);

        foreach (IEffectComponent component in effect.Components)
            switch (component.ComponentType)
            {
                case EffectComponentType.Dimensions:
                    if (component is DimensionEffectComponent dimensionComponent)
                        // Example: Log or visualize the dimensions of the effect
                        _logger.LogDebug("  Dimensions: {DimensionsString}", dimensionComponent.ToDimensionsString());
                    // If this effect creates a physical object, you would instantiate it here
                    break;

                case EffectComponentType.Physics:
                    if (component is PhysicsEffectComponent physicsComponent)
                        // Example: Apply physics to a projectile
                        _logger.LogDebug(
                            "  Physics: Initial Velocity: {Velocity}, Acceleration: {Acceleration}, Mass: {Mass}",
                            physicsComponent.InitialVelocity, physicsComponent.Acceleration, physicsComponent.Mass);
                    // In a real game, this might add a projectile to a physics engine
                    break;

                case EffectComponentType.Elemental:
                    if (component is ElementalEffectComponent elementalComponent)
                        // Example: Apply elemental damage or status effect
                        _logger.LogDebug("  Elemental: Type: {ElementType}, Intensity: {Intensity}",
                            elementalComponent.ElementType, elementalComponent.Intensity);
                    // This might interact with a damage calculation system or apply status effects
                    break;

                case EffectComponentType.Duration:
                    if (component is DurationEffectComponent durationComponent)
                        // Example: Set duration for a buff/debuff
                        _logger.LogDebug("  Duration: {Duration} seconds (Permanent: {IsPermanent})",
                            durationComponent.DurationInSeconds, durationComponent.IsPermanent);
                    // This duration would then be managed by a separate system that ticks over time
                    break;

                case EffectComponentType.Targeting:
                    if (component is TargetingEffectComponent targetingComponent)
                        // Example: Determine actual targets based on targeting type and parameters
                        _logger.LogDebug("  Targeting: Type: {TargetType}, Range: {Range}, Radius: {Radius}",
                            targetingComponent.TargetType, targetingComponent.Range, targetingComponent.Radius);
                    // This would filter the 'targets' list or determine which entities are affected
                    break;

                case EffectComponentType.StatModification:
                    if (component is ModifierEffectComponent modifierEffectComponent)
                    {
                        // Example: Apply stat modifiers to targets
                        _logger.LogDebug("  Stat Modification: {ModifierDisplay}",
                            modifierEffectComponent.ModifierEffect.ToDisplay());
                        if (targets is IEntity[] enumeration)
                            foreach (IEntity target in enumeration)
                                // This is a simplified example. In a real scenario, you'd apply it to
                                // a StatEntry of the target entity.
                                _logger.LogDebug("    Applying stat modification to target: {TargetName}",
                                    target.Name.Singular);
                        // Example (conceptual):
                        // if (target is IHasStats hasStats && hasStats.TryGetStat("Health", out IStat healthStat))
                        // {
                        //     StatEntry<IStat> healthEntry = new StatEntry<IStat>(healthStat, isDamageable: true, isHealable: true);
                        //     _statService.AddModifier(healthEntry, new Modifier(modifierEffectComponent.ModifierEffect, /* other modifier properties */));
                        // }
                    }

                    break;

                case EffectComponentType.Summoning: // New case for SummoningEffectComponent
                    if (component is SummonEffectComponent summonComponent)
                        _logger.LogDebug("  Summoning: Creature: {Creature}, Count: {Count}, Duration: {Duration}s",
                            summonComponent.CreatureTypeToSummon, summonComponent.NumberOfCreatures,
                            summonComponent.DurationInSeconds);
                    // In a real game, this would trigger your summoning system to instantiate creatures.
                    break;
                case EffectComponentType.None:
                    break;
                default:
                    _logger.LogWarning("Unsupported effect component type encountered: {ComponentType}",
                        component.ComponentType);
                    break;
            }
    }
}
