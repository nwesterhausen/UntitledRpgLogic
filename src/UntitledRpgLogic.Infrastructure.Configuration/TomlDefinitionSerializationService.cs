using System.Text;
using Tomlyn;
using UntitledRpgLogic.Core.Data;
using UntitledRpgLogic.Core.Data.Urpglib;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Configuration.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml;

public sealed class TomlDefinitionSerializationService : IDefinitionSerializationService
{
	public string FileExtension => ".toml";

	public TModel Deserialize<TModel>(string content) where TModel : class, IDefined
	{
		// 1. Delegate to mapping logic to find the registered DTO
		// Alternatively, invoke the DTO deserializer directly:
		if (typeof(TModel) == typeof(ItemDefinition))
		{
			var dto = TomlSerializer.Deserialize<ItemDefinitionDto>(content);
			return (dto.ToModel() as TModel)!;
		}

		throw new NotSupportedException($"No TOML configuration DTO registered for {typeof(TModel).Name}.");
	}

	public TModel Deserialize<TModel>(Stream stream) where TModel : class, IDefined
	{
		using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, leaveOpen: true);
		return Deserialize<TModel>(reader.ReadToEnd());
	}

	public string Serialize<TModel>(TModel model) where TModel : class, IDefined
	{
		if (model is ItemDefinition itemDef)
		{
			var dto = ItemDefinitionDto.FromModel(itemDef);
			return TomlSerializer.Serialize(dto);
		}

		throw new NotSupportedException($"No TOML configuration DTO registered for {typeof(TModel).Name}.");
	}

	public void Serialize<TModel>(TModel model, Stream stream) where TModel : class, IDefined
	{
		string content = Serialize(model);
		using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
		writer.Write(content);
		writer.Flush();
	}
}
