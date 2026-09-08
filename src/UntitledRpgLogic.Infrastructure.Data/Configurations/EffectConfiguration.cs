
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for `Effect`
///</summary>
public class EffectConfiguration : IEntityTypeConfiguration<Effect>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Effect> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);


		// Configure TPH (Table-Per-Hierarchy) for the Effect model
		// ---
		// This allows a single "effect" table to hold definitions for many various types
		// of effects, using a column "EffectType" to differentiate (discriminate) between
		// the types.
		//
		// When querying, specifying the effect type automatically uses a "WHERE" behind the scenes.
		// `dbContext.Effects.OfType<DamageEffect>().ToListAsync()` would only grab DamageEffects
		// stored in the table. Without specifying `OfType`, a mixed list of all various effect types
		// would be returned.
		_ = builder
			.HasDiscriminator(e => e.EffectType)
			.HasValue<SummonEffect>(EffectType.Summon)
			.HasValue<EnchantEffect>(EffectType.Enchant)
			.HasValue<CharmEffect>(EffectType.Charm)
			.HasValue<HealEffect>(EffectType.Heal)
			.HasValue<DamageEffect>(EffectType.Damage)
			.HasValue<BuffEffect>(EffectType.Buff)
			.HasValue<DebuffEffect>(EffectType.Debuff)
			.HasValue<ElementalEffect>(EffectType.Elemental);
	}
}
