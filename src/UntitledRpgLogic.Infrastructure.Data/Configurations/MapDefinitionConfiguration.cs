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

		builder.OwnsOne(m => m.Atmosphere, ab =>
		{
			ab.ToJson();
			ab.OwnsMany(a => a.GasFractions);
		});
		builder.OwnsMany(m => m.BaselineAmbients, bb => bb.ToJson());

		builder.HasMany(m => m.Chunks)
			.WithOne(c => c.Map)
			.HasForeignKey(c => c.MapId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(m => m.Transitions)
			.WithOne(t => t.SourceMap)
			.HasForeignKey(t => t.SourceMapId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.OwnsOne(m => m.GenerationConfig, cb =>
		{
			cb.ToJson("generation_config");
			cb.OwnsOne(c => c.Terrain, tb =>
			{
				tb.OwnsMany(t => t.Minerals);
				tb.OwnsMany(t => t.Stone);
				tb.OwnsOne(t => t.NoiseGeneration);
			});
			cb.OwnsOne(c => c.Hydrology);
			cb.OwnsOne(c => c.Climate);
			cb.OwnsOne(c => c.Arcana, ab =>
			{
				ab.OwnsMany(a => a.AvailableElements);
			});
		});

		builder.OwnsMany(m => m.OreDeposits, ob =>
		{
			ob.ToJson();
		});
	}
}
