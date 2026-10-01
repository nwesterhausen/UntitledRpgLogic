using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines relationship in the <see cref="ModuleInfo" /> table
/// </summary>
public sealed class ModuleInfoConfiguration : IEntityTypeConfiguration<ModuleInfo>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<ModuleInfo> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Put the summary into a json value
		builder.OwnsMany(x => x.ContentSummary, csb =>
		{
			csb.ToJson();
		});

		// Flatten manifest details into the module info table
		builder.OwnsOne(x => x.Manifest, manifest =>
		{
			// Map the nested properties to explicit column names
			manifest.Property(m => m.Id)
				.HasColumnName("package_id");
			manifest.Property(m => m.Name)
				.HasColumnName("name");
			manifest.Property(m => m.Description)
				.HasColumnName("description");
			manifest.Property(m => m.AuthorId)
				.HasColumnName("author_id");
			manifest.Property(m => m.AuthorName)
				.HasColumnName("author_name");
			manifest.Property(m => m.Version)
				.HasColumnName("version");

			manifest.Property(m => m.Dependencies)
				.HasColumnName("dependencies");
		});

		// Ensures the owned entity navigation is loaded automatically
		builder.Navigation(x => x.Manifest).IsRequired();
	}
}
