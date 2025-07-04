using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

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
        GuidBehavior = new GuidBehavior(config.ExplicitId);
        NameBehavior = new NameBehavior(config.Name, config.PluralName, config.NameAsAdjective);
        Quality = config.ItemQuality ?? Quality.None;
        ItemType = config.ItemType;
        ItemSubtype = config.ItemSubtype ?? ItemSubtype.None;
        CraftedBy = config.CraftedBy ?? ReservedGuids.GameSystem;
        DimensionScale = config.DimensionScale ?? DimensionScale.cm;
        ShapeType = config.ShapeType ??
                    ShapeType.RectangularPrism; // Actually base this on ItemType and ItemSubtype in the future.
        Width = config.Width;
        Height = config.Height;
        Depth = config.Depth ?? 1.0f;
    }

    /// <summary>
    ///     How we track our GUID, the behavior contains any helper methods or other functionality that is needed for working
    ///     with the GUID.
    /// </summary>
    private GuidBehavior GuidBehavior { get; }

    /// <summary>
    ///     The name behavior contains any helper methods or other functionality that is needed for working with the names
    ///     of an object or entity.
    /// </summary>
    private NameBehavior NameBehavior { get; }

    /// <inheritdoc />
    public Guid Guid => GuidBehavior.Guid;

    /// <inheritdoc />
    public string ShortGuid => GuidBehavior.ShortGuid;

    /// <inheritdoc />
    public string Id => GuidBehavior.Id;

    /// <inheritdoc />
    public string Name => NameBehavior.Name;

    /// <inheritdoc />
    public string PluralName => NameBehavior.PluralName;

    /// <inheritdoc />
    public string NameAsAdjective => NameBehavior.NameAsAdjective;

    /// <inheritdoc />
    public Quality Quality { get; }

    /// <inheritdoc />
    public ItemType ItemType { get; }

    /// <inheritdoc />
    public ItemSubtype ItemSubtype { get; }

    /// <inheritdoc />
    public Guid CraftedBy { get; }

    /// <inheritdoc />
    public DimensionScale DimensionScale { get; }

    /// <inheritdoc />
    public ShapeType ShapeType { get; }

    /// <inheritdoc />
    public float Width { get; set; }

    /// <inheritdoc />
    public float Height { get; set; }

    /// <inheritdoc />
    public float Depth { get; set; }
}
