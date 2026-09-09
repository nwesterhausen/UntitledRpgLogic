using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

///<summary>
/// Defines advanced table configuration for <see cref="ItemDefinition"/>
///</summary>
public class ItemDefinitionConfiguration : IEntityTypeConfiguration<ItemDefinition>
{
	/// <inheritdoc  />
	public void Configure(EntityTypeBuilder<ItemDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Configure the owned collection into a dedicated relational child table
		builder.OwnsMany(i => i.Materials, mb =>
		{
			mb.ToTable("item_definition_materials");

			// Enum converted to byte column
			mb.Property(m => m.Slot)
			  .HasConversion<byte>()
			  .IsRequired();

			// Outward foreign key constraint to the materials catalog table
			mb.HasOne(m => m.Material)
			  .WithMany()
			  .HasForeignKey(m => m.MaterialId)
			  .OnDelete(DeleteBehavior.Restrict);
		});
	}
}
