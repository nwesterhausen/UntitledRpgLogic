
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Advanced table configuration for `LinkedStats`
///</summary>
public class LinkedStatsConfiguration : IEntityTypeConfiguration<LinkedStats>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<LinkedStats> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Composite PK
		_ = builder.HasKey(ls => new { ls.DependentStatId, ls.LinkedStatId });

		// Relationships
		_ = builder
			 .HasOne(ls => ls.DependentStat)
			 .WithMany() // StatDefinition does not have a collection of LinkedStats, so this is empty.
			 .HasForeignKey(ls => ls.DependentStatId)
			 .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a StatDefinition if it's in use.

		_ = builder
			.HasOne(ls => ls.LinkedStat)
			.WithMany()
			.HasForeignKey(ls => ls.LinkedStatId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
