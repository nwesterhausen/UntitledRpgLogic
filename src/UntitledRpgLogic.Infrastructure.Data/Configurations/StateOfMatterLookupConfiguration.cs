using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines the <see cref="StateOfMatter" /> lookup table
/// </summary>
public sealed class StateOfMatterLookupConfiguration : IEntityTypeConfiguration<StateOfMatterLookup>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<StateOfMatterLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Define table
		_ = builder.ToTable("state_of_matter_lookup");
		_ = builder.HasKey(x => x.Id);
		_ = builder.Property(x => x.Id)
			.HasConversion<int>();
		_ = builder.Property(x => x.Name)
			.HasMaxLength(127)
			.IsRequired();

		// Seed with enum data
		var seedData = Enum.GetValues<StateOfMatter>()
			.Select(e => new StateOfMatterLookup { Id = e, Name = e.ToString() });
		_ = builder.HasData(seedData);
	}
}
