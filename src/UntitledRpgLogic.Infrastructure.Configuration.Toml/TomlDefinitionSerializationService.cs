using System.Text;
using System.Text.Json;
using Tomlyn;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Skills;
using UntitledRpgLogic.Core.Stats;
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
		if (typeof(TModel) == typeof(ItemDefinition))
		{
			if (TomlSerializer.TryDeserialize<ItemDefinitionDto>(
				    content, out var itemDefinitionDto, this.options))
			{
				return (itemDefinitionDto.ToModel() as TModel)!;
			}

			if (TomlSerializer.TryDeserialize<StatDefinitionDto>(
				    content, out var statDefinitionDto, this.options))
			{
				return (statDefinitionDto.ToModel() as TModel)!;
			}

			if (TomlSerializer.TryDeserialize<SkillDefinitionDto>(
				    content, out var skillDefinitionDto, this.options))
			{
				return (skillDefinitionDto.ToModel() as TModel)!;
			}

			throw new NotSupportedException($"Invalid TOML file encountered for  {typeof(TModel).Name}.");
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
			return TomlSerializer.Serialize(ItemDefinitionDto.FromModel(itemDef), this.options);
		}
		if (model is StatDefinition statDef)
		{
			return TomlSerializer.Serialize(StatDefinitionDto.FromModel(statDef), this.options);
		}
		if (model is SkillDefinition skillDef)
		{
			return TomlSerializer.Serialize(SkillDefinitionDto.FromModel(skillDef), this.options);
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
