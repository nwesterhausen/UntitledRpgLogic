namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

///<summary>
/// Defines the `RequirementType` lookup table
///</summary>
public class RequirementTypeLookupConfiguration : IEntityTypeConfiguration<RequirementTypeLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<RequirementTypeLookup> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		builder.ToTable("requirement_type_lookup");
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.HasConversion<int>();
		builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<RequirementType>()
			.Select(e => new RequirementTypeLookup
			{
				Id = e,
				Name = e.ToString()
			});
		builder.HasData(seedData);
	}
}
