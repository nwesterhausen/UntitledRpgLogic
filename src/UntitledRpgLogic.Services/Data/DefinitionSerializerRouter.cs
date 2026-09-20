using UntitledRpgLogic.Core.Data.Urpglib;

namespace UntitledRpgLogic.Services.Data;

/// <inheritdoc />
public sealed class DefinitionSerializerRouter : IDefinitionSerializerRouter
{
    private readonly Dictionary<string, IDefinitionSerializationService> servicesByExtension;

    /// <summary>
    ///		Create a serialization router to route serialize/deserialize for module definition files.
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public DefinitionSerializerRouter(IEnumerable<IDefinitionSerializationService> services)
    {
	    ArgumentNullException.ThrowIfNull(services);

	    this.servicesByExtension = new Dictionary<string, IDefinitionSerializationService>(StringComparer.OrdinalIgnoreCase);

        foreach (var service in services)
        {
            foreach (var ext in service.SupportedFileExtensions)
            {
                var normalized = NormalizeExtension(ext);
                if (this.servicesByExtension.TryGetValue(normalized, out var existingService))
                {
	                throw new InvalidOperationException(
		                $"Duplicate file extension registration: '{normalized}' is already registered to " +
		                $"{existingService.GetType().Name}. Cannot re-register for {service.GetType().Name}.");
                }

                this.servicesByExtension[normalized] = service;
            }
        }
    }

    /// <inheritdoc />
    public IDefinitionSerializationService GetServiceForExtension(string extension)
    {
	    ArgumentNullException.ThrowIfNull(extension);

        var normalized = NormalizeExtension(extension);
        if (this.servicesByExtension.TryGetValue(normalized, out var service))
        {
            return service;
        }

        throw new NotSupportedException($"No serialization service registered for file extension '{extension}'.");
    }

    /// <inheritdoc />
    public IDefinitionSerializationService GetServiceForFile(string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return this.GetServiceForExtension(extension);
    }

    /// <inheritdoc />
    public bool SupportsExtension(string extension)
    {
	    ArgumentNullException.ThrowIfNull(extension);
	    return this.servicesByExtension.ContainsKey(NormalizeExtension(extension));
    }

    /// <inheritdoc />
    public bool SupportsFile(string filePath) => this.SupportsExtension(Path.GetExtension(filePath));

    private static string NormalizeExtension(string ext) =>
        ext.StartsWith('.') ? ext : $".{ext}";
}
