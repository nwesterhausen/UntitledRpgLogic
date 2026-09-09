
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
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
		_ = builder.HasKey(es => new { es.EntityId, es.InstancedStatId });
	}
}
