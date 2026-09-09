
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines the <see cref="Quality" /> lookup table
///</summary>
public class QualityLookupConfiguration : IEntityTypeConfiguration<QualityLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<QualityLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("quality_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<Quality>()
			.Select(e => new QualityLookup
			{
				Id = e,
				Name = e.ToString()
			});
		_ = builder.HasData(seedData);
	}
}
