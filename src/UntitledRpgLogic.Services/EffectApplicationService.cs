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
	private readonly ILogger<EffectApplicationService> logger;
	private readonly IStatService statService;

	/// <summary>
	///     Creation of the service with dependency injection.
	/// </summary>
	/// <param name="logger"></param>
	/// <param name="statService"></param>
	public EffectApplicationService(ILogger<EffectApplicationService> logger, IStatService statService)
	{
		this.logger = logger;
		this.statService = statService;
	}

	/// <inheritdoc />
	public void ApplyEffect(IEffect effect, IEntity? caster = null, IEnumerable<IEntity>? targets = null)
	{
		ArgumentNullException.ThrowIfNull(effect, nameof(effect));

		this.logger.LogInformation("Applying effect: {EffectName} (ID: {EffectShortGuid})", effect.Name.Singular,
			effect.ShortGuid);

		foreach (var component in effect.EffectComponents)
		{
			switch (component.ComponentType)
			{
				case EffectComponentType.Dimensions:
					if (component is DimensionEffectComponent dimensionComponent)
					{
						// Example: Log or visualize the dimensions of the effect
						this.logger.LogDebug("  Dimensions: {DimensionsString}", dimensionComponent.ToDimensionsString());
					}

					// If this effect creates a physical object, you would instantiate it here
					break;

				case EffectComponentType.Physics:
					if (component is PhysicsEffectComponent physicsComponent)
					{
						// Example: Apply physics to a projectile
						this.logger.LogDebug(
							"  Physics: Initial Velocity: {Velocity}, Acceleration: {Acceleration}, Mass: {Mass}",
							physicsComponent.InitialVelocity, physicsComponent.Acceleration, physicsComponent.Mass);
					}

					// In a real game, this might add a projectile to a physics engine
					break;

				case EffectComponentType.Elemental:
					if (component is ElementalEffectComponent elementalComponent)
					{
						// Example: Apply elemental damage or status effect
						this.logger.LogDebug("  Elemental: Type: {ElementType}, Intensity: {Intensity}",
							elementalComponent.ElementType, elementalComponent.Intensity);
					}

					// This might interact with a damage calculation system or apply status effects
					break;

				case EffectComponentType.Duration:
					if (component is DurationEffectComponent durationComponent)
					{
						// Example: Set duration for a buff/debuff
						this.logger.LogDebug("  Duration: {Duration} seconds (Permanent: {IsPermanent})",
							durationComponent.DurationInSeconds, durationComponent.IsPermanent);
					}

					// This duration would then be managed by a separate system that ticks over time
					break;

				case EffectComponentType.Targeting:
					if (component is TargetingEffectComponent targetingComponent)
					{
						// Example: Determine actual targets based on targeting type and parameters
						this.logger.LogDebug("  Targeting: Type: {TargetType}, Range: {Range}, Radius: {Radius}",
							targetingComponent.TargetType, targetingComponent.Range, targetingComponent.Radius);
					}

					// This would filter the 'targets' list or determine which entities are affected
					break;

				case EffectComponentType.StatModification:
					if (component is ModifierEffectComponent modifierEffectComponent)
					{
						// Example: Apply stat modifiers to targets
						this.logger.LogDebug("  Stat Modification: {ModifierDisplay}",
							modifierEffectComponent.ModifierEffect.ToDisplay());
						if (targets is IEntity[] enumeration)
						{
							foreach (var target in enumeration)
							{
								// This is a simplified example. In a real scenario, you'd apply it to
								// a StatEntry of the target entity.
								this.logger.LogDebug("    Applying stat modification to target: {TargetName}",
									target.Name.Singular);
							}
						}
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
					{
						this.logger.LogDebug("  Summoning: Creature: {Creature}, Count: {Count}, Duration: {Duration}s",
							summonComponent.CreatureTypeToSummon, summonComponent.NumberOfCreatures,
							summonComponent.DurationInSeconds);
					}

					// In a real game, this would trigger your summoning system to instantiate creatures.
					break;
				case EffectComponentType.None:
					break;
				default:
					this.logger.LogWarning("Unsupported effect component type encountered: {ComponentType}",
						component.ComponentType);
					break;
			}
		}
	}
}
