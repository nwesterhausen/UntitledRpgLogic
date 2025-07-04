using UntitledRpgLogic.Core.Enums;
using UntitledRpgLogic.Core.Interfaces;

namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents the data for an item in the game. This is an immutable data container.
/// </summary>
public record Item : IItem
{
    /// <inheritdoc />
    public Guid Guid { get; init; }

    /// <inheritdoc />
    public string Id { get; init; } = string.Empty;

    /// <inheritdoc />
    public string ShortGuid { get; init; } = string.Empty;

    /// <inheritdoc />
    public Name Name { get; init; } = Name.Empty;

    /// <inheritdoc />
    public Quality Quality { get; init; }

    /// <inheritdoc />
    public ItemType ItemType { get; init; }

    /// <inheritdoc />
    public ItemSubtype ItemSubtype { get; init; }

    /// <inheritdoc />
    public Guid CraftedBy { get; init; }

    /// <inheritdoc />
    public DimensionScale DimensionScale { get; set; }

    /// <inheritdoc />
    public ShapeType ShapeType { get; init; }

    /// <inheritdoc />
    public float Width { get; set; }

    /// <inheritdoc />
    public float Height { get; set; }

    /// <inheritdoc />
    public float Depth { get; set; }
}
