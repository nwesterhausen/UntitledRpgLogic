namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

///<summary>
/// Defines advanced table configuration for `EntityStats`
///</summary>
public class EntityStatsConfiguration : IEntityTypeConfiguration<EntityStats>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<EntityStats> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Composite PK
		builder.HasKey(es => new { es.EntityId, es.InstancedStatId });
	}
}
