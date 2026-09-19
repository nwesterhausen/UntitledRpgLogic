namespace UntitledRpgLogic.Core.World.Generation;

/// <summary>
///     Pure domain service synthesizing individual world chunks from macro simulation context.
/// </summary>
public interface IChunkGeneratorService
{
	/// <summary>
	///     Synthesizes a populated <see cref="WorldChunk" /> at the specified chunk grid coordinates.
	/// </summary>
	public WorldChunk GenerateChunk(Ulid mapId, int chunkX, int chunkY, WorldGenContext context);
}
