using System.Runtime.InteropServices;
using UntitledRpgLogic.Core.Enums;

namespace UntitledRpgLogic.Core.Models;

/// <summary>
///     Low-level unmanaged 8-byte struct representing a single 2D terrain cell in a chunk.
///     Uses chunk-local palette indices to resolve 128-bit material ULIDs without memory bloat.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Tile2D
{
	/// <summary>
	///     Bedrock or terrain floor elevation relative to world datum (-32,768 to +32,767).
	/// </summary>
	public short Elevation;

	/// <summary>
	///     Active surface liquid column depth (0 = dry, up to 65,535 units).
	///     Operates independently of Elevation to support dry basins and deep inland trenches.
	/// </summary>
	public ushort LiquidDepth;

	/// <summary>
	///     0-based index pointing to the chunk's <see cref="WorldChunk.MaterialPalette" /> for the ground surface material.
	/// </summary>
	public byte GroundPaletteIndex;

	/// <summary>
	///     0-based index pointing to the chunk's <see cref="WorldChunk.MaterialPalette" /> for the liquid substance (e.g., Water, Acid, Lava).
	/// </summary>
	public byte LiquidPaletteIndex;

	/// <summary>
	///     Local thermal deviation from the chunk's baseline temperature (-128 to +127 °C).
	/// </summary>
	public sbyte TemperatureOffset;

	/// <summary>
	///     Bitwise flags indicating passability, combustion, and constructed status.
	/// </summary>
	public TileTraits Flags;
}
