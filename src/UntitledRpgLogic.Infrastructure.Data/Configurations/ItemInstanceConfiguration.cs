
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the `Entity` table
///</summary>
public class ItemInstanceConfiguration : IEntityTypeConfiguration<ItemInstance>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<ItemInstance> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Relationships
		_ = builder
			 .HasOne(i => i.ItemDefinition)
			 .WithMany()
			 .HasForeignKey(i => i.ItemDefinitionId);

		_ = builder
			.HasOne(i => i.PrimaryMaterial)
			.WithMany()
			.HasForeignKey(i => i.PrimaryMaterialId);
	}
}
