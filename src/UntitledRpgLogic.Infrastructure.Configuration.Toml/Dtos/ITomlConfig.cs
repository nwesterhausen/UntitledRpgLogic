using UntitledRpgLogic.Core.Data;

namespace UntitledRpgLogic.Infrastructure.Configuration.Dtos;

/// <summary>
/// Internal contract used exclusively by the TOML serializer adapter to
/// map TOML schema representations to and from Core domain models.
/// </summary>
internal interface ITomlConfigDto<TSelf, TModel>
	where TSelf : ITomlConfigDto<TSelf, TModel>
	where TModel : class, IDefined
{
	public TModel ToModel();
	public static abstract TSelf FromModel(TModel model);
}
