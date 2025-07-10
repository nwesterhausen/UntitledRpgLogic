using System.Collections.Concurrent;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

public class ConfigStoreService : IConfigStore
{
    private readonly ConcurrentDictionary<Guid, ITomlConfig> _configs = new();

    public AuthorConfig Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ModuleInfoConfig ModuleInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ITomlConfig? GetConfig(Guid key)
    {
        _ = _configs.TryGetValue(key, out ITomlConfig? config);
        return config;
    }

    public void UpdateConfig(Guid key, ITomlConfig config)
    {
        _configs[key] = config;
    }

    public IEnumerable<Guid> GetAllKeys() => _configs.Keys;
}
