
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the <see cref="EntityDefinition" /> table
///</summary>
public class EntityDefinitionConfiguration : IEntityTypeConfiguration<EntityDefinition>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<EntityDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

        builder.OwnsOne(ed => ed.RespiratoryProfile, rb =>
        {
            rb.ToJson();
            rb.OwnsMany(r => r.ToxicSubstances);
        });
        builder.OwnsMany(ed => ed.StartingStats, sb => sb.ToJson());
	}
}
