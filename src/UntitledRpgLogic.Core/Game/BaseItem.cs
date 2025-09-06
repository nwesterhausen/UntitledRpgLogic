using UntitledRpgLogic.Core.Classes;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces.Common;
using UntitledRpgLogic.Core.Interfaces.Inventory;

namespace UntitledRpgLogic.Core.Game;

/// <summary>
///     Data defining an item in the game. This would provide the data-side of an item (where the 'visual' side would
///     happen in the UI/game engine).
/// </summary>
public class BaseItem : IItem
{
	/// <summary>
	///     Creates an instance of <see cref="BaseItem" /> using the provided configuration (which is loaded from TOML).
	/// </summary>
	/// <param name="config"></param>
	public BaseItem(ItemDataConfig config)
	{
		ArgumentNullException.ThrowIfNull(config, nameof(config));

		this.Id = config.Id;
		this.Name = Name.Deserialize(config.Name);
		this.Quality = config.ItemQuality ?? Quality.None;
		this.ItemType = config.ItemType;
		this.ItemSubtype = config.ItemSubtype ?? ItemSubtype.None;
		this.CraftedBy = config.CraftedBy;
		this.DimensionScale = config.DimensionScale ?? DimensionScale.Cm;
		this.ShapeType = config.ShapeType ??
		                 ShapeType.RectangularPrism; // Actually base this on ItemType and ItemSubtype in the future.
		this.Width = config.Width;
		this.Height = config.Height;
		this.Depth = config.Depth ?? 1.0f;
	}

	/// <inheritdoc />
	public Quality Quality { get; }

	/// <inheritdoc />
	public ItemType ItemType { get; }

	/// <inheritdoc />
	public ItemSubtype ItemSubtype { get; }

	/// <inheritdoc />
	public Ulid CraftedBy { get; }

	/// <inheritdoc />
	public DimensionScale DimensionScale { get; set; }

	/// <inheritdoc />
	public ShapeType ShapeType { get; }

	/// <inheritdoc />
	public float Width { get; set; }

	/// <inheritdoc />
	public float Height { get; set; }

	/// <inheritdoc />
	public float Depth { get; set; }

	/// <inheritdoc />
	public Ulid Id { get; }

	/// <inheritdoc />
	public Name Name { get; }

	/// <inheritdoc />
	public IMaterial PrimaryMaterial { get; } = Material.Empty;
}
