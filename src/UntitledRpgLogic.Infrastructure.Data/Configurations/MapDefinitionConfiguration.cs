using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines advanced table configuration for <see cref="MapDefinition" />
/// </summary>
public sealed class MapDefinitionConfiguration : IEntityTypeConfiguration<MapDefinition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<MapDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.HasOne<MapTypeLookup>()
			.WithMany()
			.HasForeignKey(m => m.Type)
			.OnDelete(DeleteBehavior.Restrict);

		_ = builder.OwnsOne(m => m.Atmosphere, ab =>
		{
			ab.ToJson();
			ab.OwnsMany(a => a.GasFractions);
		});
		_ = builder.OwnsMany(m => m.BaselineAmbients, bb => bb.ToJson());

		_ = builder.HasMany(m => m.Chunks)
			.WithOne(c => c.Map)
			.HasForeignKey(c => c.MapId)
			.OnDelete(DeleteBehavior.Cascade);

		_ = builder.HasMany(m => m.Transitions)
			.WithOne(t => t.SourceMap)
			.HasForeignKey(t => t.SourceMapId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
