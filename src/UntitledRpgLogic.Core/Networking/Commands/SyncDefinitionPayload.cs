namespace UntitledRpgLogic.Core.Networking.Commands;

/// <summary>
///     Server payload broadcasting a newly authored or dynamically generated content definition to clients.
/// </summary>
/// <typeparam name="T">The catalog definition type being synchronized.</typeparam>
/// <param name="DefinitionType">The string name or classification of the definition type.</param>
/// <param name="Definition">The definition instance data.</param>
public record SyncDefinitionPayload<T>(string DefinitionType, T Definition);
