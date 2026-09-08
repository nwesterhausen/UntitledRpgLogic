
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for `Ability`
///</summary>
public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Ability> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Relationships
		_ = builder
			 .HasMany(ability => ability.ActiveEffects)
			 .WithMany(effect => effect.AbilitiesUsingAsActive)
			 .UsingEntity("AbilityActiveEffects");

		_ = builder
			.HasMany(ability => ability.FailureEffects)
			.WithMany(effect => effect.AbilitiesUsingAsFailure)
			.UsingEntity("AbilityFailureEffects");

		_ = builder.HasOne<AbilityTypeLookup>()
			.WithMany()
			.HasForeignKey(x => x.AbilityType)
			.OnDelete(DeleteBehavior.Restrict);

		_ = builder.HasOne<TargetTypeLookup>()
			.WithMany()
			.HasForeignKey(x => x.TargetType)
			.OnDelete(DeleteBehavior.Restrict);

		_ = builder.OwnsMany(a => a.FailureInfluences, fib =>
		{
			fib.ToTable("ability_failure_influences");
			fib.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(fi => fi.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
			fib.HasOne<Entity>()
				.WithMany()
				.HasForeignKey(fi => fi.RequiredEntityId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		_ = builder.OwnsMany(a => a.CastingRequirements, crb =>
		{
			crb.ToTable("ability_casting_requirements");
			crb.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(cr => cr.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
			crb.HasOne<Entity>()
				.WithMany()
				.HasForeignKey(cr => cr.RequiredEntityId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		_ = builder.OwnsMany(a => a.LearningRequirements, lrb =>
		{
			lrb.ToTable("ability_learning_requirements");
			lrb.HasOne<RequirementTypeLookup>()
				.WithMany()
				.HasForeignKey(lr => lr.RequirementType)
				.OnDelete(DeleteBehavior.Restrict);
			lrb.HasOne<Entity>()
				.WithMany()
				.HasForeignKey(lr => lr.RequiredEntityId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		_ = builder.OwnsMany(a => a.StatCosts, scb =>
		{
			scb.ToTable("ability_stat_costs");
			scb.HasOne<StatDefinition>()
				.WithMany()
				.HasForeignKey(sd => sd.AffectedStatId)
				.OnDelete(DeleteBehavior.Restrict);
		});

	}
}
