
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

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
	}
}
