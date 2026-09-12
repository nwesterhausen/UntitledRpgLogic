using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Infrastructure.Data.LookupEntities;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Lookup table for <see cref="MaterialSlot" />
/// </summary>
public sealed class MaterialSlotLookupConfiguration : IEntityTypeConfiguration<MaterialSlotLookup>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<MaterialSlotLookup> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.ToTable("material_slot_lookup");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.HasConversion<byte>();

		builder.Property(x => x.Name)
			.HasMaxLength(64)
			.IsRequired();

		// Seed values
		var seed = Enum.GetValues<MaterialSlot>()
			.Select(e => new MaterialSlotLookup { Id = e, Name = e.ToString() });

		builder.HasData(seed);
	}
}
