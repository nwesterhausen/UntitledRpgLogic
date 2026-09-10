
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the <see cref="EntitySkills" /> table
///</summary>
public sealed class EntitySkillsConfiguration : IEntityTypeConfiguration<EntitySkills>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<EntitySkills> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Composite PK
		_ = builder.HasKey(es => new { es.EntityId, es.InstancedSkillId });
	}
}
