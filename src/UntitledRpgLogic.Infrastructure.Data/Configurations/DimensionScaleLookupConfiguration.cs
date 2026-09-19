using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines the <see cref="DimensionScale" /> lookup table
/// </summary>
public sealed class DimensionScaleLookupConfiguration : IEntityTypeConfiguration<DimensionScaleLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<DimensionScaleLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("dimension_scale_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<DimensionScale>()
			.Select(e => new DimensionScaleLookup { Id = e, Name = e.ToString() });
		_ = builder.HasData(seedData);
	}
}
