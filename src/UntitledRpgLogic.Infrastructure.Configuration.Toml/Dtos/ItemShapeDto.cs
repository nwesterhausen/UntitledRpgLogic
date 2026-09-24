using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Items;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public class ItemShapeDto : IConfigDto<ItemShapeDto, ItemShape>
{
	public Ulid Id { get; init; } = Ulid.Empty;
	public Name Name { get; init; } = Name.Empty;
	public string Description { get; init; } = string.Empty;
	public ShapeType ShapeType { get; init; } = ShapeType.Cube;
	public DimensionScale Scale { get; init; } = DimensionScale.Cm;
	public float Width { get; init; } = 1f;
	public float Height { get; init; } = 1f;
	public float Depth { get; init; } = 1f;

	public ItemShape ToModel() => new()
	{
		Id = this.Id,
		Name = this.Name,
		Description = this.Description,
		Dimensions = new Dimensions
		{
			Width = this.Width,
			Height = this.Height,
			Depth = this.Depth,
			DimensionScale = this.Scale,
			ShapeType = this.ShapeType
		}
	};

	public static ItemShapeDto FromModel(ItemShape model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new ItemShapeDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			ShapeType = model.Dimensions.ShapeType,
			Scale = model.Dimensions.DimensionScale,
			Width = model.Dimensions.Width,
			Height = model.Dimensions.Height,
			Depth = model.Dimensions.Depth
		};
	}
}
