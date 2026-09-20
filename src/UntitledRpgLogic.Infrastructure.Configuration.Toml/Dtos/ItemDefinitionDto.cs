using System.Text.Json.Serialization;
using Tomlyn.Serialization;
using UntitledRpgLogic.Core.Common;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Infrastructure.Configuration.Dtos;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

/// <inheritdoc />
public sealed class ItemDefinitionDto : ITomlConfigDto<ItemDefinitionDto, ItemDefinition>
{
	[JsonPropertyName("id")] public Ulid Id { get; init; }

	[JsonPropertyName("name")] public Name Name { get; init; } = default!;

	[JsonPropertyName("description")] public string? Description { get; init; }

	[JsonPropertyName("max_stack")] public int MaxStack { get; init; } = 1;

	[JsonPropertyName("type")] public ItemType MainType { get; init; } = ItemType.Miscellaneous;

	[JsonPropertyName("sub_type")] public ItemSubtype SubType { get; init; } = ItemSubtype.None;

	[JsonPropertyName("base_quality")] public Quality BaseQuality { get; init; } = Quality.Common;

	[JsonPropertyName("base_durability")] public float BaseDurability { get; init; } = 100f;

	[JsonPropertyName("base_value")] public int BaseValue { get; init; } = 1;

	[JsonPropertyName("weight")] public float Weight { get; init; } = 1f;

	[JsonPropertyName("materials")]
	[TomlSingleOrArray]
	public List<ItemMaterialComponentDto> Materials { get; init; } = [];

	/// <inheritdoc />
	public ItemDefinition ToModel() =>
		new()
		{
			Id = this.Id,
			Name = this.Name,
			Description = this.Description ?? string.Empty,
			MaxStackSize = this.MaxStack,
			ItemType = this.MainType,
			ItemSubtype = this.SubType,
			BaseQuality = this.BaseQuality,
			BaseDurability = this.BaseDurability,
			BaseValue = this.BaseValue,
			Weight = this.Weight,
			Materials = this.Materials.ConvertAll(m => m.ToModel())
		};

	/// <inheritdoc />
	public static ItemDefinitionDto FromModel(ItemDefinition model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new ItemDefinitionDto
		{
			Id = model.Id,
			Name = model.Name,
			Description = model.Description,
			MaxStack = model.MaxStackSize,
			MainType = model.ItemType,
			SubType = model.ItemSubtype,
			BaseQuality = model.BaseQuality,
			BaseDurability = model.BaseDurability,
			BaseValue = model.BaseValue,
			Weight = model.Weight,
			Materials = model.Materials.Select(ItemMaterialComponentDto.FromModel).ToList()
		};
	}
}
