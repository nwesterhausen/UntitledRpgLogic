namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Models;

///<summary>
/// Defines relationship in the `EntityStats` table
///</summary>
public class EntitySkillsConfiguration : IEntityTypeConfiguration<EntitySkills>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<EntitySkills> builder)
	{

		ArgumentNullException.ThrowIfNull(builder);

		// Composite PK
		builder.HasKey(es => new { es.EntityId, es.InstancedSkillId });
	}
}
