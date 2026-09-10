using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UntitledRpgLogic.Infrastructure.Data.ValueConverters;

/// <summary>
/// 	Conversion for the elemental attunement which is a dict of ID, AMNT
/// </summary>
public class ElementalAttunementConverter : ValueConverter<Dictionary<Ulid, float>, string>
{
	/// <inheritdoc />
	public ElementalAttunementConverter() : base(
		// Convert to JSON string for storage in the database
		dict => JsonSerializer.Serialize(
			dict.ToDictionary(k => k.Key.ToString(), v => v.Value),
			(JsonSerializerOptions?)null),

		// Read JSON string back to Dictionary<Ulid, float>
		json => string.IsNullOrEmpty(json)
			? new Dictionary<Ulid, float>()
			: JsonSerializer.Deserialize<Dictionary<string, float>>(json, (JsonSerializerOptions?)null)!
				.ToDictionary(k => Ulid.Parse(k.Key), v => v.Value)
	)
	{
	}
}
