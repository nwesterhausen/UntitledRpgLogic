namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

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
		builder.HasKey(ls => new { ls.DependentStatId, ls.LinkedStatId });

		// Relationships
		builder
			 .HasOne(ls => ls.DependentStat)
			 .WithMany() // StatDefinition does not have a collection of LinkedStats, so this is empty.
			 .HasForeignKey(ls => ls.DependentStatId)
			 .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a StatDefinition if it's in use.

		builder
			.HasOne(ls => ls.LinkedStat)
			.WithMany()
			.HasForeignKey(ls => ls.LinkedStatId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
