
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core;
using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Models;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines advanced table configuration for <see cref="StatDefinition" />
///</summary>
public class StatDefinitionConfiguration : IEntityTypeConfiguration<StatDefinition>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<StatDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.HasOne<StatVariationLookup>()
			.WithMany()
			.HasForeignKey(x => x.Variation)
			.OnDelete(DeleteBehavior.Restrict);

		// seed well-known universal stats
		builder.HasData(new StatDefinition
		{
			Id = WellKnownIdentifiers.PlayerLevel,
			Name = new Name("Level"),
			Variation = StatVariation.Pseudo,
			MinValue = 0,
			MaxValue = int.MaxValue,
			HasChangeableValue = true
		});
	}
}
