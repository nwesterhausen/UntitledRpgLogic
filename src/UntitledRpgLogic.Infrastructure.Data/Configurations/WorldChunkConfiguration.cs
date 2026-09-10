using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

///<summary>
/// Defines advanced table configuration for <see cref="WorldChunk"/>
///</summary>
public sealed class WorldChunkConfiguration : IEntityTypeConfiguration<WorldChunk>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<WorldChunk> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Spatial coordinate lookup constraint
		_ = builder.HasIndex(c => new { c.MapId, c.ChunkX, c.ChunkY })
			.IsUnique();

		// Owned JSON collections
		_ = builder.OwnsOne(c => c.AtmosphereOverride, ab =>
		{
			ab.ToJson();
			ab.OwnsMany(a => a.GasFractions);
		});
		_ = builder.OwnsMany(c => c.AmbientOverrides, ob => ob.ToJson());

		_ = builder.Property(c => c.CompressedTileBlob)
			.IsRequired();
		_ = builder.Property(c => c.MaterialPalette)
			.IsRequired();

		_ = builder.HasOne(c => c.Map)
			.WithMany(m => m.Chunks)
			.HasForeignKey(c => c.MapId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
