
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines the <see cref="AmbientType" /> lookup table
///</summary>
public class AmbientTypeLookupConfiguration : IEntityTypeConfiguration<AmbientTypeLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<AmbientTypeLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("ambient_type_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<AmbientType>()
			.Select(e => new AmbientTypeLookup
			{
				Id = e,
				Name = e.ToString()
			});
		_ = builder.HasData(seedData);
	}
}
