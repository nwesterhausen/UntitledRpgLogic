using Tomlet;

namespace UntitledRpgLogic.Infrastructure.Configuration.Tomlet;

/// <summary>
///     Definition of custom mappers for types that Tomlet does not support by default.
/// </summary>
public static partial class MapperRegistration
{
	/// <summary>
	///     Register the custom mappers to handle types we use that Tomlet does not support by default, such as
	///     <see cref="Guid" />.
	/// </summary>
	public static void RegisterMappers()
	{
		// Register custom mappers for types that Tomlet does not support by default.
		TomletMain.RegisterMapper(SerializeUlid, DeserializeUlid);
		TomletMain.RegisterMapper(SerializeColor, DeserializeColor);
		TomletMain.RegisterMapper(SerializeDimensionScale, DeserializeDimensionScale);
		TomletMain.RegisterMapper(SerializeMassScale, DeserializeMassScale);
	}
}
