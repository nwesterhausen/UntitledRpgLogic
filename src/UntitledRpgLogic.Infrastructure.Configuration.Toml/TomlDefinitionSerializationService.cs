using System.Text;
using System.Text.Json;
using Tomlyn;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Configuration.Serialization;
using UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml;

/// <inheritdoc />
public sealed class TomlDefinitionSerializationService : IDefinitionSerializationService
{
	private readonly TomlSerializerOptions options;

	/// <summary>
	///		Create a new TOML definition serializer.
	/// </summary>
	public TomlDefinitionSerializationService() =>
		this.options = new TomlSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
			DefaultIgnoreCondition = TomlIgnoreCondition.WhenWritingNull,
			Converters = [
				new TomlNameConverter(),
				new TomlUlidConverter()
			],
		};

	/// <inheritdoc />
	public string DefaultFileExtension => ".toml";

	/// <inheritdoc />
	public IReadOnlyCollection<string> SupportedFileExtensions { get; } = [".toml"];

	/// <inheritdoc />
	public TModel Deserialize<TModel>(string content) where TModel : class, IDefined
	{
		// 1. Delegate to mapping logic to find the registered DTO
		// Alternatively, invoke the DTO deserializer directly:
		if (typeof(TModel) == typeof(ItemDefinition))
		{
			var dto = TomlSerializer.Deserialize<ItemDefinitionDto>(content, this.options);
			return (dto.ToModel() as TModel)!;
		}

		throw new NotSupportedException($"No TOML configuration DTO registered for {typeof(TModel).Name}.");
	}

	/// <inheritdoc />
	public TModel Deserialize<TModel>(Stream stream) where TModel : class, IDefined
	{
		using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
		return this.Deserialize<TModel>(reader.ReadToEnd());
	}

	/// <inheritdoc />
	public string Serialize<TModel>(TModel model) where TModel : class, IDefined
	{
		if (model is ItemDefinition itemDef)
		{
			var dto = ItemDefinitionDto.FromModel(itemDef);
			return TomlSerializer.Serialize(dto, this.options);
		}

		throw new NotSupportedException($"No TOML configuration DTO registered for {typeof(TModel).Name}.");
	}

	/// <inheritdoc />
	public void Serialize<TModel>(TModel model, Stream stream) where TModel : class, IDefined
	{
		var content = this.Serialize(model);
		using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
		writer.Write(content);
		writer.Flush();
	}
}
