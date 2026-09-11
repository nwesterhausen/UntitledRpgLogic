
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines the <see cref="FractureType" /> lookup table
///</summary>
public sealed class FractureTypeLookupConfiguration : IEntityTypeConfiguration<FractureTypeLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<FractureTypeLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("fracture_type_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<FractureType>()
			.Select(e => new FractureTypeLookup
			{
				Id = e,
				Name = e.ToString()
			});
		_ = builder.HasData(seedData);
	}
}
