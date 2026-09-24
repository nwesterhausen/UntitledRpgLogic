using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Infrastructure.Data.Configurations;

/// <summary>
///     Defines relationship in the <see cref="ItemShape" /> table
/// </summary>
public sealed class ItemShapeConfiguration : IEntityTypeConfiguration<ItemShape>
{
	///<inheritdoc />
	public void Configure(EntityTypeBuilder<ItemShape> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		// Flatten Dimensions into the item_shapes table
		builder.OwnsOne(x => x.Dimensions, dim =>
		{
			// Map the nested properties to explicit column names
			dim.Property(d => d.DimensionScale)
				.HasColumnName("dimension_scale");

			dim.Property(d => d.ShapeType)
				.HasColumnName("dimensions_shape");

			dim.Property(d => d.Depth)
				.HasColumnName("length");

			dim.Property(d => d.Width)
				.HasColumnName("width");

			dim.Property(d => d.Height)
				.HasColumnName("height");
		});

		// Ensures the owned entity navigation is loaded automatically
		builder.Navigation(x => x.Dimensions).IsRequired();
	}
}
