using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Services;

/// <summary>
///     Pure domain service managing stack counts, duration renewal, and stat adjustments for active modifiers.
/// </summary>
public sealed class ModifierApplicationService : IModifierApplicationService
{
	/// <inheritdoc />
	public bool TryApplyModifier(Entity entity, ModifierDefinition definition, DateTimeOffset currentTime)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(definition);

		var existing = entity.AppliedModifiers
			.FirstOrDefault(m => m.ModifierDefinitionId == definition.Id);

		DateTimeOffset? expiration = definition.IsPermanent
			? null
			: currentTime.AddSeconds(definition.Duration);

		if (existing is not null)
		{
			// If already at or above max stacks, refresh duration and return false
			if (existing.Stacks >= definition.MaxStacks)
			{
				existing.ExpiresAt = expiration;
				return false;
			}

			existing.Stacks++;
			existing.ExpiresAt = expiration;
			return true;
		}

		var newModifier = new AppliedModifier(definition.Id, entity.Id)
		{
			Stacks = 1, AppliedAt = currentTime, ExpiresAt = expiration, ModifierDefinition = definition
		};

		entity.AppliedModifiers.Add(newModifier);
		return true;
	}

	/// <inheritdoc />
	public void TickModifiers(Entity entity, DateTimeOffset currentTime)
	{
		ArgumentNullException.ThrowIfNull(entity);

		var expired = entity.AppliedModifiers
			.Where(m => m.ExpiresAt.HasValue && m.ExpiresAt.Value <= currentTime)
			.ToList();

		foreach (var modifier in expired)
		{
			var def = modifier.ModifierDefinition;
			if (def is null || def.LoseAllStacksOnExpiration || modifier.Stacks <= 1)
			{
				entity.AppliedModifiers.Remove(modifier);
			}
			else
			{
				modifier.Stacks--;
				modifier.ExpiresAt = currentTime.AddSeconds(def.Duration);
			}
		}
	}
}
