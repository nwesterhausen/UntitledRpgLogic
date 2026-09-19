using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Entities;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines advanced table configuration for <see cref="Entity" />
/// </summary>
public sealed class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Entity> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Relationships
		_ = builder.HasOne(e => e.Inventory)
			.WithOne(i => i.Entity)
			.HasForeignKey<Inventory>(i => i.EntityId);

		_ = builder.OwnsMany(e => e.AffectedStats, sp =>
		{
			sp.ToJson();
		});

		// Map WorldPosition as flat columns: map_id, position_x, position_y, rotation_yaw
		builder.OwnsOne(e => e.Position, pb =>
		{
			pb.Property(p => p.MapId)
				.HasColumnName("map_id");

			pb.Property(p => p.X).HasColumnName("position_x");
			pb.Property(p => p.Y).HasColumnName("position_y");
			pb.Property(p => p.RotationYaw).HasColumnName("rotation_yaw");

			// Spatial lookup index: find all entities on a specific map
			pb.HasIndex(p => p.MapId);
		});

		builder.HasOne(e => e.CurrentMap)
			.WithMany()
			.HasForeignKey("Position_MapId")
			.OnDelete(DeleteBehavior.SetNull);
	}
}
