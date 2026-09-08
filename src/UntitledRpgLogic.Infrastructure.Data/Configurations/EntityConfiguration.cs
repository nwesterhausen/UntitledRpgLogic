
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for `Entity`
///</summary>
public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Entity> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Relationships
		_ = builder.HasOne(e => e.Inventory)
			.WithOne(i => i.Entity)
			.HasForeignKey<EntityInventory>(i => i.EntityId);

		_ = builder.OwnsMany(e => e.AffectedStats, sp =>
		{
			sp.ToJson();
		});
	}
}
