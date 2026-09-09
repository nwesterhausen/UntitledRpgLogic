using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.ValueConverters;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

///<summary>
/// Defines advanced table configuration for <see cref="MapDefinition"/>
///</summary>
public sealed class MapDefinitionConfiguration : IEntityTypeConfiguration<MapDefinition>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<MapDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.Property(m => m.Id)
			.HasConversion<UlidToBytesConverter>()
			.IsRequired();

		_ = builder.Property(m => m.Type)
			.HasConversion<byte>()
			.IsRequired();

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
