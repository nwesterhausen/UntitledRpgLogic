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
    public string ModuleName
    {
        get => this.ModuleInfo.Name;
        set
        {
            if (this.ModuleInfo.Name == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                this.ModuleInfo.Name = string.Empty;
            }
            else
            {
                this.ModuleInfo.Name = value;
            }
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleName", this.ModuleInfo.Name)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string ModuleDescription { get => this.ModuleInfo.Description;
        set
        {
            if (this.ModuleInfo.Description == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                this.ModuleInfo.Description = string.Empty;
            }
            else
            {
                this.ModuleInfo.Description = value;
            }
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleDescription", this.ModuleInfo.Description)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        } }

    /// <inheritdoc />
    public string ModuleVersion
    {
        get => this.ModuleInfo.Version;
        set
        {
            if (this.ModuleInfo.Version == value)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                this.ModuleInfo.Version = "1.0.0";
            }
            else
            {
                this.ModuleInfo.Version = value;
            }
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleVersion", this.ModuleInfo.Version)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public int ModuleVersionNumber
    {
        get => this.ModuleInfo.VersionNumber;
        set
        {
            if (this.ModuleInfo.VersionNumber == value)
            {
                return;
            }
            this.ModuleInfo.VersionNumber = value;
            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleVersionNumber", this.ModuleInfo.VersionNumber)
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public string ModuleGuid
    {
        get => this.ModuleInfo.Id.ToString();
        set
        {
            if (this.ModuleInfo.Id.ToString() == value)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                this.ModuleInfo.Id = Guid.NewGuid();
            }
            else
            {
                if (Guid.TryParse(value, out Guid guid))
                {
                    this.ModuleInfo.Id = guid;
                }
                else
                {
                    throw new ArgumentException("Invalid GUID format.", nameof(value));
                }
            }

            _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleGuid", this.ModuleInfo.Id.ToString())
                .ConfigureAwait(true);
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public event Action? OnChange;

    /// <inheritdoc />
    public bool IsReady { get; private set; } = false;

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
        string? moduleName = await _jsRuntime.InvokeAsync<string?>("readCache", "moduleName");
        string? moduleDescription = await _jsRuntime.InvokeAsync<string?>("readCache", "moduleDescription");
        string? moduleVersion = await _jsRuntime.InvokeAsync<string?>("readCache", "moduleVersion");
        int? moduleVersionNumber =
            await _jsRuntime.InvokeAsync<int>("readCache", "moduleVersionNumber");
        string? moduleGuid = await _jsRuntime.InvokeAsync<string?>("readCache", "moduleGuid");

        Author.AuthorName = authorName ?? string.Empty;
        Author.AuthorId = string.IsNullOrWhiteSpace(authorGuid) ? Guid.NewGuid() : Guid.Parse(authorGuid);
        Author.Website = authorUrl ?? string.Empty;
        ModuleInfo.Name = moduleName ?? string.Empty;
        ModuleInfo.Description = moduleDescription ?? string.Empty;
        ModuleInfo.Version = moduleVersion ?? "1.0.0";
        ModuleInfo.VersionNumber = moduleVersionNumber ?? 1;
        ModuleInfo.Id = string.IsNullOrWhiteSpace(moduleGuid) ? Guid.NewGuid() : Guid.Parse(moduleGuid);

        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorName", Author.AuthorName);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorGuid", Author.AuthorId.ToString());
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "authorUrl", Author.Website);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleName", ModuleInfo.Name);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleDescription", ModuleInfo.Description);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleVersion", ModuleInfo.Version);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleVersionNumber", ModuleInfo.VersionNumber);
        await _jsRuntime.InvokeVoidAsync("updateUserDetailCache", "moduleGuid", ModuleInfo.Id.ToString());

        this.IsReady = true;
        OnChange?.Invoke();
    }
}
