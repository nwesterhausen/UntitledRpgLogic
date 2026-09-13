using UntitledRpgLogic.Core.World;
using UntitledRpgLogic.WorldGen.Models;

namespace UntitledRpgLogic.WorldGen;

/// <summary>
///     Pure domain service synthesizing individual 16x16 world chunks from macro simulation context.
/// </summary>
public interface IChunkGeneratorService
{
	/// <summary>
	///     Synthesizes a populated <see cref="WorldChunk" /> at the specified chunk grid coordinates.
	/// </summary>
	public WorldChunk GenerateChunk(Ulid mapId, int chunkX, int chunkY, WorldGenContext context);
}
