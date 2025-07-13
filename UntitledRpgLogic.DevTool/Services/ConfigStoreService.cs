using System.Collections.Concurrent;
using Microsoft.JSInterop;
using UntitledRpgLogic.Core.Configuration;
using UntitledRpgLogic.Core.Interfaces;
using UntitledRpgLogic.DevTool.Services.Contracts;

namespace UntitledRpgLogic.DevTool.Services;

public class ConfigStoreService : IConfigStore
{
    private readonly ConcurrentDictionary<Guid, ITomlConfig> _configs = new();
    private readonly IJSRuntime _jsRuntime;

    public ConfigStoreService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        InitializeFromCache().ConfigureAwait(true);
    }

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
    public AuthorConfig Author { get; } =
        new() { AuthorName = "", Website = "", AuthorId = Guid.NewGuid() };

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

            if (string.IsNullOrWhiteSpace(value))
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

            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorGuid", Author.AuthorId.ToString())
                .ConfigureAwait(true);
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

            if (string.IsNullOrWhiteSpace(value))
            {
                Author.AuthorName = string.Empty;
            }

            Author.AuthorName = value;
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorName", Author.AuthorName)
                .ConfigureAwait(true);
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

            if (string.IsNullOrWhiteSpace(value))
            {
                Author.Website = string.Empty;
            }

            Author.Website = value;
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorUrl", Author.Website)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    private async Task InitializeFromCache()
    {
        string? authorName = await _jsRuntime.InvokeAsync<string?>("readCache", "authorName");
        string? authorGuid = await _jsRuntime.InvokeAsync<string?>("readCache", "authorGuid");
        string? authorUrl = await _jsRuntime.InvokeAsync<string?>("readCache", "authorUrl");

        Author.AuthorName = authorName ?? string.Empty;
        Author.AuthorId = string.IsNullOrWhiteSpace(authorGuid) ? Guid.NewGuid() : Guid.Parse(authorGuid);
        Author.Website = authorUrl ?? string.Empty;

        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorName", Author.AuthorName);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorGuid", Author.AuthorId.ToString());
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorUrl", Author.Website);

        OnChange?.Invoke();
    }
}
