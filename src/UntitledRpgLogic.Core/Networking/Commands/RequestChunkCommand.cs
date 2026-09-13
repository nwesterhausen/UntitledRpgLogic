namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Client command requesting terrain tile data for a specific chunk coordinate.
/// </summary>
/// <param name="MapId">The unique identifier of the map containing the chunk.</param>
/// <param name="ChunkX">The horizontal chunk coordinate.</param>
/// <param name="ChunkY">The vertical chunk coordinate.</param>
public record RequestChunkCommand(Ulid MapId, int ChunkX, int ChunkY);
