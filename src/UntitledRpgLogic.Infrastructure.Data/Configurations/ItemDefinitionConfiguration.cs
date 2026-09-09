using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

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

		// Map lookup tables for enums
		builder.HasOne<ItemTypeLookup>()
			.WithMany()
			.HasForeignKey(x => x.ItemType)
			.OnDelete(DeleteBehavior.Restrict);
		builder.HasOne<ItemSubtypeLookup>()
			.WithMany()
			.HasForeignKey(x => x.ItemSubtype)
			.OnDelete(DeleteBehavior.Restrict);

		// Configure the owned collection into a dedicated relational child table
		builder.OwnsMany(i => i.Materials, mb =>
		{
			mb.ToTable("item_definition_materials");

			// Outward foreign key constraint to the material slot lookup (as byte)
			mb.Property(m => m.Slot)
			  .HasConversion<byte>()
			  .IsRequired();
			mb.HasOne<MaterialSlotLookup>()
				.WithMany()
				.HasForeignKey(x => x.Slot)
				.OnDelete(DeleteBehavior.Restrict);

			// Outward foreign key constraint to the materials catalog table
			mb.HasOne(m => m.Material)
			  .WithMany()
			  .HasForeignKey(m => m.MaterialId)
			  .OnDelete(DeleteBehavior.Restrict);
		});
	}
}
