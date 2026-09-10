
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines the <see cref="MassScale" /> lookup table
///</summary>
public sealed class MassScaleLookupConfiguration : IEntityTypeConfiguration<MassScaleLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<MassScaleLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("mass_scale_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<MassScale>()
			.Select(e => new MassScaleLookup
			{
				Id = e,
				Name = e.ToString()
			});
		_ = builder.HasData(seedData);
	}
}
