using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Abilities;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Entity Framework Core configuration for <see cref="AbilityDefinition" />.
/// </summary>
public sealed class AbilityConfiguration : IEntityTypeConfiguration<AbilityDefinition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<AbilityDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// 1. Enum Foreign Key Constraints to Lookup Tables
		builder.HasOne<AbilityTypeLookup>()
			.WithMany()
			.HasForeignKey(a => a.AbilityType)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne<TargetingTypeLookup>()
			.WithMany()
			.HasForeignKey(a => a.TargetingType)
			.OnDelete(DeleteBehavior.Restrict);

		// 2. Owned Collections (Dependent Relational Tables)
		builder.OwnsMany(a => a.StatCosts, scb =>
		{
			scb.ToTable("ability_stat_costs");

			// Enforces that StatId references the stat_definitions catalog table
			scb.HasOne(sc => sc.Stat)
				.WithMany()
				.HasForeignKey(sc => sc.StatId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.OwnsMany(a => a.LearningRequirements, lrb =>
		{
			lrb.ToTable("ability_learning_requirements");

			lrb.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(lr => lr.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.OwnsMany(a => a.CastingRequirements, crb =>
		{
			crb.ToTable("ability_casting_requirements");

			crb.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(cr => cr.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
		});

		builder.OwnsMany(a => a.FailureInfluences, fib =>
		{
			fib.ToTable("ability_failure_influences");

			fib.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(fi => fi.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
		});

		// 3. Many-to-Many Relationships to Polymorphic Effect Table
		builder.HasMany(a => a.ActiveEffects)
			.WithMany(e => e.TriggeringAbilities)
			.UsingEntity(
				"ability_active_effects",
				r => r.HasOne(typeof(Effect)).WithMany().HasForeignKey("EffectId").OnDelete(DeleteBehavior.Cascade),
				l => l.HasOne(typeof(AbilityDefinition)).WithMany().HasForeignKey("AbilityId")
					.OnDelete(DeleteBehavior.Cascade));

		builder.HasMany(a => a.FailureEffects)
			.WithMany()
			.UsingEntity(
				"ability_failure_effects",
				r => r.HasOne(typeof(Effect)).WithMany().HasForeignKey("EffectId").OnDelete(DeleteBehavior.Cascade),
				l => l.HasOne(typeof(AbilityDefinition)).WithMany().HasForeignKey("AbilityId")
					.OnDelete(DeleteBehavior.Cascade));
	}
}
