using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for <see cref="Inventory" />
///</summary>
public sealed class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Inventory> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Map InventoryFilter as an owned JSON column
		builder.OwnsOne(inv => inv.Filter, fb =>
		{
			fb.ToJson();
		});
	}
}
