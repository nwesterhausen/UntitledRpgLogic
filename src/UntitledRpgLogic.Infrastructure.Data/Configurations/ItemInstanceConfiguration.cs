namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

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
		builder
			 .HasOne(i => i.ItemDefinition)
			 .WithMany()
			 .HasForeignKey(i => i.ItemDefinitionId);

		builder
			.HasOne(i => i.PrimaryMaterial)
			.WithMany()
			.HasForeignKey(i => i.PrimaryMaterialId);
	}
}
