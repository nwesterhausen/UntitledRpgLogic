using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Extensions.Common;

/// <summary>
///     Extension methods for managing entity stat attachments and values.
/// </summary>
public static class EntityStatExtensions
{
	/// <summary>
	///     Attaches or updates a stat on an entity, initializing the underlying <see cref="Stat" /> and
	///     <see cref="EntityStats" /> join record.
	/// </summary>
	/// <param name="entity">The target entity.</param>
	/// <param name="definition">The stat template definition.</param>
	/// <param name="initialValue">The starting base and apparent value.</param>
	/// <returns>The resulting <see cref="EntityStats" /> join record.</returns>
	public static EntityStats SetStat(this Entity entity, StatDefinition definition, int initialValue)
	{
		ArgumentNullException.ThrowIfNull(entity);
		ArgumentNullException.ThrowIfNull(definition);

		var existing = entity.Stats.FirstOrDefault(s => s.InstancedStat?.DefinitionId == definition.Id);
		if (existing is not null)
		{
			if (existing.InstancedStat is not null)
			{
				existing.InstancedStat.BaseValue = initialValue;
				existing.InstancedStat.ApparentValue = initialValue;
			}

			return existing;
		}

		var instancedStat = new Stat(definition.Id) { BaseValue = initialValue, ApparentValue = initialValue };

		var join = new EntityStats(entity.Id, instancedStat.Id) { Entity = entity, InstancedStat = instancedStat };

		entity.Stats.Add(join);
		return join;
	}

	/// <summary>
	///     Get the level of a particular skill on this entity.
	/// </summary>
	/// <param name="entity">target entity</param>
	/// <param name="statDefinitionId">The ID of the skill to find</param>
	/// <returns>The skill level or `0` if the entity doesn't know the skill</returns>
	/// <exception cref="ArgumentNullException">Throws if <paramref name="entity" /> is `null`</exception>
	public static int GetStatApparentValue(this Entity entity, Ulid statDefinitionId)
	{
		ArgumentNullException.ThrowIfNull(entity);

		var stat = entity.Stats.FirstOrDefault(s => s.InstancedStat?.DefinitionId == statDefinitionId);

		return stat?.InstancedStat?.ApparentValue ?? 0;
	}
}
