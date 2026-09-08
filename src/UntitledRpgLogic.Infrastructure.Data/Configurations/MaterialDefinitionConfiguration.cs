
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;
using UntitledRpgLogic.Infrastructure.Data.ValueConverters;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for `MaterialDefinition`
///</summary>
public class MaterialDefinitionConfiguration : IEntityTypeConfiguration<MaterialDefinition>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<MaterialDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Owned Types
		builder.OwnsOne(m => m.MechanicalProperties, mp => mp.ToJson());
		builder.OwnsOne(m => m.ThermalProperties, tp => tp.ToJson());
		builder.OwnsOne(m => m.ElectricalProperties, ep => ep.ToJson());
		builder.OwnsOne(m => m.FantasticalProperties, fp =>
		{
			fp.ToJson();
			fp.Property(p => p.ElementalAttunement)
	  			.HasConversion<ElementalAttunementConverter>();
		});

		builder.OwnsMany(m => m.StateProperties, spb =>
		{
			spb.ToTable("material_state_properties");
			spb.HasOne<StateOfMatterLookup>()
				.WithMany()
				.HasForeignKey(sp => sp.State)
				.OnDelete(DeleteBehavior.Restrict);

			//	Store optional sub-properties as JSON columns in this table
			spb.OwnsOne(sp => sp.MechanicalProperties);
			spb.OwnsOne(sp => sp.ThermalProperties);
			spb.OwnsOne(sp => sp.ElectricalProperties);
			spb.OwnsOne(sp => sp.FantasticalProperties, fb =>
			{
				fb.Property(p => p.ElementalAttunement)
		  			.HasConversion<ElementalAttunementConverter>();
			});
		});
	}
}
