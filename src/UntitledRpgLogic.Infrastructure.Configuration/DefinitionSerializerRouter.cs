using System.Diagnostics.CodeAnalysis;
using UntitledRpg.LibraryFile;
using UntitledRpgLogic.Core.Abilities.Effects;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;

namespace UntitledRpgLogic.Infrastructure.Configuration;

public sealed class DefinitionSerializerRouter : IDefinitionSerializerRouter
{
	private static readonly Dictionary<string, Type> PathToTypeMap = new(StringComparer.OrdinalIgnoreCase)
	{
		["items"] = typeof(ItemDefinition),
		["skills"] = typeof(SkillDefinition),
		["stats"] = typeof(StatDefinition),
		["materials"] = typeof(MaterialDefinition),
		["effects"] = typeof(Effect)
	};

	private readonly Dictionary<string, IDefinitionSerializationService> _servicesByExtension;

	public DefinitionSerializerRouter(IEnumerable<IDefinitionSerializationService> services)
	{
		ArgumentNullException.ThrowIfNull(services);
		this._servicesByExtension =
			new Dictionary<string, IDefinitionSerializationService>(StringComparer.OrdinalIgnoreCase);

		foreach (var service in services)
		{
			foreach (var ext in service.SupportedFileExtensions)
			{
				var normalized = NormalizeExtension(ext);

				// Replaced ContainsKey + indexer lookup with a single TryGetValue call
				if (this._servicesByExtension.TryGetValue(normalized, out var existingService))
				{
					throw new InvalidOperationException(
						$"Duplicate file extension registration: '{normalized}' is already registered to " +
						$"{existingService.GetType().Name}. Cannot re-register for {service.GetType().Name}.");
				}

				this._servicesByExtension.Add(normalized, service);
			}
		}
	}

	public IDefinitionSerializationService GetServiceForExtension(string extension)
	{
		ArgumentNullException.ThrowIfNull(extension);
		var normalized = NormalizeExtension(extension);
		if (this._servicesByExtension.TryGetValue(normalized, out var service))
		{
			return service;
		}

		throw new NotSupportedException($"No serialization service registered for file extension '{extension}'.");
	}

	public IDefinitionSerializationService GetServiceForFile(string filePath)
	{
		var extension = Path.GetExtension(filePath);
		return this.GetServiceForExtension(extension);
	}

	public bool SupportsExtension(string extension)
	{
		ArgumentNullException.ThrowIfNull(extension);
		return this._servicesByExtension.ContainsKey(NormalizeExtension(extension));
	}

	public bool SupportsFile(string filePath) =>
		this.SupportsExtension(Path.GetExtension(filePath));


	public bool TryResolveTypeFromEntry(UrpglibPackageEntry entry, [NotNullWhen(true)] out Type? definitionType)
	{
		ArgumentNullException.ThrowIfNull(entry);
		return this.TryResolveTypeFromFile(entry.Name, out definitionType);
	}

	public Type GetDefinitionTypeFromEntry(UrpglibPackageEntry entry)
	{
		ArgumentNullException.ThrowIfNull(entry);
		return this.GetDefinitionTypeFromFile(entry.Name);
	}

	/// <summary>
	///     Infers the catalog definition Type from a relative path or filename.
	///     E.g., "content/items/iron_sword.toml" -> typeof(ItemDefinition)
	/// </summary>
	public bool TryResolveTypeFromFile(string filePath, [NotNullWhen(true)] out Type? definitionType)
	{
		ArgumentNullException.ThrowIfNull(filePath);
		var segments = filePath.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

		foreach (var segment in segments)
		{
			if (PathToTypeMap.TryGetValue(segment, out definitionType))
			{
				return true;
			}
		}

		definitionType = null;
		return false;
	}

	public IDefinitionSerializationService GetServiceForEntry(UrpglibPackageEntry entry)
	{
		ArgumentNullException.ThrowIfNull(entry);
		return this.GetServiceForFile(entry.Name);
	}

	public bool SupportsEntry(UrpglibPackageEntry entry)
	{
		ArgumentNullException.ThrowIfNull(entry);
		return this.SupportsFile(entry.Name);
	}

	public Type GetDefinitionTypeFromFile(string filePath)
	{
		if (this.TryResolveTypeFromFile(filePath, out var definitionType))
		{
			return definitionType;
		}

		throw new NotSupportedException($"Unable to determine definition type from {filePath}");
	}

	private static string NormalizeExtension(string ext) =>
		ext.StartsWith('.') ? ext : $".{ext}";
}
