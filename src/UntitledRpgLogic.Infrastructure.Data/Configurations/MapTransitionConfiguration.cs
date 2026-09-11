using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
/// 	Advanced table configuration for <see cref="MapTransition"/>
/// </summary>
public sealed class MapTransitionConfiguration : IEntityTypeConfiguration<MapTransition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<MapTransition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Spatial lookup index for portals/doors
		_ = builder.HasIndex(t => new { t.SourceMapId, t.SourceX, t.SourceY });

		// Relationships
		_ = builder.HasOne(t => t.SourceMap)
			.WithMany(m => m.Transitions)
			.HasForeignKey(t => t.SourceMapId)
			.OnDelete(DeleteBehavior.Cascade);

		_ = builder.HasOne(t => t.TargetMap)
			.WithMany()
			.HasForeignKey(t => t.TargetMapId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
