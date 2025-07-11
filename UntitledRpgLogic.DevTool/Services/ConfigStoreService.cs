using System.Collections.Concurrent;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

public class ConfigStoreService : IConfigStore
{
    private readonly ConcurrentDictionary<Guid, ITomlConfig> _configs = new();

    public ModuleInfoConfig ModuleInfo { get; set; } = new() { Name = string.Empty };

    /// <inheritdoc />
    public event Action? OnChange;

    /// <inheritdoc />
    public ITomlConfig? GetConfig(Guid key)
    {
        _ = _configs.TryGetValue(key, out ITomlConfig? config);
        return config;
    }

    /// <inheritdoc />
    public void UpdateConfig(Guid key, ITomlConfig config)
    {
        _configs[key] = config;
    }

    /// <inheritdoc />
    public IEnumerable<Guid> GetAllKeys() => _configs.Keys;

    /// <inheritdoc />
    public AuthorConfig Author { get; private set; } = new() { AuthorName = "", Website = "", AuthorId = Guid.NewGuid() };

    /// <inheritdoc />
    public string AuthorGuid
    {
        get => Author.AuthorId.ToString();
        set
        {
            if (Author.AuthorId.ToString() == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value) || value is null)
            {
                Author.AuthorId = Guid.NewGuid();
            }
            else
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    Author.AuthorId = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid GUID format.", nameof(value));
                }
            }
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string AuthorName
    {
        get => Author.AuthorName;
        set
        {
            if (Author.AuthorName == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value) || value is null)
            {
                Author.AuthorName = string.Empty;
            }

            Author.AuthorName = value;
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string AuthorWebsite
    {
        get => Author.Website;
        set
        {
            if (Author.Website == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value) || value is null)
            {
                Author.Website = string.Empty;
            }

            Author.Website = value;
            OnChange?.Invoke();
        }
    }
}
