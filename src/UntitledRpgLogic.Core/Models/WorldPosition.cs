using System.ComponentModel.DataAnnotations.Schema;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Represents the spatial placement of an entity within a discrete 2D map.
/// </summary>
public record WorldPosition
{
	/// <inheritdoc />
	public WorldPosition()
	{
	}

	/// <inheritdoc />
	/// <param name="mapId">ULID of the map located on</param>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	/// <param name="rotationYaw">rotation</param>
	public WorldPosition(Ulid mapId, float x, float y, float rotationYaw = 0f)
	{
		this.MapId = mapId;
		this.X = x;
		this.Y = y;
		this.RotationYaw = rotationYaw;
	}

	/// <summary>
	///     Foreign key referencing the active <see cref="MapDefinition" />.
	/// </summary>
	public Ulid MapId { get; init; }

	/// <summary>
	///     Horizontal world coordinate.
	/// </summary>
	public float X { get; set; }

	/// <summary>
	///     Vertical world coordinate.
	/// </summary>
	public float Y { get; set; }

	/// <summary>
	///     Facing angle or heading in degrees (0 to 360).
	/// </summary>
	public float RotationYaw { get; set; }

	/// <summary>
	///     Calculates the horizontal chunk index containing this position.
	/// </summary>
	[NotMapped]
	public int ChunkX => (int)MathF.Floor(this.X / 16f);

	/// <summary>
	///     Calculates the vertical chunk index containing this position.
	/// </summary>
	[NotMapped]
	public int ChunkY => (int)MathF.Floor(this.Y / 16f);
}
