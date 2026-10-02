using UntitledRpg.LibraryFile;

namespace UntitledRpgLogic.Core.Data.Urpglib;

/// <summary>
///     Resolves the appropriate serialization service based on file extensions or paths.
/// </summary>
public interface IDefinitionSerializerRouter
{
	public IDefinitionSerializationService GetServiceForExtension(string extension);
	public IDefinitionSerializationService GetServiceForFile(string filePath);
	public IDefinitionSerializationService GetServiceForEntry(UrpglibPackageEntry entry);
	public bool SupportsExtension(string extension);
	public bool SupportsFile(string filePath);
	public bool SupportsEntry(UrpglibPackageEntry entry);
	public bool TryResolveTypeFromEntry(UrpglibPackageEntry entry, out Type? definitionType);
	public bool TryResolveTypeFromFile(string filePath, out Type? definitionType);
	public Type GetDefinitionTypeFromEntry(UrpglibPackageEntry entry);
	public Type GetDefinitionTypeFromFile(string filePath);
}
