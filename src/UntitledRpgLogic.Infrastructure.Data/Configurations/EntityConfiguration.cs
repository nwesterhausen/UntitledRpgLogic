namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

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
		builder.HasOne(e => e.Inventory)
			.WithOne(i => i.Entity)
			.HasForeignKey<EntityInventory>(i => i.EntityId);
	}
}
