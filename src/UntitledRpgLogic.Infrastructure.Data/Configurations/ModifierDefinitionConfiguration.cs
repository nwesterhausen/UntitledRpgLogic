using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Abilities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Entity Framework Core configuration for <see cref="ModifierDefinition" />.
/// </summary>
public sealed class ModifierDefinitionConfiguration : IEntityTypeConfiguration<ModifierDefinition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<ModifierDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasMany(modifier => modifier.ModificationEffects)
			.WithMany()
			.UsingEntity(j => j.ToTable("modification_base_effects"));
		builder.HasMany(modifier => modifier.StackEffects)
			.WithMany()
			.UsingEntity(j => j.ToTable("modification_stack_effects"));
	}
}
