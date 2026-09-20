using System.Text.Json.Serialization;
using UntitledRpgLogic.Core.Items;
using UntitledRpgLogic.Core.Materials;

namespace UntitledRpgLogic.Infrastructure.Configuration.Toml.Dtos;

public sealed class ItemMaterialComponentDto
{
	[JsonPropertyName("id")] public Ulid Id { get; init; }

	[JsonPropertyName("mass_proportion")] public float MassRatio { get; init; } = 1f;

	[JsonPropertyName("slot")] public MaterialSlot Slot { get; init; } = MaterialSlot.Primary;

	public ItemMaterialComponent ToModel() => new(this.Id, this.MassRatio, this.Slot);

	public static ItemMaterialComponentDto FromModel(ItemMaterialComponent model)
	{
		ArgumentNullException.ThrowIfNull(model);

		return new ItemMaterialComponentDto { Id = model.MaterialId, MassRatio = model.Proportion, Slot = model.Slot };
	}
}
