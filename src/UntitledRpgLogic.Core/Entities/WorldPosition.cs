using System.ComponentModel.DataAnnotations.Schema;
using UntitledRpgLogic.Core.World;

namespace UntitledRpgLogic.Core.Entities;

/// <summary>
///     Represents the spatial placement of an entity within a discrete 2D map.
/// </summary>
public record WorldPosition
{
	/// <summary>
	///     Create an empty world position record.
	/// </summary>
	public WorldPosition()
	{
	}

	/// <summary>
	///     Create a world position record.
	/// </summary>
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
	public float X { get; init; }

	/// <summary>
	///     Vertical world coordinate.
	/// </summary>
	public float Y { get; init; }

	/// <summary>
	///     Facing angle or heading in degrees (0 to 360).
	/// </summary>
	public float RotationYaw { get; init; }

	/// <summary>
	///     Elevation relative to world datum (-32,768 to +32,767).
	/// </summary>
	public short Elevation { get; init; }

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
