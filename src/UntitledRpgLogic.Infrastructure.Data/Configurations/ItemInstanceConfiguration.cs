
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the <see cref="Item" /> table
///</summary>
public sealed class ItemInstanceConfiguration : IEntityTypeConfiguration<Item>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<Item> builder)
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
