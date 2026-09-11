
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;
///<summary>
/// Defines relationship in the <see cref="SkillDefinition" /> table
///</summary>
public sealed class SkillDefinitionConfiguration : IEntityTypeConfiguration<SkillDefinition>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<SkillDefinition> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

	}
}
