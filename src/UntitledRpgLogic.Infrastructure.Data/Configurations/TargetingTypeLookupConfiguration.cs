
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines the <see cref="TargetingType" /> lookup table
///</summary>
public class TargetingTypeLookupConfiguration : IEntityTypeConfiguration<TargetingTypeLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<TargetingTypeLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("targeting_type_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<TargetingType>()
			.Select(e => new TargetingTypeLookup
			{
				Id = e,
				Name = e.ToString()
			});
		_ = builder.HasData(seedData);
	}
}
