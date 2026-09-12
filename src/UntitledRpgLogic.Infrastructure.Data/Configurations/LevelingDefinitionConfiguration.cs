using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Progression;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Entity Framework Core configuration for <see cref="LevelingDefinition" />.
/// </summary>
public sealed class LevelingDefinitionConfiguration : IEntityTypeConfiguration<LevelingDefinition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<LevelingDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// FK in lookup table
		builder.HasOne<ScalingCurveTypeLookup>()
			.WithMany()
			.HasForeignKey(x => x.ScalingCurve)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
