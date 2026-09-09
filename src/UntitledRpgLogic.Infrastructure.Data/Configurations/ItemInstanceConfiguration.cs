
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the <see cref="ItemInstance" /> table
///</summary>
public class ItemInstanceConfiguration : IEntityTypeConfiguration<ItemInstance>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<ItemInstance> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Relationships
		// link to ItemDefinition is set in ItemDefinitionConfiguration

		_ = builder
			.HasOne(i => i.PrimaryMaterial)
			.WithMany()
			.HasForeignKey(i => i.PrimaryMaterialId);
	}
}
