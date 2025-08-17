namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents identifying information for a single game module or content pack.
/// </summary>
/// <param name="Id">The unique <see cref="Ulid" /> identifying the module.</param>
/// <param name="VersionHash">A cryptographic hash (e.g., SHA-256) of the module's content, representing its exact version.</param>
public record ModuleInfo(Ulid Id, string VersionHash);
