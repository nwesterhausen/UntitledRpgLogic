namespace UntitledRpgLogic.Core.Classes;

/// <summary>
///     Represents a complete manifest of all modules and their versions for a given game instance (client or server).
/// </summary>
/// <param name="GameVersion">The semantic version of the core game executable (e.g., "1.2.0").</param>
/// <param name="Modules">A list of all modules being used.</param>
public record ContentManifest(string GameVersion, IReadOnlyList<ModuleInfo> Modules);
